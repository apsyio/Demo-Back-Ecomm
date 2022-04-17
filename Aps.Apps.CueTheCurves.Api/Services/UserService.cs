using Appstagram.Base.Enums;
using Appstagram.Base.Extensions;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Apstagram.Auth.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class UserService : UserServiceBase<Users, UserInput>,IUserService
    {
        private readonly IUserRepository userRepo;

        public UserService(IUserRepository userRepo)
            : base(userRepo)
        {
            this.userRepo = userRepo;
        }

        public ResponseBase DeactiveUser(Users user)
        {
            user.IsDeleted = true;
            Update(user);
            return ResponseBase.Success();
        }

        public override ResponseBase<Users> Add(Users user)
        {
            var preUser = userRepo.GetDbSet<Users>().IgnoreQueryFilters()
                .Where(a => a.Email == user.Email).FirstOrDefault();
            if(preUser is null)
            {
                return base.Add(user);
            }
            else if(preUser.IsDeleted)
            {
                preUser.IsDeleted = false;
                var context = userRepo.GetDbContext();
                context.Set<Users>().Update(preUser);
                context.SaveChanges();
                return ResponseBase<Users>.Success(preUser);
            }
            else
            {
                return ResponseBase<Users>.Failure(ResponseStatus.ALREADY_EXIST);
            }

        }

        public ResponseBase<UserDto> GetInspo(int inspoId)
        {
            TypeAdapterConfig<Users, UserDto>
                .NewConfig()
                .Map(dest => dest.Brands, src => src.UserBrands == null ? null : src.UserBrands.Select(x => x.Brand))
                .Map(dest => dest.Socials, src => src.Socials == null ? null : src.Socials.ToList())
                .Map(dest => dest.Closets, src => src.Closets == null ? null : src.Closets.ToList());

            var user = userRepo.Where<Users>(a => a.Id == inspoId)
                .Include(a => a.Socials)
                .Include(a => a.Closets)
                    .ThenInclude(a => a.ClosetItems)
                .Include(a => a.UserBrands)
                    .ThenInclude(a => a.Brand)
                .ProjectToType<UserDto>()
                .FirstOrDefault();

            return ResponseBase<UserDto>.Success(user);
        }

        public ListResponseBase<Users> GetInspos(Users user, bool isCommon, bool isRandom)
        {
            List<int> userStyles = new List<int>();
            List<int> userBrands = new List<int>();

            if (isCommon)
            {
                userStyles = userRepo.Where<UserStyles>(a => a.UserId == user.Id)
                    .Select(a => a.StyleId).ToList();
                userBrands = userRepo.Where<UserBrands>(a => a.UserId == user.Id)
                    .Select(a => a.BrandId).ToList();
            }
            var users = userRepo.GetDbSet().AsQueryable();
            if (isCommon)
            {
                users = users.Where(a => user.UserStyles.Any(x => userStyles.Any(z => z == x.StyleId))
                    || user.UserBrands.Any(x => userBrands.Any(z => z == x.BrandId)));
            }
            if (isRandom) users = users.OrderBy(a => Guid.NewGuid());
            if (user.IsAdmin)
            {
                users = users
                    .Where(a => a.AccountType == AccountTypes.PUBLIC || a.AccountType == AccountTypes.PRIVATE);
            }
            else
            {
                users = users.Where(a => a.AccountType == AccountTypes.PUBLIC);
            }

            return ListResponseBase<Users>.Success(users);
        }

        public ListResponseBase<Users> GetSelectedInspos(Users user)
        {
            return ListResponseBase<Users>.Success(userRepo.Where<Users>(a => a.IsSelected));
        }

        public ResponseBase SetBrands(List<int> brandIds, Users user)
        {
            var brands = userRepo.GetDbSet<Brands>().Where(a => brandIds.Contains(a.Id));
            if (brands == null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }


            var userBrands = userRepo.GetDbSet<UserBrands>().Where(a => a.UserId == user.Id);
            userRepo.DeleteRange(userBrands);
            userRepo.AddRange(brands.Select(a => new UserBrands { UserId = user.Id, BrandId = a.Id }));
            return ResponseBase.Success();
        }

        public ResponseBase SetStyles(List<int> styleIds, Users user)
        {
            var styles = userRepo.GetDbSet<Styles>().Where(a => styleIds.Contains(a.Id));
            if (styles == null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }


            var userStyles = userRepo.GetDbSet<UserStyles>().Where(a => a.UserId == user.Id);
            userRepo.DeleteRange(userStyles);
            userRepo.AddRange(styles.Select(a => new UserStyles { UserId = user.Id, StyleId = a.Id }));
            return ResponseBase.Success();
        }

        public override ResponseBase<Users> Update(UserInput input)
        {
            if(input.Socials != null && input.Socials.Count > 0)
            {
                var preSocials = userRepo.GetDbSet<UserSocials>().Where(a => a.UserId == input.Id);
                userRepo.DeleteRange(preSocials);
                var socials = input.Socials.AsQueryable().ProjectToType<UserSocials>().ToList();
                socials.ForEach(a => a.UserId = input.Id ?? 0);
                userRepo.AddRange(socials);
            }
            return base.Update(input);
        }

        public ResponseBase<StatDto> GetAppStats(Users user)
        {
            if (!user.IsAdmin)
            {
                return ResponseBase<StatDto>.Failure(ResponseStatus.NOT_ALLOWED);
            }

            var brandsCount = userRepo.GetDbSet<Brands>().Count();
            var stylesCount = userRepo.GetDbSet<Styles>().Count();
            var usersCount = userRepo.GetDbSet<Users>().Count();

            return new ResponseBase<StatDto>
            {
                Result = new StatDto
                {
                    BrandsCount = brandsCount,
                    StylesCount = stylesCount,
                    UsersCount = usersCount
                }
            };
        }

        public ResponseBase DeactiveUserAdmin(int userId)
        {
            var user = userRepo.GetById(userId);
            if (!user.IsActive) return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            user.IsActive = false;
            Update(user);
            return ResponseBase.Success();
        }

        public ResponseBase ActiveUserAdmin(int userId)
        {
            var user = userRepo.GetById(userId);
            if (user.IsActive) return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            user.IsActive = true;
            Update(user);
            return ResponseBase.Success();
        }

        public ResponseBase SetSelectedInspos(List<int> userIds)
        {
            var selectedUsers = userRepo.Where<Users>(a => userIds.Contains(a.Id)).ToList();
            if (selectedUsers.Count() != userIds.Count()) return ResponseBase.Failure(ResponseStatus.DIFFRENET_IDS);
            userRepo.GetDbContext().Database.ExecuteSqlRaw("UPDATE Users SET IsSelected = 0");
            selectedUsers.ForEach(a => a.IsSelected = true);
            userRepo.UpdateRange(selectedUsers);
            return ResponseBase.Success();
        }
    }
}

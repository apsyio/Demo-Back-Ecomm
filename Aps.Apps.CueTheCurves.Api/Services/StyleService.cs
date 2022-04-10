using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class StyleService : ServiceBase<Styles, InputDef>,IStyleService
    {
        private readonly IStyleRepository styleRepository;
        private readonly IUserRepository userRepository;

        public StyleService(IStyleRepository styleRepository, IUserRepository userRepository)
            :base(styleRepository)
        {
            this.styleRepository = styleRepository;
            this.userRepository = userRepository;
        }

        public override ResponseBase<Styles> Add(Styles entity)
        {
            if(styleRepository.Any(a => a.Name == entity.Name))
            {
                return ResponseBase<Styles>.Failure(ResponseStatus.ALREADY_EXIST);
            }
            return base.Add(entity);
        }

        public ResponseBase ActiveStyle(int styleId)
        {
            var style = styleRepository.GetDbSet<Styles>().IgnoreQueryFilters()
                .Where(a => a.IsDeleted && a.Id == styleId)
                .FirstOrDefault();
            if (style is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            style.IsDeleted = false;
            var context = styleRepository.GetDbContext();
            context.Set<Styles>().Update(style);
            context.SaveChanges();
            return ResponseBase.Success();
        }

        public ResponseBase DeActiveStyle(int styleId)
        {
            var style = styleRepository
                .Where<Styles>(a => a.Id == styleId)
                .FirstOrDefault();
            if (style is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            style.IsDeleted = true;
            Update(style);
            return ResponseBase.Success();
        }

        public ResponseBase<StyleDto> GetStyle(Users user, int styleId)
        {
            TypeAdapterConfig<Styles, StyleDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.StyleLikes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.Inspos, src => src.UserStyles.Select(a => a.User).Where(a => a.AccountType == AccountTypes.PUBLIC))
                .Map(dest => dest.Brands, src => src.StyleBrands.Select(a => a.Brand))
                ;

            StyleDto style;

            if (user.IsAdmin)
            {
                style = styleRepository.GetDbSet()
                    .IgnoreQueryFilters().Where(a => a.Id == styleId)
                .ProjectToType<StyleDto>()
                .FirstOrDefault();
            }
            else
            {
                style = styleRepository.Where<Styles>(a => a.Id == styleId)
                .ProjectToType<StyleDto>()
                .FirstOrDefault();
            }

            if(style is null) return ResponseBase<StyleDto>.Failure(ResponseStatus.NOT_FOUND);

            return ResponseBase<StyleDto>.Success(style);
        }

        public ListResponseBase<Styles> GetStyles(Users user = null, bool isRemoved = false)
        {
            if (user == null)
            {
                if (isRemoved)
                {
                    return ListResponseBase<Styles>.Success(styleRepository.GetDbSet().IgnoreQueryFilters());
                }
                else
                {
                    return ListResponseBase<Styles>.Success(styleRepository.GetDbSet());
                }
                
            }
            return ListResponseBase<Styles>.Success(styleRepository.GetUserStyles(user.Id));
        }

        public ResponseBase LikeStyle(Users user, int styleId, bool liked)
        {
            bool add = true;
            var preLike = styleRepository.Where<StyleLikes>(a => a.UserId == user.Id && a.StyleId == styleId)
                .FirstOrDefault();
            if(preLike == null)
            {
                var styleLike = new StyleLikes
                {
                    Liked = liked,
                    StyleId = styleId,
                    UserId = user.Id
                };
                styleRepository.Add(styleLike);
            }
            else
            {
                if(preLike.Liked == liked)
                {
                    add = false;
                }
                else
                {
                    preLike.Liked = liked;
                    styleRepository.Update(preLike);
                }
            }
            if (add)
            {
                var style = styleRepository.GetById(styleId);
                style.LikesCount = liked ? style.LikesCount+1 : style.LikesCount-1;
                Update(style);
            }
            return ResponseBase.Success();
        }

        public ResponseBase<Styles> UpdateStyle(Styles style)
        {
            if (!styleRepository.Any(a => a.Id == style.Id))
            {
                return ResponseBase<Styles>.Failure(ResponseStatus.NOT_FOUND);
            }

            var preStyle = styleRepository.GetById(style.Id);
            preStyle.Name = string.IsNullOrEmpty(style.Name) ? preStyle.Name : style.Name;
            preStyle.Thumbnail = string.IsNullOrEmpty(style.Thumbnail) ? preStyle.Thumbnail : style.Thumbnail;
            preStyle.Photos = style.Photos is null ? preStyle.Photos : style.Photos;

            var context = styleRepository.GetDbContext();
            context.Set<Styles>().Update(preStyle);
            context.SaveChanges();
            return ResponseBase<Styles>.Success(preStyle);
        }
    }
}

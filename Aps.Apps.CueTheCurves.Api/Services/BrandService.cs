using Appstagram.Base.Enums;
using Appstagram.Base.Extensions;
using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Enums;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class BrandService : ServiceBase<Brands, InputDef>, IBrandService
    {
        private readonly IBrandRepository brandRepository;

        public BrandService(IBrandRepository brandRepository)
            :base(brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public override ResponseBase<Brands> Add(Brands entity)
        {
            if(brandRepository.Any(a => a.Name == entity.Name))
            {
                return ResponseBase<Brands>.Failure(ResponseStatus.ALREADY_EXIST);
            }
            return base.Add(entity);
        }


        public ResponseBase ActiveBrand(int brandId)
        {
            var brand = brandRepository.GetDbSet<Brands>().IgnoreQueryFilters()
                .Where(a => a.IsDeleted && a.Id == brandId)
                .FirstOrDefault();
            if(brand is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            brand.IsDeleted = false;
            var context = brandRepository.GetDbContext();
            context.Set<Brands>().Update(brand);
            context.SaveChanges();
            return ResponseBase.Success();
        }

        public ResponseBase DeActiveBrand(int brandId)
        {
            var brand = brandRepository
                .Where<Brands>(a => a.Id == brandId)
                .FirstOrDefault();
            if (brand is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            brand.IsDeleted = true;
            Update(brand);
            return ResponseBase.Success();
        }

        public ListResponseBase<BrandDto> GetAllBrands(bool withRemoved = false)
        {
            TypeAdapterConfig<Brands, BrandDto>
                .NewConfig()
                .Map(dest => dest.Styles, src => src.StyleBrands.Select(a => a.Style).ToList())
                .Map(dest => dest.Inspos, src => src.UserBrands.Select(a => a.User).Where(a => a.AccountType == AccountTypes.PUBLIC).ToList())
                .Map(dest => dest.Sizes, src => src.BrandSizes.Select(a => a.Size.Size).ToList())
                ;

            if (withRemoved)
            {
                var result = brandRepository.GetDbSet().IgnoreQueryFilters()
                                .ProjectToType<BrandDto>();

                return ListResponseBase<BrandDto>.Success(result);
            }
            else
            {
                var result = brandRepository.GetDbSet()
                .ProjectToType<BrandDto>();

                return ListResponseBase<BrandDto>.Success(result);
            }

            
        }

        public ResponseBase<BrandDto> GetBrand(Users user, int brandId)
        {
            TypeAdapterConfig<Brands, BrandDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.BrandLikes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.Styles, src => src.StyleBrands.Select(a => a.Style).ToList())
                .Map(dest => dest.Inspos, src => src.UserBrands.Select(a => a.User).Where(a => a.AccountType == AccountTypes.PUBLIC))
                .Map(dest => dest.Sizes, src => src.BrandSizes.Select(a => a.Size.Size).ToList())
                ;

            var result = brandRepository.Where<Brands>(a => a.Id == brandId)
                .ProjectToType<BrandDto>()
                .FirstOrDefault()
                ;

            if (result is null) return ResponseBase<BrandDto>.Failure(ResponseStatus.NOT_FOUND);
            return ResponseBase<BrandDto>.Success(result);
        }

        public ListResponseBase<Brands> GetBrands(List<int> brandIds = null)
        {
            if (brandIds.Count == 0) brandIds = null;
            return ListResponseBase<Brands>.Success(brandRepository.GetBrands(brandIds));
        }

        public ListResponseBase<Brands> GetBrands(Users user)
        {
            var userBrands = brandRepository.Where<UserBrands>(a => a.UserId == user.Id)
                .ExcludeRemovedItems()
                .Select(a => a.BrandId)
                .ToList();
            var brands = brandRepository.Where<Brands>(a => userBrands.Contains(a.Id));
            return ListResponseBase<Brands>.Success(brands);
        }

        public ResponseBase<Brands> GetRecommendBrand(Users user)
        {
            var userBrands = brandRepository.Where<UserBrands>(a => a.UserId == user.Id).Select(a => a.BrandId).ToList();
            var brand = brandRepository.Where<Brands>(a => !userBrands.Contains(a.Id))
                .OrderBy(a => Guid.NewGuid()).Take(1).FirstOrDefault();
            return ResponseBase<Brands>.Success(brand);
        }

        public ResponseBase LikeBrand(int userId, int brandId, bool liked)
        {
            bool add = true;
            var preLike = brandRepository.Where<BrandLikes>(a => a.UserId == userId && a.BrandId == brandId)
                .FirstOrDefault();
            if (preLike == null)
            {
                var brandLike = new BrandLikes
                {
                    Liked = liked,
                    BrandId = brandId,
                    UserId = userId
                };
                brandRepository.Add(brandLike);
            }
            else
            {
                if (preLike.Liked == liked)
                {
                    add = false;
                }
                else
                {
                    preLike.Liked = liked;
                    brandRepository.Update(preLike);
                }
            }
            if (add)
            {
                var brand = brandRepository.GetById(brandId);
                brand.LikesCount = liked ? brand.LikesCount + 1 : brand.LikesCount - 1;
                Update(brand);
            }
            return ResponseBase.Success();
        }

        public ResponseBase<Brands> UpdateBrand(Brands brand)
        {
            var context = brandRepository.GetDbContext();
            if (!context.Set<Brands>().Any(a => a.Id == brand.Id))
            {
                return ResponseBase<Brands>.Failure(ResponseStatus.NOT_FOUND);
            }

            var preBrand = context.Set<Brands>().Where(a => a.Id == brand.Id)
                .Include(a => a.StyleBrands)
                .Include(a => a.BrandSizes)
                .AsSplitQuery()
                .FirstOrDefault();

            preBrand.Name = string.IsNullOrEmpty(brand.Name) ? preBrand.Name : brand.Name;
            preBrand.Thumbnail = string.IsNullOrEmpty(brand.Thumbnail) ? preBrand.Thumbnail : brand.Thumbnail;
            preBrand.Photos = brand.Photos is null || brand.Photos.Count == 0 ? preBrand.Photos : brand.Photos;
            preBrand.StyleBrands = brand.StyleBrands is null || brand.StyleBrands.Count == 0 ? preBrand.StyleBrands : brand.StyleBrands;
            preBrand.BrandUrl = string.IsNullOrEmpty(brand.BrandUrl) ? preBrand.BrandUrl : brand.BrandUrl;
            preBrand.BrandSizes = brand.BrandSizes is null || brand.BrandSizes.Count == 0 ? preBrand.BrandSizes : brand.BrandSizes;

            context.Set<Brands>().Update(preBrand);
            context.SaveChanges();
            return ResponseBase<Brands>.Success(preBrand);
        }
    }
}

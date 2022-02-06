﻿using Appstagram.Base.Extensions;
using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Mapster;
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

        public ListResponseBase<BrandDto> GetAllBrands()
        {
            TypeAdapterConfig<Brands, BrandDto>
                .NewConfig()
                .Map(dest => dest.Styles, src => src.StyleBrands.Select(a => a.Style).ToList())
                .Map(dest => dest.Inspos, src => src.UserBrands.Select(a => a.User).ToList())
                ;

            var result = brandRepository.GetDbSet()
                .ProjectToType<BrandDto>();

            return ListResponseBase<BrandDto>.Success(result);
        }

        public ResponseBase<BrandDto> GetBrand(Users user, int brandId)
        {
            TypeAdapterConfig<Brands, BrandDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.BrandLikes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.Styles, src => src.StyleBrands.Select(a => a.Style).ToList())
                .Map(dest => dest.Inspos, src => src.UserBrands.Select(a => a.User));

            var result = brandRepository.Where<Brands>(a => a.Id == brandId)
                .ProjectToType<BrandDto>()
                .FirstOrDefault()
                ;

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
            var preLike = brandRepository.Where<BrandLikes>(a => a.UserId == userId)
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
    }
}

using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class BrandQueries
    {
        [GraphQLName("brand_getBrands")]
        public ListResponseBase<Brands> GetBrands(ClaimsPrincipal claims, [Service] IBrandService brandService, List<int> brandIds = null)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Brands>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Brands>.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.GetBrands(brandIds);
        }

        [GraphQLName("brand_recommendBrand")]
        public ResponseBase<Brands> RecommendBrand(ClaimsPrincipal claims, [Service] IBrandService brandService)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Brands>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Brands>.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.GetRecommendBrand(user);
        }

        [GraphQLName("brand_getAllBrands")]
        public ListResponseBase<BrandDto> GetAllBrands(ClaimsPrincipal claims, [Service] IBrandService brandService)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<BrandDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<BrandDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.GetAllBrands();
        }

        [GraphQLName("brand_getBrand")]
        public ResponseBase<BrandDto> GetBrand(ClaimsPrincipal claims, [Service] IBrandService brandService, int brandId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<BrandDto>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<BrandDto>.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.GetBrand(user, brandId);
        }
    }
}

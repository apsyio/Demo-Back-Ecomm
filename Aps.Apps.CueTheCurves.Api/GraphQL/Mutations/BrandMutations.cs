using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Mapster;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class BrandMutations
    {
        [GraphQLName("brand_likeBrand")]
        public ResponseBase LikeBrand(ClaimsPrincipal claims, [Service] IBrandService brandService, int brandId, bool liked = true)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return brandService.LikeBrand(user.Id, brandId, liked);
        }

        [GraphQLName("brand_activeBrand")]
        public ResponseBase ActiveBrand(ClaimsPrincipal claims, [Service] IBrandService brandService, int brandId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            return brandService.ActiveBrand(brandId);
        }

        [GraphQLName("brand_deActiveBrand")]
        public ResponseBase DeActiveBrand(ClaimsPrincipal claims, [Service] IBrandService brandService, int brandId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            return brandService.DeActiveBrand(brandId);
        }

        [GraphQLName("brand_createBrand")]
        public ResponseBase<Brands> CreateBrand(ClaimsPrincipal claims, [Service] IBrandService brandService, BrandInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Brands>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Brands>.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase<Brands>.Failure(ResponseStatus.NOT_ALLOWED);

            TypeAdapterConfig<BrandInput, Brands>
                .NewConfig()
                .Map(dest => dest.StyleBrands, src => src.Styles == null ? null : src.Styles.Select(a => new StyleBrands { StyleId = a }))
                .Map(dest => dest.BrandSizes, src => src.Sizes == null ? null : src.Sizes.Select(a => new BrandSizes { SizeId = a }));

            return brandService.Add(input.Adapt<Brands>());
        }

        [GraphQLName("brand_updateBrand")]
        public ResponseBase<Brands> UpdateBrand(ClaimsPrincipal claims, [Service] IBrandService brandService,BrandInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Brands>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Brands>.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase<Brands>.Failure(ResponseStatus.NOT_ALLOWED);
            if (input.Id is null) return ResponseBase<Brands>.Failure(ResponseStatus.NOT_ENOUGH_DATA);

            TypeAdapterConfig<BrandInput, Brands>
                .NewConfig()
                .Map(dest => dest.StyleBrands, src => src.Styles == null ? null : src.Styles.Select(a => new StyleBrands { StyleId = a }))
                .Map(dest => dest.BrandSizes, src => src.Sizes == null ? null : src.Sizes.Select(a => new BrandSizes { SizeId = a }));

            return brandService.UpdateBrand(input.Adapt<Brands>());
        }
    }
}

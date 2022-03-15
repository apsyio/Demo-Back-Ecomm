using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface IBrandService : IServiceBase<Brands,InputDef>
    {
        ListResponseBase<Brands> GetBrands(List<int> brandIds = null);
        ResponseBase<Brands> GetRecommendBrand(Users user);
        ListResponseBase<BrandDto> GetAllBrands(bool withRemoved = false);
        ResponseBase<BrandDto> GetBrand(Users user, int brandId);
        ListResponseBase<Brands> GetBrands(Users user);
        ResponseBase LikeBrand(int id, int brandId, bool liked);
        ResponseBase ActiveBrand(int brandId);
        ResponseBase DeActiveBrand(int brandId);
        ResponseBase<Brands> UpdateBrand(Brands brands);
    }
}

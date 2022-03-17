using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
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
    public class SizeQueries
    {
        [GraphQLName("sizes_getSizes")]
        public ListResponseBase<Sizes> GetBrands(ClaimsPrincipal claims, [Service] ISizeService sizeService, List<int> brandIds = null)
        {
            if (!claims.Identity.IsAuthenticated) return ListResponseBase<Sizes>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ListResponseBase<Sizes>.Failure(ResponseStatus.USER_NOT_FOUND);

            return sizeService.GetSizes();
        }
    }
}

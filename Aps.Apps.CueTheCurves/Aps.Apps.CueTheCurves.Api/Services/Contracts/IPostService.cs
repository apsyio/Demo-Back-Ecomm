using Appstagram.Base.Generics.Responses;
using Appstagram.Social.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface IPostService : IPostLikeServiceBase<Posts, Users, PostLikes, PostInput>
    {
        ListResponseBase<PostDto> GetStylePosts(Users user, int styleId);
        ListResponseBase<PostDto> GetBrandPosts(Users user, int brandId);
    }
}

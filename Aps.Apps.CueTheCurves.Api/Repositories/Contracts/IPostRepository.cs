using Appstagram.Social.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;

namespace Aps.Apps.CueTheCurves.Api.Repositories.Contracts
{
    public interface IPostRepository : IPostLikeRepositoryBase<Users, PostLikes, Posts>
    {

    }
}

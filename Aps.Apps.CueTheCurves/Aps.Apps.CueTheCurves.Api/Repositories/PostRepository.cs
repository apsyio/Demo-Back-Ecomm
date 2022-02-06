using Appstagram.Social.Repositories;
using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Aps.Apps.CueTheCurves.Api.Repositories
{
    public class PostRepository : PostLikeRepositoryBase<Users, Posts, PostLikes, DataContext>, IPostRepository
    {
        public PostRepository(IDbContextFactory<DataContext> dbContextFactory)
            :base(dbContextFactory)
        {

        }
    }
}

using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Apstagram.Auth.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Aps.Apps.CueTheCurves.Api.Repositories
{
    public class UserRepository : UserRepositoryBase<DataContext,Users>,IUserRepository
    {
        public UserRepository(IDbContextFactory<DataContext> dbContextFactory)
            :base(dbContextFactory)
        {

        }
    }
}

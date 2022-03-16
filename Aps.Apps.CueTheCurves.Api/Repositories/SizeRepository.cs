using Appstagram.Base.Repositories;
using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Aps.Apps.CueTheCurves.Api.Repositories
{
    public class SizeRepository : Repository<Sizes, DataContext>, ISizeRepository
    {
        public SizeRepository(IDbContextFactory<DataContext> dbContextFactory)
            :base(dbContextFactory)
        {

        }
    }
}

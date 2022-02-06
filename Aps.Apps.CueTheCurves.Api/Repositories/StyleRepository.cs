using Appstagram.Base.Repositories;
using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Repositories
{
    public class StyleRepository : Repository<Styles, DataContext>,IStyleRepository
    {
        public StyleRepository(IDbContextFactory<DataContext> contextFactory)
            :base(contextFactory)
        {

        }

        public IQueryable<Styles> GetUserStyles(int userId)
        {
            return GetDbSet<UserStyles>()
                .Where(a => a.UserId == userId)
                .Select(a => a.Style);
        }
    }
}

using Appstagram.Base.Repositories;
using Aps.Apps.CueTheCurves.Api.Data;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Repositories
{
    public class BrandRepository : Repository<Brands, DataContext>, IBrandRepository
    {
        private readonly DataContext context;
        public BrandRepository(IDbContextFactory<DataContext> dbContextFactory)
            :base(dbContextFactory)
        {
            context = dbContextFactory.CreateDbContext();
        }

        public IQueryable<Brands> GetBrands(List<int> brandIds = null)
        {
            return context.Brands.Where(a => brandIds == null || brandIds.Contains(a.Id));
        }
    }
}

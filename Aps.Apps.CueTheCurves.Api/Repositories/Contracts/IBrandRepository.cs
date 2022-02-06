using Appstagram.Base.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Repositories.Contracts
{
    public interface IBrandRepository : IRepository<Brands>
    {
        IQueryable<Brands> GetBrands(List<int> brandIds = null);
    }
}

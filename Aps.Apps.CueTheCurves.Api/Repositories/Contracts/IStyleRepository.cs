using Appstagram.Base.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Repositories.Contracts
{
    public interface IStyleRepository : IRepository<Styles>
    {
        IQueryable<Styles> GetUserStyles(int userId);
    }
}

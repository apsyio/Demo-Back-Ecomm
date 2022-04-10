using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Services.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Apstagram.Auth.Services.Contracts;
using System.Collections.Generic;

namespace Aps.Apps.CueTheCurves.Api.Services.Contracts
{
    public interface IUserService : IUserServiceBase<Users, UserInput>
    {
        ResponseBase SetStyles(List<int> styleIds, Users user);
        ResponseBase SetBrands(List<int> brandIds, Users user);
        ListResponseBase<Users> GetSelectedInspos(Users user);
        ListResponseBase<Users> GetInspos(Users user, bool isCommon, bool isRandom);
        ResponseBase<UserDto> GetInspo(int inspoId);
        ResponseBase DeactiveUser(Users user);
        ResponseBase<StatDto> GetAppStats(Users user);
        ResponseBase ActiveUserAdmin(int userId);
        ResponseBase DeactiveUserAdmin(int userId);
        ResponseBase SetSelectedInspos(List<int> userIds);
    }
}

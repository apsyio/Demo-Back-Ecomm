using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Models.Inputs;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class StyleService : ServiceBase<Styles, InputDef>,IStyleService
    {
        private readonly IStyleRepository styleRepository;
        private readonly IUserRepository userRepository;

        public StyleService(IStyleRepository styleRepository, IUserRepository userRepository)
            :base(styleRepository)
        {
            this.styleRepository = styleRepository;
            this.userRepository = userRepository;
        }

        public ResponseBase<StyleDto> GetStyle(Users user, int styleId)
        {
            TypeAdapterConfig<Styles, StyleDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.StyleLikes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.Inspos, src => src.UserStyles.Select(a => a.User))
                .Map(dest => dest.Brands, src => src.StyleBrands.Select(a => a.Brand))
                ;

            var result = styleRepository.Where<Styles>(a => a.Id == styleId)
                .ProjectToType<StyleDto>()
                .FirstOrDefault();
            return ResponseBase<StyleDto>.Success(result);
        }

        public ListResponseBase<Styles> GetStyles(Users user = null)
        {
            if (user == null) return ListResponseBase<Styles>.Success(styleRepository.GetDbSet());
            return ListResponseBase<Styles>.Success(styleRepository.GetUserStyles(user.Id));
        }

        public ResponseBase LikeStyle(Users user, int styleId, bool liked)
        {
            bool add = true;
            var preLike = styleRepository.Where<StyleLikes>(a => a.UserId == user.Id)
                .FirstOrDefault();
            if(preLike == null)
            {
                var styleLike = new StyleLikes
                {
                    Liked = liked,
                    StyleId = styleId,
                    UserId = user.Id
                };
                styleRepository.Add(styleLike);
            }
            else
            {
                if(preLike.Liked == liked)
                {
                    add = false;
                }
                else
                {
                    preLike.Liked = liked;
                    styleRepository.Update(preLike);
                }
            }
            if (add)
            {
                var style = styleRepository.GetById(styleId);
                style.LikesCount = liked ? style.LikesCount+1 : style.LikesCount-1;
                Update(style);
            }
            return ResponseBase.Success();
        }
    }
}

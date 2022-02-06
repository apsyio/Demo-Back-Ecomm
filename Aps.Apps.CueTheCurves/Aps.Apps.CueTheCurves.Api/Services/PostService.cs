using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Appstagram.Social.Services;
using Aps.Apps.CueTheCurves.Api.Models.Dtos;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Mapster;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class PostService : PostLikeServiceBase<Posts,Users, PostLikes, PostInput>,IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
            :base(postRepository)
        {
            this.postRepository = postRepository;
        }

        public ListResponseBase<PostDto> GetBrandPosts(Users user, int brandId)
        {
            TypeAdapterConfig<Posts, PostDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.Likes != null && src.Likes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.LikesCount, src => src.Likes == null ? 0 : src.Likes.Where(x => x.Liked).Count());

            var result = postRepository.Where<Posts>(a => a.BrandId == brandId)
                .ProjectToType<PostDto>();

            return ListResponseBase<PostDto>.Success(result);
        }

        public ListResponseBase<PostDto> GetStylePosts(Users user, int styleId)
        {
            TypeAdapterConfig<Posts, PostDto>
                .NewConfig()
                .Map(dest => dest.Liked, src => src.Likes != null && src.Likes.Any(x => x.UserId == user.Id && x.Liked))
                .Map(dest => dest.LikesCount, src => src.Likes == null ? 0 : src.Likes.Where(x => x.Liked).Count());

            var result = postRepository.Where<Posts>(a => a.StyleId == styleId)
                .ProjectToType<PostDto>();

            return ListResponseBase<PostDto>.Success(result);
        }

        public override ListResponseBase<PostDto> GetUserPosts<PostDto>(Users user)
        {
            var context = postRepository.GetDbContext();
            var userStyles = postRepository.Where<UserStyles>(a => a.UserId == user.Id, context).Select(a => a.StyleId).ToList();
            var userBrands = postRepository.Where<UserBrands>(a => a.UserId == user.Id, context).Select(a => a.BrandId).ToList();

            TypeAdapterConfig<Posts, PostDto>
                .NewConfig()
                .Map(dest => dest.LikesCount, src => src.Likes != null ? src.Likes.Where(x => x.Liked).Count() : 0)
                .Map(dest => dest.Liked, src => src.Likes != null || src.Likes.Count > 0 ? src.Likes.Any(a => a.UserId == user.Id && a.Liked) : false)
                ;

            var result = postRepository.GetAllPosts()
                .Where(a => userStyles.Contains(a.StyleId ?? 0) || userBrands.Contains(a.BrandId ?? 0))
                .ProjectToType<PostDto>();

            return ListResponseBase<PostDto>.Success(result);
        }

        public override ResponseBase<Posts> Add(PostInput input)
        {
            if(input.StyleId != null && !postRepository.Any<Styles>(a => a.Id == input.StyleId))
            {
                return ResponseBase<Posts>.Failure(ResponseStatus.NOT_FOUND);
            }
            if(input.BrandId != null && !postRepository.Any<Brands>(a => a.Id == input.BrandId))
            {
                return ResponseBase<Posts>.Failure(ResponseStatus.NOT_FOUND);
            }

            return base.Add(input);
        }
    }
}

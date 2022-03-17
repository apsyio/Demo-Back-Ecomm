using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Appstagram.Base.Services;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Services
{
    public class SizeService : ServiceBase<Sizes,SizeInput>, ISizeService
    {
        private readonly ISizeRepository sizeRepository;

        public SizeService(ISizeRepository sizeRepository):
            base(sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public ResponseBase ActivateSize(int sizeId)
        {
            var size = sizeRepository.GetDbSet<Sizes>().IgnoreQueryFilters()
                .Where(a => a.IsDeleted && a.Id == sizeId)
                .FirstOrDefault();
            if (size is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            size.IsDeleted = false;
            var context = sizeRepository.GetDbContext();
            context.Set<Sizes>().Update(size);
            context.SaveChanges();
            return ResponseBase.Success();
        }

        public override ResponseBase<Sizes> Add(Sizes entity)
        {
            if(sizeRepository.GetDbSet<Sizes>().IgnoreQueryFilters()
                .Any(a => a.Size == entity.Size))
            {
                return ResponseBase<Sizes>.Failure(ResponseStatus.ALREADY_EXIST);
            }
            return base.Add(entity);
        }

        public ResponseBase DeactiveSize(int sizeId)
        {
            var size = sizeRepository
                .Where<Sizes>(a => a.Id == sizeId)
                .FirstOrDefault();
            if (size is null)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            size.IsDeleted = true;
            Update(size);
            return ResponseBase.Success();
        }

        public ListResponseBase<Sizes> GetSizes()
        {
            return ListResponseBase<Sizes>.Success(sizeRepository.GetDbSet());
        }
    }
}

using Appstagram.Base.Models.Entities;
using HotChocolate.Types;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.ObjectTypes
{
    public class TaggableObjectType<T> : ObjectType<T>
        where T : TaggableEntity
    {
        protected override void Configure(IObjectTypeDescriptor<T> descriptor)
        {
            base.Configure(descriptor);
        }
    }
}

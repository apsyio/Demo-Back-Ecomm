using Aps.Apps.CueTheCurves.Api.GraphQL.Queries;
using HotChocolate.Types;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.ObjectTypes
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
        }
    }
}

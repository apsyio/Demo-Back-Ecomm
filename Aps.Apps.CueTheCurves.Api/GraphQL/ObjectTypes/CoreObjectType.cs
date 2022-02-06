using Appstagram.Base.Attributes;
using Appstagram.Base.Extensions;
using Appstagram.Base.Models.Entities;
using Appstagram.Base.Repositories.Contracts;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Repositories.Contracts;
using HotChocolate.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.ObjectTypes
{
    public class CoreObjectType<T> : ObjectType<T>
        where T : class, new()
    {
        protected override void Configure(IObjectTypeDescriptor<T> descriptor)
        {
            foreach(var prop in typeof(T).GetProperties())
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach(object attr in attrs)
                {
                    if(attr.GetType() == typeof(TaggableAttribute))
                    {
                        var taggable = (TaggableAttribute)attr;
                        PropertyInfo pi = typeof(T).GetProperty(prop.Name,
                        BindingFlags.Instance |
                         BindingFlags.NonPublic |
                         BindingFlags.Public);
                        
                        descriptor.ExtendsType<T>()
                            .Field(taggable.Name)
                            .Type<ListType<AnyType>>()
                            .Resolve(context =>
                            {
                                var parent = context.Parent<T>();
                                var value = long.Parse(pi.GetValue(parent, null).ToString());
                                IStyleRepository styleRepo = context.Service<IStyleRepository>();
                                Type type = taggable.TaggedEntityType;

                                MethodInfo method = styleRepo.GetType().GetMethod("GetTaggableItems");
                                MethodInfo generic = method.MakeGenericMethod(type);
                                var res = generic.Invoke(styleRepo, new object[] { value }) as IQueryable<object>;
                                //var resJson = JsonConvert.SerializeObject(res);
                                return res.ToList();
                            });
                    }
                }
            }
        }

        private void Process<TT>(IQueryable<IQueryable<TT>> ienumerable)
        {
            Console.WriteLine("Processing type: {0}", typeof(TT).Name);
            foreach (var inner in ienumerable)
                foreach (TT item in inner)
                    Console.WriteLine(item.ToString()); // item is now type T
        }
    }
}

using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Data
{
    public class DataInitializer
    {
        internal static void Initialize(DataContext context, bool v)
        {
            context.Database.Migrate();
            AddStyles(context);
            AddBrands(context);
            AddStyleBrands(context);
        }

        private static void AddStyleBrands(DataContext context)
        {
            if(context.Brands.Any() && context.Styles.Any() && !context.StyleBrands.Any())
            {
                context.StyleBrands.AddRange(new StyleBrands
                {
                    BrandId = 1,
                    StyleId = 1
                }, new StyleBrands
                {
                    BrandId = 2,
                    StyleId = 1
                });
                context.SaveChanges();
            }
        }

        private static void AddBrands(DataContext context)
        {
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(new Brands
                {
                    Name = "Adidas"
                }, new Brands
                {
                    Name = "Nike"
                });
                context.SaveChanges();
            }
        }

        private static void AddStyles(DataContext context)
        {
            if (!context.Styles.Any())
            {
                context.Styles.AddRange(new Styles
                {
                    Name = "Casual"
                }, new Styles
                {
                    Name = "Street Casual"
                }, new Styles
                {
                    Name = "Elegant"
                }, new Styles
                {
                    Name = "Sport"
                });
                context.SaveChanges();
            }
        }
    }
}

using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Data
{
    public class DataInitializer
    {

        internal static void Initialize(DataContext context, bool v)
        {
            context.Database.Migrate();
            //AddStyles(context);
            //AddBrands(context);
        }

        private static void AddBrands(DataContext context)
        {
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(GetBrands(context.Styles.ToList()));
                context.SaveChanges();
            }
        }

        private static List<Brands> GetBrands(List<Styles> styles)
        {

            string[] brandSizes = new string[]
            {
                "00-24","00-28","00-40","0-26 (offers custom as well)","0-32","0-42","0X-4X","12+","12-20 1X-3X","12-22 1X-4X",
                "12-26 1X-3X","12-26 1X-4X","12-26+","12-28 1X-3X","12-30 1X-4X","12-32 1X-3X","12-32 1X-3X","14-22","14-24","14-24",
                "14-24 1X-6X","14-28","14-28","14-28","14-28","14-30 1X-3X","14-32","14-32","14-38","14-40",
                "14-44","16-22","16-28","16-32","18-28","18-28, 44-54 Scandanavian","1X-2X","1X-3X","1X-3X","1X-3X",
                "1X-3X","1X-3X","1X-3X","1X-3X","1X-3X","1X-3X","1X-3X","1X-3X","1X-3X","1X-3X",
                "1X-3X 12-24","1X-3X, 18-22","1X-4X","1X-4X","1X-4X","1X-4X","1X-4X","1X-4X","1X-4X","1X-4X 12-24",
                "1X-4X 16-26","1X-5X","1X-5X","1X-5X","1X-5X","1X-5X","1X-6X","1X-6X","1X-6X","1X-6X",
                "1X-6X","1X-6X","1X-7X","1X-7X 12-30","1X-7X 12-30","band size 30-58, cup sizes B-L","L-3X","L-3XL","L-5XL","S-4X",
                "S-5X","S-6X","X-4X","X-4X","XL - 4X","XL-3X","XL-XXL","XS - 5X","XS-2X","XS-3X",
                "XS-4X","XS-5X","XS-6X","XXS - 5X","XXS-5X","XXS-6X","XXs-7X"
            };

            string[] brandNames = new string[]
            {
                "Good American","ModCloth","Universal Standard","JJ's House","Old Navy",
                "loud bodies","Cupshe","Your Big Sister's Closet","Karen Millen","Mango",
                "Athleta","H & M","Friday Flamingo","New Look","Marisota",
                "Debenhams","Le Redoute","You and All","Peach the Label","Warp + Weft",
                "Rainbow","Day/Won","Eloquii","Eloquii Unlimited","Navabi",
                "Verishop","Dia & Co.","Lady Voluptuous","yours clothing","Sydney's Closet",
                "Swimsuits for All","collusion","Pretty Little Thing","Apples & Pears Clothing","new girl order",
                "Inan Isik","Aerie","Boohoo","Forever 21","Impressions",
                "Savage X Fenty","Parade","Glam Curves","Vintage Grace Boutique","Fringe + Co.",
                "Swim by Elly","Thistle and Spire","Wilde Thing","Anthropolige","Christy Dawn",
                "J Crew","Vince","Beyond Yoga","Fabletics","Target",
                "Aline","Heisis","Just my Size","Nordstrom","Beaton",
                "Chicwe","ayamani design co","Rainbeau Curves","Truly Curvy Boutique","Jessakae",
                "Berriez","ASOS","Girlfriend Collective","Glitzy Girlz Boutique","ullapopken",
                "Cantiq","Macy's","superfit","Simply Be","Topsy Curvy",
                "Glamorise","Nike","Fashion Nova","Shein","Feminine Funk",
                "Rebdolls","shiny by nature","Blue Sky Clothing","Unlucky Lingerie","Rue21",
                "Pink Coconut","Revolve","Nomads Swimwear","gmmrs","Pretty + All Boutique",
                "Adore Me","trash queen","C'EST D","Selkie","Honey's Blowtorch",
                "big bud press","tuesday of california"
            };

            Dictionary<string, string[]> brandStyles = new Dictionary<string, string[]>();
            foreach(var brand in brandNames)
            {
                switch (brand)
                {
                    case "Good American": brandStyles.Add(brand, new string[] { "Streetwear", "Chic" });break;
                    case "Cupshe": brandStyles.Add(brand, new string[] { "Beachy", "Lounge", "Bohemian" }); break;
                    case "ModCloth": brandStyles.Add(brand, new string[] { "Office Wear", "Vintage" }); break;
                    case "Universal Standard": brandStyles.Add(brand, new string[] { "Office Wear"}); break;
                    case "JJ's House": brandStyles.Add(brand, new string[] { "Formal", "Victorian", "Modest", "Southern" }); break;
                    case "Old Navy": brandStyles.Add(brand, new string[] { "Lounge", "Streetwear", "Minimalist" }); break;
                    case "loud bodies": brandStyles.Add(brand, new string[] { "Bohemian", "Camp", "Victorian", "Formal", "Light Academia" }); break;
                    case "Your Big Sister's Closet": brandStyles.Add(brand, new string[] { "Office Wear"}); break;
                    case "Karen Millen": brandStyles.Add(brand, new string[] { "chic", "formal", "Victorian", "preppy" }); break;
                    case "Mango": brandStyles.Add(brand, new string[] { "Boho", "baddie", "minimalist", "chic" }); break;
                    case "Athleta": brandStyles.Add(brand, new string[] { "Lounge", "athletic", "streetwear" }); break;
                    case "H & M": brandStyles.Add(brand, new string[] { "basics", "camp", "Y2K", "eclectic", "baddie" }); break;
                    case "Friday Flamingo": brandStyles.Add(brand, new string[] { "Boho", "Chic", "Country" }); break;
                    case "New Look": brandStyles.Add(brand, new string[] { "Y2K", "camp", "eclectic", "chic", "streetwear", "light academia" }); break;
                    case "Marisota": brandStyles.Add(brand, new string[] { "basics", "swimwear", "lingerie", "modest", "lounge", "formal", "bohemian" }); break;
                    case "Debenhams": brandStyles.Add(brand, new string[] { "chic", "preppy", "formal", "eclectic", "camp" }); break;
                    case "Le Redoute": brandStyles.Add(brand, new string[] { "basics", "chic", "victorian", "light academia" }); break;
                    case "You and All": brandStyles.Add(brand, new string[] { "Bohemian", "Baddie", "Beachy" }); break;
                    case "Peach the Label": brandStyles.Add(brand, new string[] { "Southern", "Bohemian", "Beachy" }); break;
                    case "Warp + Weft": brandStyles.Add(brand, new string[] { "Minimalist", "chic" }); break;
                    case "Rainbow": brandStyles.Add(brand, new string[] { "Baddie", "streetwear", "Y2k" }); break;
                    case "Day/Won": brandStyles.Add(brand, new string[] { "Athletic" }); break;
                    case "Eloquii": brandStyles.Add(brand, new string[] { "Workwear", "Baddie", "Streetwear", "Preppy", "Vintage" }); break;
                    case "Eloquii Unlimited": brandStyles.Add(brand, new string[] { "Workwear", "Baddie", "Streetwear", "Preppy", "Vintage" }); break;
                    case "Navabi": brandStyles.Add(brand, new string[] { "Office Wear", "Preppy" }); break;
                    case "Verishop": brandStyles.Add(brand, new string[] { "eclectic", "camp", "chic" }); break;
                    case "Dia & Co.": brandStyles.Add(brand, new string[] { "Workwear", "Going Out" }); break;
                    case "Lady Voluptuous": brandStyles.Add(brand, new string[] { "eclectic", "victorian", "modest" }); break;
                    case "yours clothing": brandStyles.Add(brand, new string[] { "Streetwear", "Athleisure", "Formal" }); break;
                    case "Sydney's Closet": brandStyles.Add(brand, new string[] { "Formal"}); break;
                    case "Swimsuits for All": brandStyles.Add(brand, new string[] { "Swimwear", "Beachy" }); break;
                    case "collusion": brandStyles.Add(brand, new string[] { "Camp", "Streetwear", "Y2K", "Grunge", "Art Ho" }); break;
                    case "Pretty Little Thing": brandStyles.Add(brand, new string[] { "Streetwear", "Baddie" }); break;
                    case "Apples & Pears Clothing": brandStyles.Add(brand, new string[] { "bohemian", "minimalist", "formal", "lounge" }); break;
                    case "new girl order": brandStyles.Add(brand, new string[] { "Streetwear", "Y2K", "Baddie" }); break;
                    case "Inan Isik": brandStyles.Add(brand, new string[] { "formal", "victorian", "bohemian" });break;
                    case "Aerie": brandStyles.Add(brand, new string[] { "Atheltic", "Streetwear", "Cottagecore", "Swimwear" }); break;
                    case "Boohoo": brandStyles.Add(brand, new string[] { "Streetwear", "Boho", "Baddie", "Y2K", "Swimwear" }); break;
                    case "Forever 21": brandStyles.Add(brand, new string[] { "Streetwear", "Boho", "Baddie", "Y2K", "Swimwear" }); break;
                    case "Impressions": brandStyles.Add(brand, new string[] { "Preppy", "Minimalist", "Boho" }); break;
                    case "Savage X Fenty": brandStyles.Add(brand, new string[] { "Baddie", "Lingerie" }); break;
                    case "Parade": brandStyles.Add(brand, new string[] { "Lingerie", "Lounge" }); break;
                    case "Glam Curves": brandStyles.Add(brand, new string[] { "Baddie", "formal", "chic" }); break;
                    case "Vintage Grace Boutique": brandStyles.Add(brand, new string[] { "Boho", "grunge" }); break;
                    case "Fringe + Co.": brandStyles.Add(brand, new string[] { "ecclectic", "camp" }); break;
                    case "Swim by Elly": brandStyles.Add(brand, new string[] { "swimwear" }); break;
                    case "Thistle and Spire": brandStyles.Add(brand, new string[] { "lingerie" }); break;
                    case "Wilde Thing": brandStyles.Add(brand, new string[] { "baddie", "formal", "Y2K" }); break;
                    case "Anthropolige": brandStyles.Add(brand, new string[] { "boho", "eclectic", "chic" }); break;
                    case "Christy Dawn": brandStyles.Add(brand, new string[] { "boho", "light academia", "Victorian", "preppy" }); break;
                    case "J Crew": brandStyles.Add(brand, new string[] { "basics", "preppy", "victorian", "minimalist", "chic" }); break;
                    case "Vince": brandStyles.Add(brand, new string[] { "Minimalist", "chic" }); break;
                    case "Beyond Yoga": brandStyles.Add(brand, new string[] { "Athleisure", "Lounge", "Chic" }); break;
                    case "Fabletics": brandStyles.Add(brand, new string[] { "Athletic", "Lounge" }); break;
                    case "Target": brandStyles.Add(brand, new string[] { "Office Wear", "Streetwear", "Swimwear" }); break;
                    case "Aline": brandStyles.Add(brand, new string[] { "Lounge", "baddie" }); break;
                    case "Heisis": brandStyles.Add(brand, new string[] { "chic", "minimalist" }); break;
                    case "Just my Size": brandStyles.Add(brand, new string[] { "Lounge", "athletic", "minimalist" }); break;
                    case "Nordstrom": brandStyles.Add(brand, new string[] { "basics", "Formal", "lounge", "athletic", "chic", "baddie", "eclectic", "modest" }); break;
                    case "Beaton": brandStyles.Add(brand, new string[] { "Light academia", "victorian", "boho", "minimalist", "preppy" }); break;
                    case "Chicwe": brandStyles.Add(brand, new string[] { "formal", "Boho", "modest" }); break;
                    case "ayamani design co": brandStyles.Add(brand, new string[] { "Chic", "Streetwear", "Bohemian", "Baddie" }); break;
                    case "Rainbeau Curves": brandStyles.Add(brand, new string[] { "Athleisure" }); break;
                    case "Truly Curvy Boutique": brandStyles.Add(brand, new string[] { "Lounge", "minimalist", "streetwear" }); break;
                    case "Jessakae": brandStyles.Add(brand, new string[] { "Victorian", "boho" }); break;
                    case "Berriez": brandStyles.Add(brand, new string[] { "camp", "camp", "baddie", "Y2K" }); break;
                    case "ASOS": brandStyles.Add(brand, new string[] { "Academia", "Streetwear", "Boho", "Swimwear" }); break;
                    case "Girlfriend Collective": brandStyles.Add(brand, new string[] { "Athletic", "Minimalist" }); break;
                    case "Glitzy Girlz Boutique": brandStyles.Add(brand, new string[] { "Preppy", "Minimalist", "Boho" }); break;
                    case "ullapopken": brandStyles.Add(brand, new string[] { "Office Wear" }); break;
                    case "Cantiq": brandStyles.Add(brand, new string[] { "Lingerie" }); break;
                    case "Macy's": brandStyles.Add(brand, new string[] { "Lounge", "chic", "baddie", "formal", "athletic" }); break;
                    case "superfit": brandStyles.Add(brand, new string[] { "Athletic", "Lounge", "Minimalist" }); break;
                    case "Simply Be": brandStyles.Add(brand, new string[] { "Baddie", "lounge", "streetwear" }); break;
                    case "Topsy Curvy": brandStyles.Add(brand, new string[] { "grunge", "lounge", "y2k", "modest" }); break;
                    case "Glamorise": brandStyles.Add(brand, new string[] { "lingerie" }); break;
                    case "Nike": brandStyles.Add(brand, new string[] { "Athletic" }); break;
                    case "Fashion Nova": brandStyles.Add(brand, new string[] { "Streetwear", "Boho", "Baddie", "Y2K" }); break;
                    case "Shein": brandStyles.Add(brand, new string[] { "Streetwear", "Boho", "Baddie", "Y2K", "Swimwear" }); break;
                    case "Feminine Funk": brandStyles.Add(brand, new string[] { "Lounge", "camp" }); break;
                    case "Rebdolls": brandStyles.Add(brand, new string[] { "Baddie", "Streetwear", "Formal" }); break;
                    case "shiny by nature": brandStyles.Add(brand, new string[] { "Art Ho", "Minimalist", "Camp", "Modest" }); break;
                    case "Blue Sky Clothing": brandStyles.Add(brand, new string[] { "Eclectic", "Chic", "Boho" }); break;
                    case "Unlucky Lingerie": brandStyles.Add(brand, new string[] { "Lingerie" }); break;
                    case "Rue21": brandStyles.Add(brand, new string[] { "Streetwear", "Boho", "Baddie", "Y2K" }); break;
                    case "Pink Coconut": brandStyles.Add(brand, new string[] { "Preppy", "Minimalist", "Boho" }); break;
                    case "Revolve": brandStyles.Add(brand, new string[] { "Streetwear", "Chic", "Baddie" }); break;
                    case "Nomads Swimwear": brandStyles.Add(brand, new string[] { "Swimwear/Beachy" }); break;
                    case "gmmrs": brandStyles.Add(brand, new string[] { "Art Ho", "Camp" }); break;
                    case "Pretty + All Boutique": brandStyles.Add(brand, new string[] { "", "" }); break;
                    case "Adore Me": brandStyles.Add(brand, new string[] { "Lingerie" }); break;
                    case "trash queen": brandStyles.Add(brand, new string[] { "Grunge", "Y2K", "Dark Academia" }); break;
                    case "C'EST D": brandStyles.Add(brand, new string[] { "Streetwear", "Y2K", "Baddie" }); break;
                    case "Selkie": brandStyles.Add(brand, new string[] { "Y2K", "Cottagecore" }); break;
                    case "Honey's Blowtorch": brandStyles.Add(brand, new string[] { "Boho", "Country", "Art Ho" }); break;
                    case "big bud press": brandStyles.Add(brand, new string[] { "Camp", "Lounge", "Beachy", "Minimalist" }); break;
                    case "tuesday of california": brandStyles.Add(brand, new string[] { "Dark Academia", "Grunge" }); break;
                }
            }

            List<Brands> result = new();
            for(int i = 0; i < brandNames.Count(); i++)
            {
                result.Add(new Brands
                {
                    Name = brandNames[i],
                    StyleBrands = GetStyles(styles, brandStyles[brandNames[i]])
                });
            }

            return result;
        }

        private static ICollection<StyleBrands> GetStyles(List<Styles> styles, string[] vs)
        {
            List<string> names = vs.ToList();
            for(int i = 0; i< names.Count; i++)
            {
                names[i] = names[i].ToLower();
            }
            List<StyleBrands> result = new();
            result.AddRange(styles.Where(a => names.Contains(a.Name.ToLower())).Select(a => new StyleBrands { StyleId = a.Id }));
            return result;
        }

        private static void AddStyles(DataContext context)
        {
            if (!context.Styles.Any(a => a.Name == "Chic"))
            {
                context.Styles.AddRange(GetInitStyles());
                context.SaveChanges();
            }
        }

        private static List<Styles> GetInitStyles()
        {
            string[] styleNames = new string[]
            {
                "Chic","Athleisure","Athletic","Streetwear",
                "Preppy","Y2K","Southern","Outdoorsy",
                "Bohemian","Grunge/Goth", "Formal",
                "Dark Academia", "Light Academia",
                "Minimalist","Lounge","Art Ho",
                "Baddie","Camp","Victorian","Swimwear/Beachy",
                "Modest", "Office Wear", "Lingerie"
            };
            List<Styles> styles = new List<Styles>();
            for(int i = 0; i < styleNames.Count(); i++)
            {
                styles.Add(new Styles
                {
                    Name = styleNames[i]
                });
            }
            return styles;
        }
    }
}

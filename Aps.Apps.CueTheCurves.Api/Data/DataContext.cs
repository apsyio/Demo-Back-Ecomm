using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aps.Apps.CueTheCurves.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var valueComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            builder.Entity<Styles>()
                .Property(x => x.Colors)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v))
                .Metadata
                .SetValueComparer(valueComparer);

            builder.Entity<Styles>()
                .Property(x => x.Photos)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v))
                .Metadata
                .SetValueComparer(valueComparer);

            builder.Entity<Brands>()
                .Property(x => x.Photos)
                .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v))
                .Metadata
                .SetValueComparer(valueComparer);

            builder.Entity<PostLikes>()
                .HasOne(a => a.Post)
                .WithMany(a => a.Likes)
                .HasForeignKey(a => a.PostId);

            List<Type> cascadeTypes = new List<Type>
            {
                typeof(UserSocials)
            };

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                if (cascadeTypes.Any(a => a.FullName == relationship.DeclaringEntityType.Name))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Cascade;
                    if (!relationship.IsRequired)
                    {
                        relationship.DeleteBehavior = DeleteBehavior.NoAction;
                    }
                }
                else
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            builder.Entity<Brands>().ToTable(nameof(Brands));
            builder.Entity<Users>().ToTable(nameof(Users));
            builder.Entity<ClosetItems>().ToTable(nameof(ClosetItems));
            builder.Entity<Closets>().ToTable(nameof(Closets));
            builder.Entity<PostLikes>().ToTable(nameof(PostLikes));
            builder.Entity<Sizes>().ToTable(nameof(Sizes));
            builder.Entity<Posts>().ToTable(nameof(Posts));
            builder.Entity<Styles>().ToTable(nameof(Styles));
            builder.Entity<UserSocials>().ToTable(nameof(UserSocials));
            builder.Entity<BrandLikes>().ToTable(nameof(BrandLikes));
            builder.Entity<UserBrands>().ToTable(nameof(UserBrands));
            builder.Entity<StyleBrands>().ToTable(nameof(StyleBrands));
            builder.Entity<UserStyles>().ToTable(nameof(UserStyles));
            builder.Entity<StyleLikes>().ToTable(nameof(StyleLikes));
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<ClosetItems> ClosetItems { get; set; }
        public DbSet<Closets> Closets { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Styles> Styles { get; set; }
        public DbSet<UserSocials> UserSocials { get; set; }
        public DbSet<BrandLikes> BrandLikes { get; set; }
        public DbSet<UserBrands> UserBrands { get; set; }
        public DbSet<StyleBrands> StyleBrands { get; set; }
        public DbSet<UserStyles> UserStyles { get; set; }
        public DbSet<StyleLikes> StyleLikes { get; set; }
    }
}

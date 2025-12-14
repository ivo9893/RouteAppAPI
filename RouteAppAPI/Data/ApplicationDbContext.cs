using Microsoft.EntityFrameworkCore;
using RouteAppAPI.Models;

namespace RouteAppAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Route> Routes { get; set; }
        public DbSet<RoutePoints> RoutePoints { get; set; }
        public DbSet<RoutePhotos> RoutePhotos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SavedRoute> SavedRoutes { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public DbSet<TerrainType> TerrainTypes { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public DbSet<RouteType> RouteTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasIndex(e => e.CreatedAt);

                entity.HasMany(u => u.Routes)
                    .WithOne(r => r.User)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Likes)
                    .WithOne(l => l.User)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Comments)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(u => u.SavedRoutes)
                    .WithOne(sr => sr.User)
                    .HasForeignKey(sr => sr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Collections)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Models.Route>(entity =>
            {
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.DistanceKm);
                entity.HasIndex(e => e.ElevationGainM);
                entity.HasIndex(e => e.RouteTypeId);
                entity.HasIndex(e => e.TerrainTypeId);
                entity.HasIndex(e => e.DifficultyLevelId);
                entity.HasIndex(e => e.IsPublic);
                entity.HasIndex(e => e.Region);
                entity.HasIndex(e => new { e.StartLatitude, e.StartLongitude });

                entity.HasMany(r => r.RoutePoints)
                    .WithOne(r => r.Route)
                    .HasForeignKey(r => r.RouteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.RoutePhotos)
                    .WithOne(r => r.Route)
                    .HasForeignKey(r => r.RouteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.Likes)
                    .WithOne(r => r.Route)
                    .HasForeignKey(r => r.RouteId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(r => r.Comments)
                    .WithOne(c => c.Route)
                    .HasForeignKey(c => c.RouteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.SavedRoutes)
                    .WithOne(sr => sr.Route)
                    .HasForeignKey(sr => sr.RouteId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.RouteType)
                    .WithMany(rt => rt.Routes)
                    .HasForeignKey(r => r.RouteTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.TerrainType)
                    .WithMany(tt => tt.Routes)
                    .HasForeignKey(r => r.TerrainTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.DifficultyLevel)
                    .WithMany(d => d.Routes)
                    .HasForeignKey(r => r.DifficultyLevelId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<RoutePoints>(entity =>
            {
                entity.HasIndex(e => new { e.RouteId, e.SequenceOrder });
            });

            modelBuilder.Entity<RoutePhotos>(entity =>
            {
                entity.HasIndex(e => e.RouteId);
                entity.HasIndex(e => e.UserId);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.RouteId }).IsUnique();

                entity.HasIndex(e => e.RouteId);
                entity.HasIndex(e => e.UserId);
            });


            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.RouteId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.CreatedAt);

                entity.HasOne(c => c.ParentComment)
                    .WithMany(c => c.Replies)
                    .HasForeignKey(c => c.ParentCommentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SavedRoute>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.RouteId }).IsUnique();

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RouteId);
                entity.HasIndex(e => e.CollectionId);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.HasMany(c => c.SavedRoutes)
                    .WithOne(sr => sr.Collection)
                    .HasForeignKey(sr => sr.CollectionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasIndex(e => new { e.FollowerUserId, e.FollowedUserId }).IsUnique();

                entity.HasIndex(e => e.FollowerUserId);
                entity.HasIndex(e => e.FollowedUserId);

                entity.HasOne(f => f.Follower)
                    .WithMany(u => u.Following)
                    .HasForeignKey(f => f.FollowerUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Followed)
                    .WithMany(u => u.Followers)
                    .HasForeignKey(f => f.FollowedUserId)
                    .OnDelete(DeleteBehavior.NoAction);

            });


            modelBuilder.Entity<DifficultyLevel>().HasData(
                new DifficultyLevel
                {
                    Id = 1,
                    Name = "Лесен",
                    Description = "Равен терен, подходящ за начинаещи",
                    SortOrder = 1
                },
                new DifficultyLevel
                {
                    Id = 2,
                    Name = "Умерен",
                    Description = "Леко неравен терен с малко изкачване",
                    SortOrder = 2
                },
                new DifficultyLevel
                {
                    Id = 3,
                    Name = "Среден",
                    Description = "Неравен терен, изисква добра физическа форма",
                    SortOrder = 3
                },
                new DifficultyLevel
                {
                    Id = 4,
                    Name = "Труден",
                    Description = "Стръмни изкачвания, технически участъци",
                    SortOrder = 4
                },
                new DifficultyLevel
                {
                    Id = 5,
                    Name = "Много труден",
                    Description = "Екстремни условия, за опитни бегачи",
                    SortOrder = 5
                }
             );

            // Route Types seed data
            modelBuilder.Entity<RouteType>().HasData(
                new RouteType
                {
                    Id = 1,
                    Name = "Кръгов маршрут",
                    Description = "Започва и завършва на едно и също място"
                },
                new RouteType
                {
                    Id = 2,
                    Name = "Линеен маршрут",
                    Description = "От точка А до точка Б"
                },
                new RouteType
                {
                    Id = 3,
                    Name = "Туристически",
                    Description = "Маркирани туристически пътеки"
                },
                new RouteType
                {
                    Id = 4,
                    Name = "Планински",
                    Description = "Високопланински терен"
                },
                new RouteType
                {
                    Id = 5,
                    Name = "Горски",
                    Description = "Маршрути през гора"
                },
                new RouteType
                {
                    Id = 6,
                    Name = "Крайбрежен",
                    Description = "Покрай морето или река"
                }
            );

            modelBuilder.Entity<TerrainType>().HasData(
                new TerrainType { Id = 1, Name = "Асфалт", Description = "Твърда асфалтова настилка" },
                new TerrainType { Id = 2, Name = "Черен път", Description = "Чакълест или земен път" },
                new TerrainType { Id = 3, Name = "Пътека", Description = "Тясна планинска пътека" },
                new TerrainType { Id = 4, Name = "Скали", Description = "Скалист терен" },
                new TerrainType { Id = 5, Name = "Трева", Description = "Тревиста повърхност" },
                new TerrainType { Id = 6, Name = "Смесен", Description = "Комбинация от различни терени" },
                new TerrainType { Id = 7, Name = "Пясък", Description = "Пясъчна повърхност" }
            );
        }
    }
}

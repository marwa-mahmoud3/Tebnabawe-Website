using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Data
{
    public class TebnabaweContext : IdentityDbContext<ApplicationUser>
    {
        public TebnabaweContext(DbContextOptions<TebnabaweContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Visitores> Visitores { get; set; }
        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Goals> Goals { get; set; }
        public virtual DbSet<Works> Works { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Brochures> Brochures { get; set; }
        public virtual DbSet<IslamicBooks> IslamicBooks { get; set; }
        public virtual DbSet<PhotosLibrary> PhotosLibrary { get; set; }
        public virtual DbSet<VideosChannel> VideosChannels { get; set; }
        public virtual DbSet<VideosLibrary> VideosLibraries { get; set; }
        public virtual DbSet<AudiosChannel> AudiosChannels { get; set; }
        public virtual DbSet<AudiosLibrary> AudiosLibraries { get; set; }
        public virtual DbSet<RadioData> RadioDatas { get; set; }
        public virtual DbSet<RequestRadioPage> RequestRadioPages { get; set; }
    }
}
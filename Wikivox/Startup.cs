using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wikivox.Data;
using Wikivox.Data.Interfaces;
using Wikivox.Service.Services;

namespace Wikivox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(Configuration);
            // build Services _____________________________________________________________

            services.AddScoped<IGenre, GenreService>();
            services.AddScoped<IArtist, ArtistService>();
            services.AddScoped<IAlbum, AlbumService>();
            services.AddScoped<ISong, SongService>();

            services.AddScoped<IMusician, MusicianService>();
            services.AddScoped<IImage, ImageService>();
            services.AddScoped<IEntity,EntityService>();
            services.AddScoped<IInstrument, InstrumentService>();

            services.AddScoped<IAlbumSong, AlbumSongService>();
            services.AddScoped<IArtistGenre, ArtistGenreService>();
            services.AddScoped<IArtistMusician, ArtistMusicianService>();
            services.AddScoped<IArtistSong, ArtistSongService>();
            services.AddScoped<IMusicianInstrument, MusicianInstrumentService>();


            services.AddDbContext<WikivoxDbContext>
                (options =>
                    options.UseSqlServer
                        (Configuration.GetConnectionString("WikivoxConnectionLocal"),
                            b => b.MigrationsAssembly("Wikivox")
                        )
                );
        } 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();
        }
    }
}

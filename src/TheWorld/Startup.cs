using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Framework.Configuration;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            //Para la carga del fichero de configuracion config.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath) //Donde esta el fichero
                .AddJsonFile("config.json");

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            if (_env.IsEnvironment("Development"))
            {
                //Sirve para implementar la interface IMailService en el AppController.
                //services.AddTransient<IMailService, DebugMailService>(); //Crea la instancia cuando sera necesaria y la usa como cache para nuevos request
                services.AddScoped<IMailService, DebugMailService>(); //Crea una instancia nueva para cada request.
                                                                      //services.AddSingleton<IMailService, DebugMailService>(); //Crea una instancia única que se reutilizará
            }
            else
            {
                //services.AddScoped<IMailService, ElServicioReal>();
            }

            services.AddDbContext<WorldContext>();
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddTransient<WorldContextSeedData>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<html><body><h3>Hello World!</h3></body></html>");
            //});

            //app.UseDefaultFiles(); Para usar index.html como página de inicio. En su lugar se usará la vista index.cshtml
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage(); //Para evitar la pantalla en blanco y que muestre el error
            }

            app.UseStaticFiles();

            //Para que haga uso de MVC y así valla a index.cshtml

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" } //Página de inicio por defecto
                    );
            });

            seeder.SeedData().Wait();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using byTourSearch.Services;
using byTourSearch.Services.Interface;
using byTourSearch.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace byTourSearch.WebApi
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
            services.AddMvc(options => options.Filters.Add(new GlobalActionFilter()));
			services.AddTransient<IHotelMappingService, HotelMappingService>();
			services.AddTransient<ITravelAgencyService, TravelAgencyService>();
			services.AddTransient<ICountryService, CountryService>();
			services.AddTransient<IHotelService, HotelService>();
			services.AddTransient<ITourService, TourService>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }
    }
}

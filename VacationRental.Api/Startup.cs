using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Commands;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Application.Rentals.Services;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;
using VacationRental.Infrastructure;

namespace VacationRental.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new OpenApiInfo() { Title = "Vacation rental information", Version = "v1" }));

            services.AddSingleton<IDictionary<int, Rental>>(new Dictionary<int, Rental>());
            services.AddSingleton<IDictionary<int, Booking>>(new Dictionary<int, Booking>());
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUnitAllocatorService, UnitAllocatorService>();
            services.AddMediatR(GetAppAssemblies().ToArray());
            services.AddAutoMapper(GetAppAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static IEnumerable<Assembly> GetAppAssemblies()
        {
            yield return typeof(Startup).Assembly;
            yield return typeof(Rental).Assembly;
            yield return typeof(CreateRentalCommand).Assembly;
            yield return typeof(RentalRepository).Assembly;
        }
    }
}

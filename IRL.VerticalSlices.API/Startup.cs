using IRL.VerticalSlices.API.Configs;
using IRL.VerticalSlices.APP.Common.Database.EntityFramework;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureCreateAccount;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureDeposit;
using IRL.VerticalSlices.APP.Features.FinanceAccounts.FeatureWithdraw;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace IRL.VerticalSlices.API
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
            services.AddDbContext<AppDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:AppContext"));

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(AutoMapperConfiguration.Configure().CreateMapper());
            services.AddTransient<DepositHandlerValidator>();
            services.AddTransient<CreateAccountValidator>();
            services.AddTransient<WithdrawHandlerValidator>();
            services.AddMediatR(AppDomain.CurrentDomain.Load("IRL.VerticalSlices.APP"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IRL.VerticalSlices.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IRL.VerticalSlices.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
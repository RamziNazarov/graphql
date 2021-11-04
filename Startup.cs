using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLTest.Context;
using GraphQLTest.GQL;
using GraphQLTest.Services;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLTest
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(x => x.UseSqlite(_configuration.GetConnectionString("Default")));

            services.AddScoped<Query>();
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddType<ProductType>()
                .AddType<CategoryType>()
                .SetPagingOptions(new PagingOptions
                {
                    IncludeTotalCount = true,
                    DefaultPageSize = 10,
                    MaxPageSize = 100
                });
            // services.AddGraphQL(x => SchemaBuilder
            //     .New()
            //     .AddType<CategoryType>()
            //     .AddType<ProductType>()
            //     .AddQueryType<Query>()
            //     .Create()
            // );
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePlayground(new PlaygroundOptions()
            {
                QueryPath = "/graphql",
                Path = "/Playground"
            });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ASP_NET
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore;

namespace JAN0837_BP.WebApp
{
    public class WebAppStarter
    {
        public static void StartApiServer()
        {
            var builder = Host.CreateDefaultBuilder()  
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:5000");

                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddControllers();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddAuthorization();
                    });

                    webBuilder.Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseSwagger();

                            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
                            app.UseSwaggerUI(c =>
                            {
                                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                                c.RoutePrefix = string.Empty; // Swagger bude dostupný na root URL '/'
                            });
                        }

                        app.UseHttpsRedirection();
                        app.UseRouting();
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });

            builder.Build().Run();
        }
    }
}

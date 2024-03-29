﻿using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.TagHelpers.LayUI;

namespace IsMatch.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {

            return
                Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.ConfigureServices(x =>
                    {
                        x.AddFrameworkService();
                        x.AddLayui();
                        x.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                        });
                    });
                     webBuilder.Configure(x =>
                     {
                         var configs = x.ApplicationServices.GetRequiredService<Configs>();
                         if (configs.IsQuickDebug == true)
                         {
                             x.UseSwagger();
                             x.UseSwaggerUI(c =>
                             {
                                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                             });
                         }
                         x.UseFrameworkService();
                     });
                 }
                 );
        }
    }
}

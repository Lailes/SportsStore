using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SportsStore;

Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
    .UseDefaultServiceProvider(options => options.ValidateScopes = false).Build().Run();

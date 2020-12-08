using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace Angular_Sample.Infrastructure.Extensions
{
    public static class IApplicationBuilderExt
    {
        public static void UseByEnvironment(this IApplicationBuilder app, bool isDevelopemnt)
        {
            if (isDevelopemnt)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
        }

        public static void UseMvcWithRoutes(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }

        public static void UseSpa(this IApplicationBuilder app, bool isDevelopemnt)
        {
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (isDevelopemnt)
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}

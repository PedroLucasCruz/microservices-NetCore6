
namespace GeekShoppingClient.Web.Configurations
{
    public static class WebAppConfig
    {

        public static void AddConfiguration(this IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app,  IWebHostEnvironment env)
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
            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapFallbackToFile("index.html");

            });

        }
    }
}

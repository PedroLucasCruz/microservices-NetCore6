
namespace GeekShoppingClient.Web.Configurations
{
    public static class WebAppConfig
    {
        public static void UseApiConfiguration(this IApplicationBuilder app)
        {

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();



            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();

            //app.UseIdentityConfiguration(); //Configuração do Identity 


            //endPointBuilder.MapControllers();

        }
    }
}

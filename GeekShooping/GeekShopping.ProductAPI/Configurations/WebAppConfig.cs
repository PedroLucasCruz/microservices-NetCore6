
namespace GeekShopping.ProductAPI.Configurations
{
    public static class WebAppConfig
    {
        public static void UseApiConfiguration(this IApplicationBuilder app,  IEndpointRouteBuilder endPointBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityConfiguration(); //Configuração do Identity 

            endPointBuilder.MapControllers();
        }
    }
}

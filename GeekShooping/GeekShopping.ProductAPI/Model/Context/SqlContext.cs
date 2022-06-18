using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(){}
        public SqlContext(DbContextOptions<SqlContext> options) : base(options){}

        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Para rodar esse Seed de dados insert 
            //1 - Abra o Console e selecionar o projeto de Api e execute os comandos a baixo
            //2 - add-migration 'nomeDaSuaMigration'
            //3 - update-dateBase
            //Apos isso basta consultar os registros.
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Camiseta Geek",
                Price = new decimal(73.9),
                Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque",
                CategoryName = "Action Figure",
                ImageUrl = "https://raw.githubusercontent.com/PedroLucasCruz/microservices-NetCore6/main/GeekShooping/Imagens/Joystique.jpg"
                
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "HQ",
                Price = new decimal(11.9),
                Description = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque",
                CategoryName = "Action Figure",
                ImageUrl = "https://raw.githubusercontent.com/PedroLucasCruz/microservices-NetCore6/main/GeekShooping/Imagens/Joystique.jpg"

            });
        }
    }
}

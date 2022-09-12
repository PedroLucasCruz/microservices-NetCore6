using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShoppingApp.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //No construtor é passado DbContextoOption para que seja possivel passar opções vindas da startUp
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;

namespace RedisExampleApp.API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)        
        {        
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(  //has datanın ne oldunu tam anlayamadım sanırım dbye önden veri eklemek için 
                
                new Product()
                {
                    Id = 1, // burda hasdata oldundan idyide girdik...
                    Name= "Ferhat",   
                },
                new Product()
                {
                    Id = 2,
                    Name= "Ali",   
                },
                new Product()
                {
                    Id = 3,
                    Name= "Mehmet",   
                },
                new Product()
                {
                    Id = 4,
                    Name= "Hasan",   
                });   
        }



    }
}

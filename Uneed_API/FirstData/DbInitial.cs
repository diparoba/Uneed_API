using Uneed_API.Models;

namespace Uneed_API.FirstData
{
    public class DbInitial
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            //User Exist Verification
            if (context.User.Any())
            {
                return;
            }
            //Create Service Category
            var srvCategory = new Models.Category[]
          {
                new Models.Category
                {
                    Id= 1,
                    ServiceName = "Plomería",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 2,
                    ServiceName = "Carpintería",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 3,
                    ServiceName = "Electricidad",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 4,
                    ServiceName = "Mecánica",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 5,
                    ServiceName = "Sastrería",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 6,
                    ServiceName = "Computación",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 7,
                    ServiceName = "Medicina",
                    Status = "A"
                },
                new Models.Category
                {
                    Id= 8,
                    ServiceName = "Abogacía",
                    Status = "A"
                }
          };
            foreach (var item in srvCategory)
            {
                context.Category.Add(item);
            }
            context.SaveChanges();
            //Rol Create
            var rols = new Models.Rol[]
            {
                new Models.Rol
                {
                    Id= 1,
                    Description = "Administrator",
                    Status = "A",
                    Add = DateTime.Now
                },
                new Models.Rol
                {
                    Id= 2,
                    Description = "Client",
                    Status = "A",
                    Add = DateTime.Now
                }
            };
            foreach (var rol in rols)
            {
                context.Rol.Add(rol);
            }
            context.SaveChanges();
            //User Create
            var users = new Models.User[]
           {
                new Models.User
                {
                    Id= 1,
                    Name = "Admin",
                    CreatedDate= DateTime.Now,
                    Lastname = "Uneed",
                    Email = "admin@uneed.com",
                    Password= "Admin",
                    Phone="",
                    RolId= 1,
                    Status= "A"

                }
           };
            foreach (var item in users)
            {
                context.User.Add(item);
            }
            context.SaveChanges();
        }

    }
}

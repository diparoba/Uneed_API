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
            var srvCategory = new Models.ServiceCategory[]
          {
                new Models.ServiceCategory
                {
                    Id= 1,
                    ServiceName = "Plomero"
                },
                new Models.ServiceCategory
                {
                    Id= 2,
                    ServiceName = "Carpintero"
                },
                new Models.ServiceCategory
                {
                    Id= 3,
                    ServiceName = "Electricista"
                },
                new Models.ServiceCategory
                {
                    Id= 4,
                    ServiceName = "Plomero"
                }
          };
            foreach (var item in srvCategory)
            {
                context.ServiceCategory.Add(item);
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

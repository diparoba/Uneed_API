using Uneed_API.Models;

namespace Uneed_API.FirstData
{
    public class DbInitial
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
            if (context.User.Any())
            {
                return;
            }
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
                    Description = "Student",
                    Status = "A",
                    Add = DateTime.Now
                }
            };
            foreach (var rol in rols)
            {
                context.Rol.Add(rol);
            }
            context.SaveChanges();

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

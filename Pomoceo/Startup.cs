using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Pomoceo.Models;

[assembly: OwinStartupAttribute(typeof(Pomoceo.Startup))]
namespace Pomoceo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            if (!roleManager.RoleExists("Uzytkownik"))
            {
                var role = new IdentityRole("Uzytkownik");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Admin"))
            {

                //create admin
                var role = new IdentityRole("Admin");
                roleManager.Create(role);

                //create default usr

                var user = new IdentityUser();
                user.UserName = "adm@domain";
                user.Email = "adm@mail.com";
                string pwd = "start1";

                var newUsr = userManager.Create(user, pwd);

                if (newUsr.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }



            }
            else
            {
                //var role = roleManager.FindByName("Admin");
                //roleManager.Delete(role);
                //var user = userManager.FindByEmail("admin@mail.com");
                //userManager.Delete(user);
                //userManager.AddToRole(user.Id, "Admin");
            }

        }
    }
}

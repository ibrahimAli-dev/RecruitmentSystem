using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WazefaOn.Models;

[assembly: OwinStartupAttribute(typeof(WazefaOn.Startup))]
namespace WazefaOn
{
    public partial class Startup
    {
        private ApplicationDbContext db;
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateDefaulRolesAndUsers();
        }
        public void CreateDefaulRolesAndUsers()
        {
            db = new ApplicationDbContext();
            var roleManager = new RoleManager <IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();

            if (!roleManager.RoleExists("Employer")) {
                role.Name = "Employer";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName ="Ibrahim";
                user.Email =  "ibrahimelhamid4@gmail.com";
                var check = userManager.Create(user, "Ibr@him123");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Employer");
                }
            }
        }
    }
}

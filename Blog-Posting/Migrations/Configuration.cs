namespace Blog_Posting.Migrations
{
    using Blog_Posting.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog_Posting.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            // Classes to work with users and roles (provided by Microsoft packages)
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //Check if the roles are already created.
            //If not, create them.
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            //Check if the admin user is already created.
            //If not, create it.
            ApplicationUser adminUser = null;
            if (!context.Users.Any(p => p.UserName == "admin@myblogapp.com"))
            {
                adminUser = new ApplicationUser();
                adminUser.UserName = "admin@myblogapp.com";
                adminUser.Email = "admin@myblogapp.com";
                adminUser.FirstName = "Admin";
                adminUser.LastName = "User";
                adminUser.DisplayName = "Admin User";
                userManager.Create(adminUser, "Password-1");
            }
            else
            {
                adminUser = context.Users.Where(p => p.UserName == "admin@myblogapp.com")
                    .FirstOrDefault();
            }
            //Check if the adminUser is already on the Admin role
            //If not, add it.
            if (!userManager.IsInRole(adminUser.Id, "Admin"))
            {
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            ApplicationUser moderatorUser = null;
            //Check if the moderator user is already created.
            //If not, create it.
            if (!context.Users.Any(p => p.UserName == "shital123@yahoo.com"))
            {
                moderatorUser = new ApplicationUser();
                moderatorUser.UserName = "shital123@yahoo.com";
                moderatorUser.Email = "shital123@yahoo.com";
                moderatorUser.FirstName = "shital";
                moderatorUser.LastName = "bhutiya";
                moderatorUser.DisplayName = "Moderator User";
                userManager.Create(moderatorUser, "Password-1");
            }
            else
            {
                moderatorUser = context.Users.Where(p => p.UserName == "shital123@yahoo.com")
                    .FirstOrDefault();
            }
            //Check if the Moderator user is already on the Admin role
            //If not, add it.
            if (!userManager.IsInRole(moderatorUser.Id, "Moderator"))
            {
                userManager.AddToRole(moderatorUser.Id, "Moderator");
            }
        }
    }
}


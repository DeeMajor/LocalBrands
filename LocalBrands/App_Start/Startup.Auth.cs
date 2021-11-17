using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using LocalBrands.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace LocalBrands
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            CreateRolesAdmin();
        }
        public void CreateRolesAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //creating role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            Admin ad = context.Admins.ToList().Find(x => x.Email == "Admin@gmail.com");
            if (ad == null)
            {
                Admin newAdmin = new Admin
                {
                    AdminID = Guid.NewGuid().ToString(),
                    FirstName = "Administrator",
                    LastName = "Admin",
                    Email = "Admin@gmail.com",

                };
                context.Admins.Add(newAdmin);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newAdmin.Email;
                user.Email = newAdmin.Email;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Admin");
            }
           
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            BrandOwner bo = context.BrandOwners.ToList().Find(x => x.BrandOwnerEmail == "Jerry@gmail.com");
            if (bo == null)
            {
                BrandOwner newBO = new BrandOwner
                {
                    EmployeeId = Guid.NewGuid().ToString(),
                    BrandOwnerName = "Jerry",
                    BrandOwnerSurname = "Smith",
                    BrandOwnerEmail = "Jerry@gmail.com",

                };
                context.BrandOwners.Add(newBO);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newBO.BrandOwnerEmail;
                user.Email = newBO.BrandOwnerEmail;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Employee");
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
            Customer cus = context.Customers.ToList().Find(x => x.Email == "Andile@gmail.com");
            if (cus == null)
            {
                Customer newCus = new Customer
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    FirstName = "Andile",
                    LastName = "Dumakude",
                    Email = "Andile@gmail.com",

                };
                context.Customers.Add(newCus);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newCus.Email;
                user.Email = newCus.Email;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Customer");
            }
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }
            Driver dr = context.Drivers.ToList().Find(x => x.Email == "andilebshange@gmail.com");
            if (dr == null)
            {
                Driver newCus = new Driver
                {
                    DriverID = Guid.NewGuid().ToString(),
                    FirstName = "Sma",
                    LastName = "Dolly",
                    Email = "andilebshange@gmail.com",

                };
                context.Drivers.Add(newCus);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newCus.Email;
                user.Email = newCus.Email;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Driver");
            }
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }
            Driver dr2 = context.Drivers.ToList().Find(x => x.Email == "andilebshange2@gmail.com");
            if (dr2 == null)
            {
                Driver newCus = new Driver
                {
                    DriverID = Guid.NewGuid().ToString(),
                    FirstName = "Peter",
                    LastName = "Parker",
                    Email = "andilebshange2@gmail.com",

                };
                context.Drivers.Add(newCus);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newCus.Email;
                user.Email = newCus.Email;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Driver");
            }
            Driver dr3 = context.Drivers.ToList().Find(x => x.Email == "andilebshange3@gmail.com");
            if (dr3 == null)
            {
                Driver newCus = new Driver
                {
                    DriverID = Guid.NewGuid().ToString(),
                    FirstName = "Percy",
                    LastName = "Tau",
                    Email = "andilebshange2@gmail.com",

                };
                context.Drivers.Add(newCus);
                context.SaveChanges();

                var user = new ApplicationUser();
                user.UserName = newCus.Email;
                user.Email = newCus.Email;
                string password = "Password@12";

                var User = userManager.Create(user, password);
                if (User.Succeeded)
                    userManager.AddToRole(user.Id, "Driver");
            }
        }
    }
}
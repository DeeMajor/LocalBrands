using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LocalBrands.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LocalBrands.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.BrandOwner> BrandOwners { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Department> Departments { get; set; }
        public System.Data.Entity.DbSet<LocalBrands.Models.Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.SubscriptionPrice> SubscriptionPrices { get; set; }
        //public System.Data.Entity.DbSet<LocalBrands.Models.Driver> Drivers { get; set; }
        public System.Data.Entity.DbSet<LocalBrands.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<LocalBrands.Models.Order_Item> Order_Items { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Shipping_Address> Shipping_Addresses { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Affiliate> Affiliates { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Affiliate_Joiner> Affiliate_Joiners { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Deposit> Deposits{ get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.WithdrawLevel> WithdrawLevels { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Withdraw> Withdraws { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Order_Tracking> Order_Trackings { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Cart_Item> Cart_Items { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Payment> Payments { get; set; }

        public System.Data.Entity.DbSet<LocalBrands.Models.Driver> Drivers { get; set; }

    }
}
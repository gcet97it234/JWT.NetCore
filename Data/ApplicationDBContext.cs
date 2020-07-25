using JWT.Utilities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.NetCore.Data
{
    public class ApplicationDBContext :IdentityDbContext<UserInformation, ApplicationRoles, Guid>
    {
        public DbSet<CompanyInfo> CompanyInfo { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<UserInformation>(b =>
            {
                //// Each User can have many UserClaims
                //b.HasMany(e => e.Claims)
                //    .WithOne()
                //    .HasForeignKey(uc => uc.UserId)
                //    .IsRequired();

                //// Each User can have many UserLogins
                //b.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<CompanyInfo>()
                .HasData(

                new CompanyInfo
                {
                     ID = 1,
                      CompanyKey = "ABC",
                       Name = "ABC LLC",
                        LocationKey ="123" 
                });

            modelBuilder.Entity<UserInformation>()
               .HasData(
                   new UserInformation
                   {
                       Id = Guid.NewGuid(),
                       Email = "1.test@test.com",
                       UserName = "1test",
                        CompanyId = 1,
                         EmailConfirmed = true,
                          PasswordHash ="123456",
                           PhoneNumber = "12345678990"
                   },
                   new UserInformation
                   {
                       Id = Guid.NewGuid(),
                       Email = "2.test@test.com",
                       UserName = "2test",
                       CompanyId = 1,
                       EmailConfirmed = true,
                       PasswordHash = "123456",
                       PhoneNumber = "12345678990"
                   }
               );

            modelBuilder.Entity<ApplicationRoles>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

    }
    
}

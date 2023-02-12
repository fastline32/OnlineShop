﻿using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, string,
        IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>             
    {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                    .HasMany(ur => ur.UserRole)
                    .WithOne(u => u.AppUser)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                builder.Entity<AppRole>()
                    .HasMany(ur => ur.UserRole)
                    .WithOne(u => u.AppRole)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
        }
    }
}
using ByteBanter.Data;
using ByteBanter.Enums;
using ByteBanter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ByteBanter.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext? _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext? dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            //Task: Create the DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            //TASK 1. Seed a few Roles into the system
            await SeedRolesAsync();

            //TASK 2. Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }

            //Otherwise, we want to create a few Roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //I need to use the Role Manager to create Roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            //If there are already Users in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Step 1: Creates new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "0123phillips@gmail.com",
                UserName = "0123phillips@gmail.com",
                FirstName = "Jacob",
                LastName = "Phillips",
                DisplayName = "Jacob",
                EmailConfirmed = true
            };

            //Step 2: Use the User Manager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            //Step 3: Add new user to the Administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            //Step 1: Create the moderator user
            var modUser = new BlogUser()
            {
                Email = "joshphillipsdesign@gmail.com",
                UserName = "joshphillipsdesign@gmail.com",
                FirstName = "Josh",
                LastName = "Phililips",
                DisplayName = "Josh",
                EmailConfirmed = true
            };

            //Step 2: Use the User Manager to create a new user that is defined by modUser
            await _userManager.CreateAsync(modUser, "Abc&123!");

            //Step 3: Add new user to the Moderator role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }
    }
}

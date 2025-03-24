using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager_App.Data;

namespace TaskManager_App.Models
{
    public class seedData
    {
        //public static void Initialize(IServiceProvider serviceProvider)
        //{
        //    using (var context = new TaskManager_AppContext(
        //        serviceProvider.GetRequiredService<
        //            DbContextOptions<TaskManager_AppContext>>()))
        //    {
        //        // Look for any movies.
        //        if (context.TaskItem.Any())
        //        {
        //            return;   // DB has been seeded
        //        }
        //        context.TaskItem.AddRange(
        //            new TaskItem
        //            {
        //                Title = "Develop Login Page",
        //                Description = "Create a user login page with email and password authentication using React and Firebase."
        //            },
        //            new TaskItem
        //            {
        //                Title = "Design Database Schema",
        //                Description = "Define the database schema for user management, including tables for users, roles, and permissions in MS SQL."
        //            },
        //            new TaskItem
        //            {
        //                Title = "Implement Payment Gateway",
        //                Description = "Integrate Stripe payment gateway for processing user transactions securely."
        //            },
        //            new TaskItem
        //            {
        //                Title = "Write API Documentation",
        //                Description = "Document all REST API endpoints using Swagger for better developer reference."
        //            }
        //        );
        //        context.SaveChanges();
        //    }
        //}


        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskManager_AppContext(
                serviceProvider.GetRequiredService<DbContextOptions<TaskManager_AppContext>>()))
            {
                // Ensure the database is created
                await context.Database.EnsureCreatedAsync();

                // Add Task Items (if not already added)
                if (!context.TaskItem.Any())
                {
                    context.TaskItem.AddRange(
                        new TaskItem
                        {
                            Title = "Develop Login Page",
                            Description = "Create a user login page with email and password authentication using React and Firebase."
                        },
                        new TaskItem
                        {
                            Title = "Design Database Schema",
                            Description = "Define the database schema for user management, including tables for users, roles, and permissions in MS SQL."
                        },
                        new TaskItem
                        {
                            Title = "Implement Payment Gateway",
                            Description = "Integrate Stripe payment gateway for processing user transactions securely."
                        },
                        new TaskItem
                        {
                            Title = "Write API Documentation",
                            Description = "Document all REST API endpoints using Swagger for better developer reference."
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }

            // Add Users & Roles
            await AddUsersAndRoles(serviceProvider);
        }

        private static async Task AddUsersAndRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles exist, if not, create them
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create an admin user if not exists
            string adminEmail = "admin@taskmanager.com";
            string adminPassword = "Admin@123"; // Change this in production!

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Set to true for skipping email verification
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager_App.Data;

namespace TaskManager_App.Models
{
    public class seedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TaskManager_AppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TaskManager_AppContext>>()))
            {
                // Look for any movies.
                if (context.TaskItem.Any())
                {
                    return;   // DB has been seeded
                }
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
                context.SaveChanges();
            }
        }

    }
}

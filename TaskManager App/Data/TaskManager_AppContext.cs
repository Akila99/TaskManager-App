using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager_App.Models;

namespace TaskManager_App.Data
{
    public class TaskManager_AppContext : DbContext
    {
        public TaskManager_AppContext (DbContextOptions<TaskManager_AppContext> options)
            : base(options)
        {
        }

        public DbSet<TaskManager_App.Models.TaskItem> TaskItem { get; set; } = default!;
    }
}

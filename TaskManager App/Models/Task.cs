using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_App.Models
{
    public class TaskItem
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string? Description { get; set; }
        //[Display(Name = "Release Date")]
        //[DataType(DataType.Date)]
        //[Column(TypeName = "decimal(18, 2)")]
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models.Enums;

namespace TimeTracker.Models
{
    // Note: doesn't expose events or behavior
    public class ToDoItemDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public TimeSpan ElapsedTime { get; set; }


        public static ToDoItemDTO FromToDoItem(ToDoItem item)
        {
            return new ToDoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Priority = item.Priority,
                Status = item.Status,
                ElapsedTime = item.ElapsedTime
            };
        }
    }
}

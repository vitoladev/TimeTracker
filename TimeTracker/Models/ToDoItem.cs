using System;
using System.ComponentModel.DataAnnotations;
using TimeTracker.Dependencies;
using TimeTracker.Models.Enums;

namespace TimeTracker.Models
{
    public class ToDoItem : BaseModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        [Display(Name = "Elapsed Time")]
        public TimeSpan ElapsedTime { get; set; }

        public void StartTask() => Status = (Status)1;
        public void StopTask() => Status = (Status)2;
        public void FinishTask() => Status = (Status)3;
        public void AddTime(TimeSpan time) => ElapsedTime = ElapsedTime.Add(time);

    }
}

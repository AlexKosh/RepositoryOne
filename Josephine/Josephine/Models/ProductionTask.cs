using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class ProductionTask
    {
        [Key]
        public int TaskId { get; set; }                
        public int isCompleted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }

        public ProductionTask()
        {
            this.TaskItems = new List<TaskItem>();
        }
        //public ProductionTask(int id, int isCompl, DateTime sTime, DateTime fTime, int priority, List<TaskItem> items)
        //{
        //    this.TaskId = id;            
        //    this.isCompleted = isCompl;
        //    this.StartTime = sTime;
        //    this.FinishTime = fTime;
        //    this.Priority = priority;

        //    this.TaskItems = items;
        //}
    }
}
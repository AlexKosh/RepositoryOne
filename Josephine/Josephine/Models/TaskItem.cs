using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskItemId { get; set; }
        public int Quantity { get; set; }
        public int TaskId { get; set; }
        public int ItemId { get; set; }
    }
}
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
        public int Id { get; set; }
        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Key, Column(Order = 1)]
        public int ItemCategory { get; set; }
        public int RecipeCategory { get; set; }
        public int Quantity { get; set; }
        public int isCompleted { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int Priority { get; set; }

        public ProductionTask()
        {

        }
        public ProductionTask(int id, int itemId, int itemCat, int recipeCat, int quantity, 
            int isCompl, DateTime sTime, DateTime fTime, int priority)
        {
            this.Id = id;
            this.ItemId = itemId;
            this.ItemCategory = itemCat;
            this.RecipeCategory = recipeCat;
            this.Quantity = quantity;
            this.isCompleted = isCompleted;
            this.StartTime = sTime;
            this.FinishTime = fTime;
            this.Priority = priority;
        }
    }
}
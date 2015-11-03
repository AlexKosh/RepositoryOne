using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class RecipeItem
    {
        [Key]
        public int Id { get; set; }
        
        public int RecipeId { get; set; }          
        public int ItemId { get; set; }
        public int ItemCategory { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitsOfMeasurement { get; set; }

        //public virtual Recipe Recipe { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }          
        public int RecipeCategory { get; set; }
        public string Name { get; set; }
        public int ResultItemId { get; set; }
        public string ResultName { get; set; }
        public int ResultQuantity { get; set; }
        public string UnitsOfMeasurement { get; set; }

        public virtual ICollection<RecipeItem> RecipeItems { get; set; }

        public Recipe()
        {
            this.RecipeItems = new List<RecipeItem>();
        }
        
    }
}
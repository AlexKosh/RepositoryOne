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
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitsOfMeasurement { get; set; }

        public Recipe()
        {

        }
        public Recipe(int itemId, int categoryId, string recipeName, int quantity, string unitsOfMeasurement)
        {
            this.ItemId = itemId;
            this.CategoryId = categoryId;
            this.Name = recipeName;
            this.Quantity = quantity;
            this.UnitsOfMeasurement = unitsOfMeasurement;
        }
    }
}
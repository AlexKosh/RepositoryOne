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
        public int ItemCategory { get; set; }
        public int RecipeCategory { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitsOfMeasurement { get; set; }

        public Recipe()
        {

        }
        public Recipe(int itemId, int itemCategory, int recipeCategory, string recipeName, int quantity, string unitsOfMeasurement)
        {
            this.ItemId = itemId;
            this.ItemCategory = itemCategory;
            this.RecipeCategory = recipeCategory;
            this.Name = recipeName;
            this.Quantity = quantity;
            this.UnitsOfMeasurement = unitsOfMeasurement;
        }
    }
}
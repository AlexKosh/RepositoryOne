using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Models
{
    public class MainWarehouse
    {
        public MainWarehouse(){ }
        public MainWarehouse(int cat, string name, string color, int quan, string measurement)
        {
            this.CategoryId = cat;
            this.Name = name;
            this.Color = color;
            this.Quantity = quan;
            this.UnitOfMeasurement = measurement;
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }

    }
}
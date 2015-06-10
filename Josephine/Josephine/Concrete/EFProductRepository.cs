using Josephine.Abstract;
using Josephine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Josephine.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public void populate()
        {
            int[] sizes = { 44, 46, 48, 50, 52, 54 };
            string[] names = { "Vika", "Vera", "Nadya" };
            int[] modelNumbers = { 417, 423, 431 };
            string[] colors = { "Blue", "Black", "White", "Red" };

            Product newJacket = new Product();
            int i = 0;

            foreach (var modelNumber in modelNumbers)
            {                
                newJacket.ModelNumber = modelNumber;
                newJacket.Name = names[i];

                foreach (var color in colors)
                {
                    newJacket.Color = color;

                    foreach (var size in sizes)
                    {
                        newJacket.Size = size;
                        newJacket.Quantity = 0;
                        context.Products.Add(newJacket);
                        context.SaveChanges();
                    }
                }
                i++;
            }
            
        }
    }
}
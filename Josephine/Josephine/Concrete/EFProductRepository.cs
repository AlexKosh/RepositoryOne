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

        public void populate(int mN, string n, string[] c, int[] s)
        {
            //int[] sizes = { 44, 46, 48, 50, 52, 54 };
            //string name = "Vika";
            //int modelNumber = 417;
            //string[] colors = { "Blue", "Black", "White", "Red" };

            int[] sizes = s;
            string name = n;
            int modelNumber = mN;
            string[] colors = c;

            Product newJacket = new Product();
            int i = 0;

            newJacket.Name = name;
            newJacket.ModelNumber = modelNumber;

            foreach (string color in colors)
            {
                foreach (int size in sizes)
                {
                    newJacket.Color = color;
                    newJacket.Size = size;
                    newJacket.Quantity = 0;

                    context.Products.Add(newJacket);
                    context.SaveChanges();
                }
            }
        }
    }
}
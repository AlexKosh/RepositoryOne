using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox.Abstract
{
    public interface IItem
    {
        [Key]
        int ItemID { get; set; }
        string Name { get; set; }
        int ItemNumber { get; set; }
        string Color { get; set; }
        int Size { get; set; }
        int Price { get; set; }
        int Quantity { get; set; }     
    }
}

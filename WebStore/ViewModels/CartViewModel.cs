using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
	    public IEnumerable<(ProductViewModel Product, int Amount)> Items { get; set; }

       public int TotalAmount => Items?.Sum(cort => cort.Amount) ?? 0;

       public decimal TotalPrice => Items?.Sum(cort => cort.Amount * cort.Product.Price) ?? 0m;
    }
}

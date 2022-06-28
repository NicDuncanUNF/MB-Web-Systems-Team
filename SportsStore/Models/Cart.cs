using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        
        public virtual decimal ComputeShipping()
        {
            //Catch if the line collection is empty as there will be no products selected.
            if (lineCollection.Count == 0)
            {
                return 0;
            }
            //If the total is in between 0 and 35,
            if(ComputeTotalValue() < 35 && ComputeTotalValue() > 0)
            {
                return 10;
            }
            // If all other conditions are passed, the total for shipping will be 0
            else
            {
                return 0;
            }
            
        }
        public virtual decimal ComputeTax()
        {
            //Multiplies the total by 7% to get the sales taxes, then returns it as a decimal type
            double sub = ((double)ComputeTotalValue());
            sub = sub * 0.07;
            return ((decimal)sub); 
        }
        public virtual decimal ComputeAbsoluteTotal()
        {
            //Adds the sub total value with shipping and tax computations
            return ComputeTotalValue() + ComputeShipping() + ComputeTax();
        }
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

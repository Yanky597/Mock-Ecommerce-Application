using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class ProductOperations
    {
        static DataClasses1DataContext databaseConnection;

        public static List<String> getListOfProductNames()
        {
            return databaseConnection.PRODUCTs.Select(product => product.PRO_NAME).ToList();
        }

        public static decimal getProductPrice(String productID) 
        {
            return databaseConnection.PRODUCTs
                .Where(product => product.PRO_ID == productID)
                .Select(product => product.PRO_SALE_P).First();
        }

        public static decimal getCartTotal(Dictionary<String, int> shoppingCart) 
        {
            decimal total = 0;
            decimal price;
            foreach (var item in shoppingCart) 
            {
                // get a reference to the item in the cart by getting its ID
                price = getProductPrice(item.Key);
                // total accumulates the price of the item * quantity
                total += price * item.Value;
            }

            return total;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class PurchaseOperations
    {
        static DataClasses1DataContext databaseConnection;

        public static decimal currentCustomersBalance(String customerID)
        {
            return databaseConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == customerID)
                .Select(customer => customer.CUS_BALANCE).First();
        }

        public static void addItemToCart(String productId, Dictionary<String, int> shoppingCart) 
        {
            shoppingCart[productId] += 1;
        }

        public static bool userCanMakePurchase(String userID, decimal ShoppingCartTotal) 
        {
            return currentCustomersBalance(userID) >= ShoppingCartTotal;
        }
    }
}
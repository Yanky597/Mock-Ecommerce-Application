using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class PurchaseOperations
    {
        public static DataClasses1DataContext dbConnection;

        public static decimal currentCustomersBalance(String customerID)
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == customerID)
                .Select(customer => customer.CUS_BALANCE).First();
        }

        public static void addItemToCart(String productId, Dictionary<String, int> shoppingCart)
        {
            if (shoppingCart.ContainsKey(productId)) 
            {
                shoppingCart[productId] += 1;
                return;
            }

            shoppingCart.Add(productId, 1);
        }

        public static void deleteItemFromCart(String productId, Dictionary<String, int> shoppingCart)
        {
            // if the shopping cart item has a quantity > 0
            // remove one element from the cart.
            // else delete the item from the cart
            // if quantity > 1, then after deleting a product there will be at least 1 product left

            if (shoppingCart[productId] > 1) 
            {
                shoppingCart[productId] -= 1;
            }          
       
            shoppingCart.Remove(productId);
        }

        public static bool userCanMakePurchase(String userID, decimal ShoppingCartTotal)
        {
            return currentCustomersBalance(userID) >= ShoppingCartTotal;
        }

        public static void submitOrder(String userID, Dictionary<String, int> shoppingCart)
        {

            if (userCanMakePurchase(userID, ProductOperations.getCartTotal(shoppingCart))) 
            {
                String invoiceID = generateAndGetUniqueId();
                updateUserAccountBalance(userID, ProductOperations.getCartTotal(shoppingCart));
                createInvoice(userID, invoiceID, shoppingCart);
                createInvoiceRows(invoiceID, shoppingCart);
            }
        }

        public static void updateUserAccountBalance(String userID, decimal shoppingCartTotal)
        {
            dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == userID)
                .First().CUS_BALANCE -= shoppingCartTotal;
        }

        public static void createInvoice(String customerID, String invoiceID, Dictionary<String, int> shoppingCart) 
        {
            INVOICE createInvoice = new INVOICE()
            {
                INV_ID = invoiceID,
                CUS_ID = customerID,
                EMP_ID = null,
                INV_DATE = new DateTime(),
                INV_REFCODE = "PROMO123",
                INV_TOTAL = ProductOperations.getCartTotal(shoppingCart),
            };
            dbConnection.INVOICEs.InsertOnSubmit(createInvoice);
            dbConnection.SubmitChanges();
        }

        public static void createInvoiceRows(String invoiceID, Dictionary<String, int> shoppingCart) 
        {
            INVOICE_ROW invoiceRow;

            foreach (var product in shoppingCart) 
            {
                decimal productPrice = ProductOperations.getProductPrice(product.Key);
                invoiceRow = new INVOICE_ROW()
                {
                    INV_ID = invoiceID,
                    PRO_ID = product.Key,
                    INR_PRICE = productPrice,
                    INR_QUANTITY = product.Value,
                    INR_DISCOUNT = 0,
                    INR_FINAL_PRICE = productPrice * product.Value
                };
                dbConnection.INVOICE_ROWs.InsertOnSubmit(invoiceRow);
                dbConnection.SubmitChanges();
            }
        }


        public static String generateAndGetUniqueId() 
        {

            // gets the highest generated invoice Id and increments it by one
            return (Convert.ToInt32(dbConnection.INVOICEs
                .OrderByDescending(row => row.INV_ID).Select(row => row.INV_ID).First()) + 1).ToString();
            
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class PurchaseOperations
    {

        public static decimal BALANCE_DUE_LIMIT = 500;
        public static DataClasses1DataContext dbConnection;

        public static CUSTOMER_BALANCE getCurrentCustomer(String customerID)
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == customerID)
                .First();
        }

        public static bool payBalanceDue(String customerID, decimal amountToBePaid)
        {
            CUSTOMER_BALANCE currentCustomer = getCurrentCustomer(customerID);

            if (currentCustomer.BALANCE_DUE == null) 
            {
                currentCustomer.BALANCE_DUE = 0;
            }

            if (amountToBePaid <= getCurrentCustomerBalance(customerID))
            {
                currentCustomer.CUS_BALANCE = currentCustomer.CUS_BALANCE - amountToBePaid;
                currentCustomer.BALANCE_DUE = currentCustomer.BALANCE_DUE - amountToBePaid;
                dbConnection.SubmitChanges();
                return true;
            }

            return false;
        }

        public static decimal? getCurrentCustomerBalanceDue(String customerID)
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == customerID)
                .First().BALANCE_DUE;
        }

        public static decimal? getCurrentCustomerBalance(String customerID)
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == customerID)
                .First().CUS_BALANCE;
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
            if (shoppingCart.ContainsKey(productId))
            {
                if (shoppingCart[productId] > 1)
                {
                    shoppingCart[productId] -= 1;
                }
                else
                {
                    shoppingCart.Remove(productId);
                }
            }


        }

        public static bool userCanMakePurchase(String userID, decimal ShoppingCartTotal)
        {

            if (getCurrentCustomer(userID).BALANCE_DUE == null)
            {
                getCurrentCustomer(userID).BALANCE_DUE = 0;
                dbConnection.SubmitChanges();
            }

            return (getCurrentCustomer(userID).BALANCE_DUE + ShoppingCartTotal) <= BALANCE_DUE_LIMIT;
        }

        public static bool submitOrder(String userID, Dictionary<String, int> shoppingCart)
        {

            if (userCanMakePurchase(userID, ProductOperations.getCartTotal(shoppingCart)) && shoppingCart.Count > 0)
            {
                String invoiceID = generateAndGetUniqueId();
                createInvoice(userID, invoiceID, shoppingCart);
                createInvoiceRows(invoiceID, shoppingCart);
                AddShoppingCartTotalToBalanceDue(userID, ProductOperations.getCartTotal(shoppingCart));
                return true;
            }

            return false;
        }

        public static void AddShoppingCartTotalToBalanceDue(String userID, decimal shoppingCartTotal)
        {

            CUSTOMER_BALANCE currentUser = dbConnection.CUSTOMER_BALANCEs
                 .Where(customer => customer.CUS_ID == userID)
                 .First();

            currentUser.BALANCE_DUE = currentUser.BALANCE_DUE + shoppingCartTotal;

            dbConnection.SubmitChanges();

        }

        public static void createInvoice(String customerID, String invoiceID, Dictionary<String, int> shoppingCart)
        {
            INVOICE createInvoice = new INVOICE()
            {
                INV_ID = invoiceID,
                CUS_ID = customerID,
                EMP_ID = null,
                INV_DATE = DateTime.Now,
                INV_REFCODE = "PROMO123",
                INV_TOTAL = ProductOperations.getCartTotal(shoppingCart),
            };
            dbConnection.INVOICEs.InsertOnSubmit(createInvoice);
            dbConnection.SubmitChanges();
        }

        public static void createInvoiceRows(String invoiceID, Dictionary<String, int> shoppingCart)
        {
            INVOICE_ROW invoiceRow = new INVOICE_ROW();

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
                .OrderByDescending(row => row.INV_DATE).Select(row => row.INV_ID).First()) + 1).ToString();
        }
    }
}
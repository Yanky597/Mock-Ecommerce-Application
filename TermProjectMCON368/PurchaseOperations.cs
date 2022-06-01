﻿using System;
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
            return currentCustomersBalance(userID) >= ShoppingCartTotal;
        }

        public static bool submitOrder(String userID, Dictionary<String, int> shoppingCart)
        {

            if (userCanMakePurchase(userID, ProductOperations.getCartTotal(shoppingCart)) && shoppingCart.Count > 0) 
            {
                String invoiceID = generateAndGetUniqueId();
                createInvoice(userID, invoiceID, shoppingCart);
                createInvoiceRows(invoiceID, shoppingCart);
                updateUserAccountBalance(userID, ProductOperations.getCartTotal(shoppingCart));
                return true;
            }

            return false;
        }

        public static void updateUserAccountBalance(String userID, decimal shoppingCartTotal)
        {


            CUSTOMER_BALANCE currentUser = dbConnection.CUSTOMER_BALANCEs
                 .Where(customer => customer.CUS_ID == userID)
                 .First();

            currentUser.CUS_BALANCE = currentUser.CUS_BALANCE - shoppingCartTotal;
            Console.WriteLine(currentUser.CUS_BALANCE);

            //updateCustomerBalance.CUS_BALANCE = updateCustomerBalance.CUS_BALANCE - shoppingCartTotal;

            //dbConnection.CUSTOMER_BALANCEs.InsertOnSubmit(currentUser);
            dbConnection.SubmitChanges();
            Console.WriteLine("This is after the submit " + currentUser.CUS_BALANCE);
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
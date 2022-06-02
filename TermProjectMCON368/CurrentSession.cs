using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    internal class CurrentSession
    {
        public string ID { get; set; } = "100195";
        public string Username { get; set; }
        public string FullName
        {
            get
            {
                return CustomerOperations.getUsersFullName(ID);
            }
            private set
            {

            }
        }
        public bool isLoggedIn { get; set; } = false;
        public decimal CustomerBalance
        {
            get
            {
                if (isLoggedIn)
                {
                    return getCurrentUserBalance();
                }
                return -1;
            }
            private set { }
        }

        public double ShoppingCartTotal;

        private Dictionary<String, int> shoppingCart = new Dictionary<String, int>();

        public void clearCart() 
        {
            shoppingCart.Clear();
        }

        DataClasses1DataContext dbConnection;

        public bool submitOrder()
        {
            return PurchaseOperations.submitOrder(ID, shoppingCart);
        }

        public void resetDbConnection(DataClasses1DataContext newDbConnection) 
        {
            dbConnection = newDbConnection;
            CustomerOperations.dbConnection = newDbConnection;
            ProductOperations.dbConnection = newDbConnection;
            PurchaseOperations.dbConnection = newDbConnection;
        }

        public CurrentSession(DataClasses1DataContext dbConnection) 
        {
            this.dbConnection = dbConnection;
            CustomerOperations.dbConnection = dbConnection;
            ProductOperations.dbConnection = dbConnection;
            PurchaseOperations.dbConnection = dbConnection;
        }

        public bool payBalanceDue(decimal amountToBePaid) 
        {
            return PurchaseOperations.payBalanceDue(ID, amountToBePaid);
        }

        public bool isValidUser(String username, String password)
        {

            if (username == "" || password == "")
            {
                return false;
            }

            var isVerified = dbConnection.LOGINs.Where(user => user.LOG_USERNAME == username && user.LOG_PASSWORD == password).Any();
            if (isVerified)
            {
                setIdIfUserIsValid(username);
                isLoggedIn = true;
            }
            return isVerified;

        }

        public List<String> GetShoppingCartAsList() 
        {
            List<String> shoppingCartAsList = new List<String>();
            String ItemName;

            foreach (var item in shoppingCart) 
            {
                ItemName = dbConnection.PRODUCTs.Where(prod => prod.PRO_ID == item.Key).First().PRO_NAME;
                shoppingCartAsList.Add($"x{item.Value} {ItemName} ${ProductOperations.getProductPrice(item.Key) * item.Value}");
            }

            return shoppingCartAsList;
        }

        public void setIdIfUserIsValid(String username)
        {
            ID = dbConnection
                 .LOGINs
                 .Where(user => user.LOG_USERNAME == username).First().CUS_ID;
        }

        public int getCartSize()
        {
            int amountOfItemsInCart = 0;

            foreach (var item in shoppingCart)
            {
                amountOfItemsInCart += item.Value;
            }

            return amountOfItemsInCart;
        }

        public decimal getCartTotal()
        {
            return ProductOperations.getCartTotal(shoppingCart);
        }

        public List<INVOICE> getInvoicesWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return dbConnection.INVOICEs
                .Where(invoice => invoice.INV_DATE >= startDate && invoice.INV_DATE <= endDate).ToList();
        }

        public decimal getCurrentUserBalance()
        {
            return CustomerOperations.getUsersBalance(ID);
        }

        public decimal? getCurrentUsersBalanceDue() 
        {
            return CustomerOperations.getUsersBalanceDue(ID);
        }


        // public addUser(newUserForm userInfo, String username, password)

        public void setSessionToLoggedOut() 
        {
            isLoggedIn = false;
        }

        public void addItemToCart(String ProductID) 
        {
            PurchaseOperations.addItemToCart(ProductID, shoppingCart);
        }

        public void deleteItemFromCart(String ProductID)
        {
            PurchaseOperations.deleteItemFromCart(ProductID, shoppingCart);
        }

        public List<INVOICE> getUsersInvoices() 
        {
           return  CustomerOperations.getUsersInvoices(ID);
        }

    }
}

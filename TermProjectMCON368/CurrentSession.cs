using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    internal class CurrentSession
    {
        public string ID { get; set; }
        public string Username { get; set;}
        public string FullName { get; set; }
        public bool isLoggedIn { get; set; } = false;
        public bool CustomerBalance { get; set;}

        public double ShoppingCartTotal;

        private Dictionary<String, int> shoppingCart = new Dictionary<String, int>();    

        DataClasses1DataContext databaseConnection;

        public bool isValidUser(String username, String password)
        {

            if (username == "" || password == "")
            {
                return false;
            }

            var isValid = databaseConnection.LOGINs.Where(user => user.LOG_USERNAME == username && user.LOG_PASSWORD == password).Any();

            if (isValid) 
            {
                setIdIfUserIsValid(username);
            }

            return isValid;
        }

        public void setIdIfUserIsValid(String username) 
        {
            var getId = databaseConnection
                 .LOGINs
                 .Where(user => user.LOG_USERNAME == username);

            if (getId.Count() == 1) 
            {
                getId.ToList().ForEach(userID => ID = userID.CUS_ID);
            }
            
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


        // public addUser(newUserForm userInfo, String username, password)



    }
}

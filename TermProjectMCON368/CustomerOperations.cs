﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class CustomerOperations
    {
        public static DataClasses1DataContext dbConnection;

        public static decimal getUsersBalance(String userID) 
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == userID)
                .Select(customer => customer.CUS_BALANCE).First();
        }

        public static decimal? getUsersBalanceDue(String userID)
        {
            return dbConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == userID)
                .Select(customer => customer.BALANCE_DUE)?.First() ?? 0.00m;
        }

        public static string getUsersFullName(String userID) 
        {
            return dbConnection.CUSTOMERs
              .Where(customer => customer.CUS_ID == userID)
              .Select(customer => customer.CUS_FNAME + " " + customer.CUS_LNAME).First();
        }

        //public static createNewUser() { }
    }
}

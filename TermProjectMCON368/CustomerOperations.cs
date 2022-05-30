﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermProjectMCON368
{
    static class CustomerOperations
    {
        static DataClasses1DataContext databaseConnection;

        public static decimal getUsersBalance(String userID) 
        {
            return databaseConnection.CUSTOMER_BALANCEs
                .Where(customer => customer.CUS_ID == userID)
                .Select(customer => customer.CUS_BALANCE).First();
        }
    }
}

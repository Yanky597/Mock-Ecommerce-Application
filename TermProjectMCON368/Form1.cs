using System;
using System.Linq;
using System.Windows.Forms;

namespace TermProjectMCON368
{
    public partial class Form1 : Form
    {

        CurrentSession theCurrentUser = new CurrentSession();
        DataClasses1DataContext dataBaseConnection;

        public Form1()
        {
            InitializeComponent();

            //Server Explorer

              
            using (dataBaseConnection = new DataClasses1DataContext())
            {
                // session that is used to manage user operations
                CurrentSession userSession = new CurrentSession();
                var customer = dataBaseConnection.CUSTOMERs;
                // and so on
                //var newCustomer = new CUSTOMER()
                //{
                //    CUS_ID = "",

                //};
                //db.CUSTOMERs.InsertOnSubmit(newCustomer);
                //db.SubmitChanges();
                //customer.ToList().ForEach(p => Console.WriteLine(p.CUS_LNAME));

            }

        }


        private void loginBTN_Click(object sender, EventArgs e)
        {
            CurrentSession currUser = new CurrentSession();
            String username = UsernametextBox.Text;
            String password = passwordTextBx.Text;

            if (username.Length > 0 && password.Length > 0) 
            {
                theCurrentUser.isLoggedIn = true;
                theCurrentUser.Name = username;
                WelcomeGuestlbl.Text = $"Welcome {username}";
            }
        }
    }
}

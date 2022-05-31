using System;
using System.Linq;
using System.Windows.Forms;

namespace TermProjectMCON368
{
    public partial class Form1 : Form
    {


        //public static DataClasses1DataContext dataBaseConnection;
        //static CurrentSession userSession;
        CurrentSession userSession;

        public Form1()
        {
            InitializeComponent();

            //using (var dataBaseConnection = new DataClasses1DataContext())
            //{

            //    ProductOperations.dbConnection = dataBaseConnection;
            //    for (int i = 0; i < ProductOperations.getListOfProductNames().Count(); i++)
            //    {
            //        productListView.Items.Add(ProductOperations.getListOfProductNames()[i], i);
            //    }

            //}


            //Server Explorer


            //using (dataBaseConnection = new DataClasses1DataContext())
            //{
            //    // session that is used to manage user operations
            //    //CurrentSession userSession = new CurrentSession(dataBaseConnection);
            //    CustomerOperations.dbConnection = dataBaseConnection;
            //    ProductOperations.dbConnection = dataBaseConnection;
            //    PurchaseOperations.dbConnection = dataBaseConnection;
            //    userSession = new CurrentSession(dataBaseConnection);




            //    var customer = dataBaseConnection.CUSTOMERs;
            //    // and so on
            //    //var newCustomer = new CUSTOMER()
            //    //{
            //    //    CUS_ID = "",

            //    //};
            //    //db.CUSTOMERs.InsertOnSubmit(newCustomer);
            //    //db.SubmitChanges();
            //    //customer.ToList().ForEach(p => Console.WriteLine(p.CUS_LNAME));

            //}

        }


        private void loginBTN_Click(object sender, EventArgs e)
        {
            String username = UsernametextBox.Text;
            String password = passwordTextBx.Text;

            using (var dataBaseConnection = new DataClasses1DataContext())
            {
                userSession = new CurrentSession(dataBaseConnection);

                if (userSession.isValidUser(username, password))
                {
                    //WelcomeGuestlbl.Text = $"Welcome {userSession.FullName}";

                    //UsernametextBox.Text = "";
                    //passwordTextBx.Text = "";
                    loginBx.Visible = false;
                    guestInfoPanel.Visible = true;
                    WelcomeGuestlbl.Text = userSession.FullName;
                    usersBalanceLbl.Text = userSession.CustomerBalance.ToString();
                }
                else 
                {
                    errorMessageLbl.Visible = true;
                }

            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void headerLbl_Click(object sender, EventArgs e)
        {

        }
    }
}

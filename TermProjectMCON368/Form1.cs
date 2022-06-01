using System;
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

                    loginBx.Visible = false;
                    guestInfoPanel.Visible = true;
                    WelcomeGuestlbl.Text = userSession.FullName;
                    usersBalanceLbl.Text = "$" + userSession.CustomerBalance.ToString();
                }
                else 
                {
                    errorMessageLbl.Visible = true;
                }

            }

        }

        private void renderListViewOfCartAfterAddingProduct(String productID) 
        {
            if (userSession != null)
            {
                using (var dataBaseConnection = new DataClasses1DataContext())
                {
                    userSession.resetDbConnection(dataBaseConnection);
                    userSession.addItemToCart(productID);

                    listViewItemsInCart.Items.Clear();

                    //ProductOperations.dbConnection = dataBaseConnection;
                    for (int i = 0; i < userSession.GetShoppingCartAsList().Count; i++)
                    {
                        listViewItemsInCart.Items.Add(userSession.GetShoppingCartAsList()[i], i);
                    }

                    totalAmountLbl.Text = "$" + userSession.getCartTotal().ToString();
                }
            }
            else
            {
                listViewItemsInCart.Items.Clear();
                listViewItemsInCart.Items.Add("Cart is empty");
            }
        }

        private void addMurphysOilToCart(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("22");
        }

        private void renderListViewOfCartAfterDeletingProduct(String productID) 
        {
            if (userSession != null)
            {
                using (var dataBaseConnection = new DataClasses1DataContext())
                {
                    userSession.resetDbConnection(dataBaseConnection);
                    userSession.deleteItemFromCart(productID);

                    listViewItemsInCart.Items.Clear();

                    //ProductOperations.dbConnection = dataBaseConnection;
                    for (int i = 0; i < userSession.GetShoppingCartAsList().Count; i++)
                    {
                        listViewItemsInCart.Items.Add(userSession.GetShoppingCartAsList()[i], i);
                    }
                    totalAmountLbl.Text = "$"+userSession.getCartTotal().ToString();

                }
            }
        }

        private void murphysOilMinusBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("22");
        }

        private void addTissueToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("264");
        }

        private void deleteTissueFromCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("264");

        }

        private void addWipesToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("348");
        }

        private void DeleteWipesFromCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("348");
        }

        private void addNapkinsToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("404");
        }

        private void deleteNapkinsFromCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("404");
        }

        private void addPaperTowelsToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("449");
        }

        private void deletePaperTowelsFromCart_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("449");
        }

        private void addDiapersToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("531");

        }

        private void deleteDiapersFromCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("531");
        }

        private void addToiletPaperBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("542");
        }

        private void DeleteToiletPapersFromCart_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("542");
        }

        private void addWindexToCartBtn_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterAddingProduct("823");
        }

        private void deleteWindexFromCart_Click(object sender, EventArgs e)
        {
            renderListViewOfCartAfterDeletingProduct("823");
        }

        private void logOutBtn_Click(object sender, EventArgs e)
        {

            UsernametextBox.Text = "";
            passwordTextBx.Text = "";

            userSession = null;

            loginBx.Visible = true;
            guestInfoPanel.Visible = false;

            listViewItemsInCart.Items.Clear();
            totalAmountLbl.Text = "$0.00";

        }


        private void ClearCartBtn_Click(object sender, EventArgs e)
        {
            clearCartViews();
        }

        public void clearCartViews() 
        {
            if (userSession != null)
            {
                userSession.clearCart();
                listViewItemsInCart.Items.Clear();
                totalAmountLbl.Text = "$0.00";
                checkoutErrorMessage.Visible = false;
            }
        }

        private void SubmitOrderBtn_Click(object sender, EventArgs e)
        {

            if (userSession != null)
            {
                using (var dataBaseConnection = new DataClasses1DataContext())
                {
                    userSession.resetDbConnection(dataBaseConnection);
                    try
                    {
                        if (userSession.submitOrder())
                        {

                            DialogResult res = MessageBox.Show("Your Order has been submitted", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            clearCartViews();
                            usersBalanceLbl.Text = userSession.getCurrentUserBalance().ToString();
                            checkoutErrorMessage.Visible = false;
                        }
                        else
                        {
                            checkoutErrorMessage.Visible = true;
                        }


                    }
                    catch (Exception E)
                    {
                        checkoutErrorMessage.Visible = true;
                    }

                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Drawing;

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


            using (var dataBaseConnection = new DataClasses1DataContext())
            {
                setupItemsToPurchase(dataBaseConnection);

            }
        }

        private void setupItemsToPurchase(DataClasses1DataContext dataBaseConnection)
        {
            OrderHistoryGroupBox.Visible = false;
            tableLayoutPanel1.Visible = true;
            ProductOperations.dbConnection = dataBaseConnection;
            var productNames = ProductOperations.getListOfProductNames();
            var productPrices = ProductOperations.getListOfProductPrices();
            List<Label> listOfItemNameLabels = new List<Label>();
            List<Label> listOfItemPricesLabels = new List<Label>();

            listOfItemNameLabels.Add(item1TitleLbl);
            listOfItemNameLabels.Add(item2TitleLbl);
            listOfItemNameLabels.Add(item3TitleLbl);
            listOfItemNameLabels.Add(item4TitleLbl);
            listOfItemNameLabels.Add(item5TitleLbl);
            listOfItemNameLabels.Add(item6TitleLbl);
            listOfItemNameLabels.Add(item7TitleLbl);
            listOfItemNameLabels.Add(item8TitleLbl);

            listOfItemPricesLabels.Add(priceItem1Lbl);
            listOfItemPricesLabels.Add(priceItem2Lbl);
            listOfItemPricesLabels.Add(priceItem3Lbl);
            listOfItemPricesLabels.Add(priceItem4Lbl);
            listOfItemPricesLabels.Add(priceItem5Lbl);
            listOfItemPricesLabels.Add(priceItem6Lbl);
            listOfItemPricesLabels.Add(priceItem7Lbl);
            listOfItemPricesLabels.Add(priceItem8Lbl);

            for (int i = 0; i < listOfItemNameLabels.Count(); i++)
            {
                listOfItemNameLabels[i].Text = productNames[i];
                listOfItemPricesLabels[i].Text = $"${Math.Round(productPrices[i], 2).ToString()}";

            }
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
                    balanceDueAmountLbl.Text = "$" + userSession.getCurrentUsersBalanceDue() ?? "0.00";
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
                listViewItemsInCart.Items.Add("Login To");
                listViewItemsInCart.Items.Add("Add Items");
                listViewItemsInCart.Items.Add("To Cart");

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

            using (var dataBaseConnection = new DataClasses1DataContext())
            {
                setupItemsToPurchase(dataBaseConnection);

            }

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
            String errorMessage = "Something went wrong while checking out";

            checkoutErrorMessage.Text = errorMessage;

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
                            usersBalanceLbl.Text = "$" + userSession.CustomerBalance.ToString();
                            balanceDueAmountLbl.Text = "$" + userSession.getCurrentUsersBalanceDue().ToString();
                            checkoutErrorMessage.Visible = false;
                        }
                        else
                        {
                            if (userSession.getCurrentUsersBalanceDue() > PurchaseOperations.BALANCE_DUE_LIMIT) 
                            {
                                checkoutErrorMessage.Text = "Your Balance Due is too high";
                            }
                            if (userSession.getCartSize() < 1) 
                            {
                                checkoutErrorMessage.Text = "You need to add an item to the cart";
                            }

                            checkoutErrorMessage.Visible = true;
                        }


                    }
                    catch (Exception E)
                    {
                        Console.WriteLine(E.Message);
                        Console.WriteLine(E.StackTrace);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void payBalanceClick(object sender, EventArgs e)
        {
            try
            {
                String payBalanceMessage = "How much of your balance would you like to pay? your answer must be numeric and less or equal to the amount you have in your account";
                decimal input = Convert.ToDecimal(Interaction.InputBox(payBalanceMessage, "Pay Balance", "$0.00", 20, 20));

                using (var dataBaseConnection = new DataClasses1DataContext())
                {
                    userSession.resetDbConnection(dataBaseConnection);

                    if (userSession.payBalanceDue(input)) 
                    {
                        usersBalanceLbl.Text = "$" + userSession.CustomerBalance.ToString();
                        balanceDueAmountLbl.Text = "$" + userSession.getCurrentUsersBalanceDue().ToString();
                    }

                }

            }
            catch (Exception E)
            {
                MessageBox.Show("Input can only be numeric", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void displayOrdersTable_Paint(object sender, PaintEventArgs e)
        {

        }

        private void viewCustomerHistory_Click(object sender, EventArgs e)
        {
            using (var dataBaseConnection = new DataClasses1DataContext())
            {
                userSession.resetDbConnection(dataBaseConnection);


                tableLayoutPanel1.Visible = false;
                dataGridDisplayInvoices.Visible = true;
                dataGridInvoice_Row.Visible = false;
                dataGridDisplayInvoices.DataSource = userSession.getUsersInvoices();
                OrderHistoryGroupBox.Visible = true;
                goBackToProductViewBtn.Visible = true;
                RightPanelShoppingCart.Visible = false;
                FilterByDateGroup.Visible = true;
                FilterByPriceGroup.Visible = true;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (var dataBaseConnection = new DataClasses1DataContext())
                {
                    CustomerOperations.dbConnection = dataBaseConnection;
                    var mySelectRow = dataGridDisplayInvoices.SelectedRows[0].DataBoundItem as INVOICE;
                    //MessageBox.Show(mySelectRow.CUS_ID);
                    String invoiceID = mySelectRow.INV_ID;
                    dataGridInvoice_Row.DataSource = CustomerOperations.getUsersInvoiceRows(invoiceID);
                    dataGridDisplayInvoices.Visible = false;
                    dataGridInvoice_Row.Visible = true;
                    goBackToProductViewBtn.Visible = false;

                }

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void goBackToProductViewBtn_Click(object sender, EventArgs e)
        {
            using (var dataBaseConnection = new DataClasses1DataContext())
            {
                setupItemsToPurchase(dataBaseConnection);
                goBackToProductViewBtn.Visible = false;
                RightPanelShoppingCart.Visible = true;
                FilterByDateGroup.Visible = false;
                FilterByPriceGroup.Visible = false;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_3(object sender, EventArgs e)
        {

        }
    }
}

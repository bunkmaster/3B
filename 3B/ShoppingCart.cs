﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3B
{
    public partial class ShoppingCart : Form
    {
        public bookstoreEntities1 BookstoreEntities1;
        public List<shoppingCartControl> shoppingCartControls;

        public static ShoppingCart INSTANCE;



        public ShoppingCart()
        {
            InitializeComponent();

        }




        public static ShoppingCart getInstance()
        {
            if (INSTANCE == null)
                INSTANCE = new ShoppingCart();
            return INSTANCE;
        }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
            OnLoad();

            UpdateDisplay();
        }

        public void OnLoad()
        {
            BookstoreEntities1 = new bookstoreEntities1();
            shoppingCartControls = new List<shoppingCartControl>();

            foreach (var book in ShoppingCartData.getInstance().BookLsListings)
            {
                var shoppingCartControl = new shoppingCartControl {bookLabel = {Text = book.Book.title}};
                shoppingCartControl.authorLabel.Text = "";
                StringBuilder stringBuilder = new StringBuilder();
                var authors = from a in BookstoreEntities1.authors where a.bookid == book.Book.bookid select a;
                foreach (var author in authors)
                {
                    stringBuilder.Append(author.fname + author.lname + " ");
                }
                shoppingCartControl.authorLabel.Text = stringBuilder.ToString();
                shoppingCartControl.priceLabel.Text = "$" + book.Book.price;
                shoppingCartControl.textBox1.Text = book.BookQuantity.ToString(CultureInfo.InvariantCulture);
                shoppingCartControl.quantityPriceLabel.Text = "$" +
                                                              ((double) book.BookQuantity*(double) book.Book.price);
                shoppingCartControl.Book = book;
                shoppingCartControls.Add(shoppingCartControl);
            }
        }

        public void UpdateDisplay()
        {
            shoppingItemPanel.Controls.Clear();
            TableLayoutPanel tableLayout = new TableLayoutPanel();
            double subtotal = 0;

            foreach (var shoppingCartControl in shoppingCartControls)
            {
                tableLayout.Controls.Add(shoppingCartControl);
                subtotal = subtotal + Double.Parse(shoppingCartControl.priceLabel.Text.Replace('$',' '));
            }

           tableLayout.Location = new Point(12, 63);
           tableLayout.Size = new Size(750, 230);
           tableLayout.Dock = DockStyle.Fill;
           tableLayout.AutoSize = false;
           tableLayout.AutoScroll = true;
           tableLayout.Name = "tableLayout";
           tableLayout.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

           shoppingItemPanel.Controls.Add(tableLayout);
           totalLabel.Text = "$" + subtotal;

           Refresh();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                double subtotal = 0;
                foreach (var shoppingCartControl in shoppingCartControls)
                {
                    subtotal = subtotal + Double.Parse(shoppingCartControl.quantityPriceLabel.Text.Replace('$', ' '));
                }
                totalLabel.Text = "$" + subtotal;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void proceedChckoutBtn_Click(object sender, EventArgs e)
        {

            if (ShoppingCartData.getInstance().BookLsListings.Count > 0)
            {
                if (ShoppingCartData.getInstance().UserName == "Temporary User")
                {
                    custRegisterForm registerForm = new custRegisterForm();
                    registerForm.ShowDialog();
                }
                else
                {
                    ConfirmOrder confirmOrder = new ConfirmOrder();
                    confirmOrder.Show();
                    this.Hide();
                }

            }

            else
            {

                MessageBox.Show("No items to checkout", "Error", MessageBoxButtons.OK);
            }
        }

        private void newSearchBtn_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = SearchForm.getInstance();
            searchForm.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShoppingCart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommercialEnterprise.WFUI
{
    public partial class ProductsForm : Form
    {
        private readonly IProductService productService;
        private bool UpdateMode = false;
        public User User { get; set; }
        
        public ProductsForm(IProductService productService)
        {
            InitializeComponent();
            this.productService = productService;
        }

        private async void ProductsForm_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("Price", typeof(double));
            table.Columns.Add("Description", typeof(string));

            var products = await productService.GetAllByUserIdAsync(User.Id);

            foreach (var product in products)
            {
                table.Rows.Add(product.Id, product.Name, product.Quantity, product.Price, product.Description);
            }

            dataGridView1.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ProductsForm_Load(sender, e);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(textBox4.Text);
                if (await productService.IsProductExistsById(id))
                {
                    if (!UpdateMode)
                    {
                        UpdateMode = true;
                        button3.Visible = false;
                        button1.BackColor = Color.Red;
                        textBox1.BackColor = Color.Red;
                        textBox2.BackColor = Color.Red;
                        textBox3.BackColor = Color.Red;
                        richTextBox1.BackColor = Color.Red;
                        button1.BackColor = Color.Green;
                        var allProduct = await productService.GetAllByUserIdAsync(User.Id);
                        var updateProduct = allProduct.FirstOrDefault(x => x.Id == id);

                        textBox1.Text = updateProduct.Name;
                        textBox2.Text = updateProduct.Quantity.ToString();
                        textBox3.Text = updateProduct.Price.ToString();
                        richTextBox1.Text = updateProduct.Description;

                        button1.Text = "Save changes";
                    }
                    else
                    {
                        var product = new Product
                        {
                            Id = id,
                            Name = textBox1.Text,
                            Quantity = Convert.ToInt32(textBox2.Text),
                            Price = Convert.ToDouble(textBox3.Text),
                            Description = richTextBox1.Text,
                            User = User,
                            UserId = User.Id
                        };
                        await productService.UpdateProductAsync(product);

                        UpdateMode = false;
                        button3.Visible = true;
                        button1.BackColor = Color.Gainsboro;
                        textBox1.BackColor = Color.Gainsboro;
                        textBox2.BackColor = Color.Gainsboro;
                        textBox3.BackColor = Color.Gainsboro;
                        richTextBox1.BackColor = Color.Gainsboro;
                        button1.BackColor = Color.Gainsboro;

                        var allProduct = await productService.GetAllByUserIdAsync(User.Id);
                        var updateProduct = allProduct.FirstOrDefault(x => x.Id == id);

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        richTextBox1.Text = "";

                        button1.Text = "Update product";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Some error while updating! Try again!");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var deletedId = Convert.ToInt32(textBox5.Text);
                await productService.DeleteProductByIdAsync(deletedId);
                MessageBox.Show("Succesfully deleted!");
            }
            catch
            {
                MessageBox.Show("Incorrect ID of product!");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {

            try
            {
                var newProduct = new Product
                {
                    Name = textBox1.Text,
                    Quantity = Convert.ToInt32(textBox2.Text),
                    Price = Convert.ToDouble(textBox3.Text),
                    Description = richTextBox1.Text,
                    UserId = User.Id
                };

                await productService.AddProductAsync(newProduct);
                MessageBox.Show("Succesfully added! To see you should update your table!");
            }
            catch
            {
                MessageBox.Show("Incorrect data were put!\nTry again!");
            }
        }

    }
}

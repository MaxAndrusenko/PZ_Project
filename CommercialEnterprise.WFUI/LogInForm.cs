using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure.Models;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
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
    public partial class LogInForm : Form
    {
        private readonly ILoginService loginService;
        private readonly RestorePassForm restorePassForm;
        private readonly ProductsForm productsForm;
        private readonly IUserRepository userRepository;

        public LogInForm(ILoginService loginService, RestorePassForm restorePassForm, ProductsForm productsForm, IUserRepository userRepository)
        {
            InitializeComponent();
            this.loginService = loginService;
            this.restorePassForm = restorePassForm;
            this.productsForm = productsForm;
            this.userRepository = userRepository;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await loginService.AuthorizateAsync(textBox1.Text, textBox2.Text);
                MessageBox.Show("LOG IN!");

                var user = await userRepository.GetUserByLoginAndPasswordAsync(textBox1.Text, textBox2.Text);
                if (user != null)
                {
                    productsForm.User = user;
                }
                productsForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new User()
                {
                    Name = textBox8.Text,
                    Surname = textBox4.Text,
                    Login = textBox3.Text,
                    Password = textBox7.Text,
                    KeyWord = textBox6.Text,
                    Gender = textBox10.Text,
                    Email = textBox5.Text,
                    TelephoneNumber = textBox9.Text,
                    Card = new BankCard()
                    {
                        Number = Convert.ToInt64(textBox11.Text),
                        Date = textBox12.Text,
                        CVV = Convert.ToInt32(textBox13.Text)
                    }
                };

                await loginService.RegisterAsync(user);
                MessageBox.Show("REGISTRATED!\nNow you should log in");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            restorePassForm.Show();
        }
    }
}

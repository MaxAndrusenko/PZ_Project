using CommercialEnterprise.Core.Exceptions;
using CommercialEnterprise.Core.Services.Interfaces;
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
    public partial class RestorePassForm : Form
    {
        private readonly ILoginService loginService;

        public RestorePassForm(ILoginService loginService)
        {
            InitializeComponent();
            this.loginService = loginService;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var login = textBox1.Text;
                var keyword = textBox2.Text;

                if (!await loginService.IsUserExistsWithLoginAndKeyWord(login, keyword))
                {
                    MessageBox.Show("User is not exist!");
                }
                else
                {
                    var newPassword = textBox3.Text;
                    await loginService.RestorePasswordAsync(login, keyword, newPassword);
                    MessageBox.Show("Successful restore!");

                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}

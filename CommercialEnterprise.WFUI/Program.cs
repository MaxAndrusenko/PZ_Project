using CommercialEnterprise.Core.Services;
using CommercialEnterprise.Core.Services.Interfaces;
using CommercialEnterprise.Infrastructure;
using CommercialEnterprise.Infrastructure.Repositories;
using CommercialEnterprise.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommercialEnterprise.WFUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var logInForm = serviceProvider.GetRequiredService<LogInForm>();
                Application.Run(logInForm);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddSingleton<EnterpriceContext>()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<ILoginService, LoginService>()
                .AddSingleton<LogInForm>()
                .AddSingleton<RestorePassForm>()
                .AddSingleton<ProductsForm>();
        }
    }
}

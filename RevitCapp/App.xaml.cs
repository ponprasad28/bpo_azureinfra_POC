using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RevitCapp.Views;

namespace RevitCapp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AzureAuthHelper.Init(
                clientId: "6f13ba5a-3a88-4756-b461-409c047aa72e",
                tenantId: "46128a69-6beb-4b94-a807-e8e4aea19b5f",
                scopes: new[] { "api://a01c063c-3c58-4563-b93d-0ec07b33a93b/access_as_user" } // or .default if using that
            );
        }
    }
}

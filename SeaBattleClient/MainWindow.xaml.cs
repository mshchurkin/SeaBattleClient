using SeaBattleClient.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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


namespace SeaBattleClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service1Client client = new Service1Client();
        String myId = "";
        String myName = "";
        public MainWindow()
        {
            InitializeComponent();
            WebRequest.DefaultWebProxy = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            loginWindow.Closing += LoginWindow_Closing;
            loginWindow.Closed += LoginWindow_Closed;
            btn.IsEnabled = false;
        }

        private void LoginWindow_Closed(object sender, EventArgs e)
        {
            if (myId != "")
            {
                EnemySelection enemySelection = new EnemySelection(myId,myName);
                enemySelection.Show();
            }
            else
            {
                MessageBox.Show("Не удалось зарегестирировать игрока");
            }
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginWindow loginWindow = sender as LoginWindow;
            if (loginWindow.nickname != "")
            {
                myId = client.CreateNewPlayer(loginWindow.nickname);
                myName = loginWindow.nickname;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}

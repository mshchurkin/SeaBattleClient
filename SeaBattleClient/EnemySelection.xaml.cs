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
using System.Windows.Shapes;

namespace SeaBattleClient
{
    /// <summary>
    /// Interaction logic for EnemySelection.xaml
    /// </summary>
    public partial class EnemySelection : Window
    {
        Service1Client client = new Service1Client();
        String myId = "";
        Player enemy;
        public EnemySelection(String myId)
        {
            InitializeComponent();
            this.myId = myId;
            UpdateList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String res=client.FindEnemy(myId);
            if (res != "0")
            {

            }
            else MessageBox.Show("Не удалось найти соперника");
        }

        private void UpdateList()
        {
            playersList.Items.Clear();
            foreach (Player p in client.ShowOnlinePlayers().Where(x => x.ID != myId))
            {
              playersList.Items.Add(p._name);
            }
        }

        private void playersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //enemy = client.ShowOnlinePlayers().Where(x => x._name == playersList.SelectedItem.ToString()).First();
            //Player me = client.ShowOnlinePlayers().Where(x => x.ID == myId).First();
            //String res = client.CreateBattle(me, enemy);
            //if (res != "0")
            //{

            //}
            BattleFieldWindow battleFieldWindow = new BattleFieldWindow(myId,"");
            battleFieldWindow.Show();
            this.Close();
        }
    }

}

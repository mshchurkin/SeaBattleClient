using SeaBattleClient.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
        String myName = "";
        Player enemy;

        async void TimerCheckStartBattle()
        {
            bool flag = false;
            do
            {
                string battleId = await GetBattleInto(3000);
                if (battleId != "0")
                {

                    BattleFieldWindow battleFieldWindow = new BattleFieldWindow(myId, battleId);
                    battleFieldWindow.Show();
                    flag = true;
                    this.Close();
                }
            } while (flag==false);
        }

        Task<string> GetBattleInto(int time)
        {
            return Task.Run(() => {
                Thread.Sleep(time);
                if (client.GetListBattle().Where(x => x.GetPlayerTwo.GetID == myId).Count()!=0)
                    return client.GetListBattle().Where(x => x.GetPlayerTwo.GetID == myId).First().GetID;
                else return "0";
            });
        }
        public EnemySelection(String myId,String myName)
        {
            InitializeComponent();
            this.myId = myId;
            this.myName = myName;
            UpdateList();
            TimerCheckStartBattle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String res=client.FindEnemy(myId);
            if (res != "0")
            {
                BattleFieldWindow battleFieldWindow = new BattleFieldWindow(myId, res);
                battleFieldWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Не удалось найти соперника");
        }

        private void UpdateList()
        {
            playersList.Items.Clear();
            foreach (Player p in client.ShowOnlinePlayers().Where(x => x.GetID != myId).Where(x=>x.GetName!=myName))
            {
                playersList.Items.Add(p.GetName);
            }
        }

        private void playersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enemy = client.ShowOnlinePlayers().Where(x => x.GetName == playersList.SelectedItem.ToString()).First();
            Player me = client.ShowOnlinePlayers().Where(x => x.GetID == myId).First();
            if ((me != null) && (enemy != null))
            {
                String res = client.CreateBattle(me, enemy);
                if (res != "0")
                {
                    BattleFieldWindow battleFieldWindow = new BattleFieldWindow(myId, res);
                    battleFieldWindow.Show();
                }
                else
                {
                    MessageBox.Show("Не удалось создать бой");
                }
            }
            else
            {
                MessageBox.Show("Не удалось создать бой");
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }

}

using SeaBattleClient.GameService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeaBattleClient
{
    /// <summary>
    /// Interaction logic for BattleFieldWindow.xaml
    /// </summary>
    public partial class BattleFieldWindow : Window
    {
        int threeCount = 2;
        int twoCount = 3;
        int oneCount = 4;
        bool battleStarted = true;
        ObservableCollection<BattleCell> battleCells = new ObservableCollection<BattleCell>();
        Service1Client client = new Service1Client();
        List<Ship> shipsList = new List<Ship>();
        Ships ships = new Ships();
        String myId = "";
        String battleId = "";
        bool inithappens = false;

        //public Ship shipFromData(int x, int y, int length, int position)
        //{
        //    Ship ship1 = new Ship();
        //    List<Cell> cells = new List<Cell>();
        //    for (int i = 0; i < length; i++)
        //        cells.Add(
        //            new Cell(x + i * ((position == 1) ? 0 : 1),
        //                y + i * ((position == 1) ? 1 : 0)));
        //    return 
        //}
        async void TimerCheck()
        {
            do
            {
                string message = await GetTurn(3000);
                if (message == myId)
                {
                    EnemyCells.IsEnabled = true;
                    mainLabel.Content = "Ваш ход";
                }
                else
                {
                    EnemyCells.IsEnabled = false;
                    mainLabel.Content = "Ход другого игрока";
                }
            } while (true);
        }

        Task<string> GetTurn(int time)
        {
            return Task.Run(() => {
                Thread.Sleep(time);
                return client.GetTurnPlayer(battleId);
            });
        }

        async void TimerCheckStartBattle()
        {
            bool ok = await GetStartBattle(3000);
            if (ok == true)
            {
                TimerCheck();
            }
        }

        Task<bool> GetStartBattle(int time)
        {
            return Task.Run(() => {
                Thread.Sleep(time);
                return client.StartBattle(battleId);
            });
        }

        public BattleFieldWindow(String myId, String battleId)
        {
            InitializeComponent();
            inithappens = true;
            this.myId = myId;
            this.battleId = battleId;
            FillBattleCells();
        }

        private void FillBattleCells()
        {
            battleCells.Add(new BattleCell(1, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(2, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(3, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(4, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(5, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(6, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(7, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(8, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(9, "", "", "", "", "", "", "", "", "", ""));
            battleCells.Add(new BattleCell(10, "", "", "", "", "", "", "", "", "", ""));
            MyCells.ItemsSource = battleCells;
            EnemyCells.ItemsSource = battleCells;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
                HorRad.IsChecked = false;
        }

        private void RadioButton_Checked_Hor(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
                VertRad.IsChecked = false;
        }

        private void RadioButton_Checked_One(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
            {
                fourCheck.IsChecked = false;
                threeCheck.IsChecked = false;
                twoCheck.IsChecked = false;
            }
        }

        private void RadioButton_Checked_Two(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
            {
                fourCheck.IsChecked = false;
                threeCheck.IsChecked = false;
                oneCheck.IsChecked = false;
            }
        }

        private void RadioButton_Checked_Three(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
            {
                fourCheck.IsChecked = false;
                twoCheck.IsChecked = false;
                oneCheck.IsChecked = false;
            }
        }

        private void RadioButton_Checked_Four(object sender, RoutedEventArgs e)
        {
            if (inithappens == true)
            {
                threeCheck.IsChecked = false;
                twoCheck.IsChecked = false;
                oneCheck.IsChecked = false;
            }
        }

        public DataGridCell GetCell(int rowIndex, int columnIndex, DataGrid dg)
        {
            DataGridRow row = dg.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            DataGridCellsPresenter p = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = p.ItemContainerGenerator.ContainerFromIndex(columnIndex) as DataGridCell;
            return cell;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        private void MyCells_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            IList<DataGridCellInfo> cellsList = MyCells.SelectedCells;
            
            BattleCell battleCell = cellsList.First().Item as BattleCell;
            int colIndex = MyCells.CurrentCell.Column.DisplayIndex;
            var rowIndex = battleCell.RowNum-1;

            Ship ship = new Ship();
            if (fourCheck.IsChecked == true)
            {
                if (HorRad.IsChecked == true)
                {
                    if (colIndex <= 7)
                    {
                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);
                        Cell cell2 = new Cell();
                        cell2.x = colIndex+1;
                        cell2.y = rowIndex;
                        cells.Add(cell2);

                        Cell cell3 = new Cell();
                        cell3.x = colIndex+2;
                        cell3.y = rowIndex;
                        cells.Add(cell3);

                        Cell cell4 = new Cell();
                        cell4.x = colIndex+3;
                        cell4.y = rowIndex;
                        cells.Add(cell4);

                        ship.Length = 4;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex = colIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex=colIndex+1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex = colIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        fourCheck.IsChecked = false;
                        fourCheck.IsEnabled = false;
                    }
                }
                else
                {
                    if (rowIndex <= 7)
                    {
                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);

                        Cell cell2 = new Cell();
                        cell2.x = colIndex;
                        cell2.y = rowIndex+1;
                        cells.Add(cell2);

                        Cell cell3 = new Cell();
                        cell3.x = colIndex;
                        cell3.y = rowIndex+2;
                        cells.Add(cell3);

                        Cell cell4 = new Cell();
                        cell4.x = colIndex;
                        cell4.y = rowIndex+3;
                        cells.Add(cell4);

                        ship.Length = 4;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex=rowIndex+1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex = rowIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex = rowIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        fourCheck.IsChecked = false;
                        fourCheck.IsEnabled = false;
                    }
                }
            }
            else if (threeCheck.IsChecked == true)
            {
                if (HorRad.IsChecked == true)
                {
                    if (colIndex <= 8)
                    {
                        threeCount--;
                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);
                        Cell cell2 = new Cell();
                        cell2.x = colIndex+1;
                        cell2.y = rowIndex;
                        Cell cell3 = new Cell();
                        cell3.x = colIndex+2;
                        cell3.y = rowIndex;
                        cells.Add(cell2);
                        cells.Add(cell3);
                        ship.Length = 3;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex = colIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex = colIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        if (threeCount < 1)
                        {
                            threeCheck.IsChecked = false;
                            threeCheck.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    if (rowIndex <= 8)
                    {
                        threeCount--;

                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);
                        Cell cell2 = new Cell();
                        cell2.x = colIndex;
                        cell2.y = rowIndex+1;
                        Cell cell3 = new Cell();
                        cell3.x = colIndex;
                        cell3.y = rowIndex+2;
                        cells.Add(cell2);
                        cells.Add(cell3);
                        ship.Length = 3;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex=rowIndex+1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex = rowIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        if (threeCount < 1)
                        {
                            threeCheck.IsChecked = false;
                            threeCheck.IsEnabled = false;
                        }
                    }
                }
                
            }
            else if (twoCheck.IsChecked == true)
            {
                if (HorRad.IsChecked == true)
                {
                    if (colIndex <= 9)
                    {
                        twoCount--;

                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);
                        Cell cell2 = new Cell();
                        cell2.x = colIndex+1;
                        cell2.y = rowIndex;
                        cells.Add(cell2);
                        ship.Length = 2;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        colIndex = colIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        if (twoCount < 1)
                        {
                            twoCheck.IsChecked = false;
                            twoCheck.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    if (rowIndex <= 9)
                    {
                        twoCount--;

                        List<Cell> cells = new List<Cell>();
                        Cell cell = new Cell();
                        cell.x = colIndex;
                        cell.y = rowIndex;
                        cells.Add(cell);
                        Cell cell2 = new Cell();
                        cell2.x=colIndex;
                        cell2.y = rowIndex+1;
                        cells.Add(cell2);
                        ship.Length = 2;
                        ship.Cells = cells;
                        shipsList.Add(ship);

                        DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        rowIndex = rowIndex + 1;
                        cellColor = GetCell(rowIndex, colIndex, MyCells);
                        cellColor.Background = new SolidColorBrush(Colors.Green);
                        if (twoCount < 1)
                        {
                            twoCheck.IsChecked = false;
                            twoCheck.IsEnabled = false;
                        }
                    }
                }           
            }
            else if (oneCheck.IsChecked == true)
            {
                    oneCount--;
                    List<Cell> cells = new List<Cell>();
                    Cell cell = new Cell();
                    cell.x = colIndex;
                    cell.y = rowIndex;
                    cells.Add(cell);
                    ship.Length = 1;
                    ship.Cells = cells;
                    shipsList.Add(ship);
                    DataGridCell cellColor = GetCell(rowIndex, colIndex, MyCells);
                    cellColor.Background = new SolidColorBrush(Colors.Green);
                    if (oneCount < 1)
                    {
                        oneCheck.IsChecked = false;
                        oneCheck.IsEnabled = false;
                    }
            }
            else
            {
                if ((twoCount < 1) && (oneCount < 1) && (threeCount < 1) && (fourCheck.IsEnabled == false)){
                    Ships ships = new Ships();
                    ships.ships = shipsList;
                    if (client.ShipsLocate(myId, battleId, ships) == true)
                    {
                        mainLabel.Content = "Ожидание второго игрока";
                        HorRad.Visibility = Visibility.Hidden;
                        VertRad.Visibility = Visibility.Hidden;
                        fourCheck.Visibility = Visibility.Hidden;
                        threeCheck.Visibility = Visibility.Hidden;
                        twoCheck.Visibility = Visibility.Hidden;
                        oneCheck.Visibility = Visibility.Hidden;
                        TimerCheckStartBattle();
                    }
                    else
                    {
                        MessageBox.Show("Неправильно расставлены корабли");
                        fourCheck.IsEnabled = true;
                        threeCount = 3;
                        threeCheck.IsEnabled = true;
                        twoCount = 2;
                        twoCheck.IsEnabled = true;
                        oneCheck.IsEnabled = true;
                        oneCount = 4;
                        MyCells.Items.Clear();
                        EnemyCells.Items.Clear();
                        FillBattleCells();
                    }
                }
            }
        }

        private void EnemyCells_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (battleStarted)
            {
                IList<DataGridCellInfo> cellsList = EnemyCells.SelectedCells;

                BattleCell battleCell = cellsList.First().Item as BattleCell;
                int colIndex = EnemyCells.CurrentCell.Column.DisplayIndex;
                var rowIndex = battleCell.RowNum - 1;

                bool success = client.MakeShot(colIndex, rowIndex, myId, battleId);
                if (success == true)
                {
                    DataGridCell cellColor = GetCell(rowIndex, colIndex, EnemyCells);
                    cellColor.Background = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    DataGridCell cellColor = GetCell(rowIndex, colIndex, EnemyCells);
                    cellColor.Background = new SolidColorBrush(Colors.Blue);
                }
                if (client.GetWinner(battleId) != "0")
                {
                    if (client.GetWinner(battleId) == myId)
                    {
                        MessageBox.Show("Поздравляем, вы победили");
                        client.ExitGame(myId);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Вы проиграли");
                        client.ExitGame(myId);
                        this.Close();
                    }
                }
                else
                {
                    EnemyCells.IsEnabled = false;
                    mainLabel.Content = "Ход другого игрока";
                }
            }
        }

        
    }
}

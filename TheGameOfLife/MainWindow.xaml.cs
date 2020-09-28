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
using System.Windows.Threading;

namespace TheGameOfLife
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Cell> Map = new List<Cell>();

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() => GridSetup()));
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

        private void GridSetup()
        {
            int columns = 41;

            int r = 0;
            int c = 0;

            foreach (Button cell in FindVisualChildren<Button>(this).Where(b => String.IsNullOrEmpty(b.Name)))
            {
                cell.Name = string.Format("Cell_{0}_{1}", r, c);
                cell.Click += Cell_Click;
                Map.Add(new Cell(cell)
                {
                    Name = cell.Name,
                    coorX = c,
                    coorY = r
                });
                if (++c > columns)
                {
                    c = 0;
                    ++r;
                }
            }
        }


        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Cell cell = Cell.Grid.Single(c => c.Name == button.Name);
            if (!cell.alive)
            {
                cell.TurnOn();
            }
            else
            {
                cell.TurnOff();
            }
        }



        //Helper function to find components
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

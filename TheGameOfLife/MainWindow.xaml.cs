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


        const int columns = 60;
        const int rows = 60;
        Rectangle[,] grid = new Rectangle[columns, rows];

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();




        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }

  

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            int columns = 60;
            int rows = 60;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = panelGame.ActualWidth / columns -0.75;
                    r.Height = panelGame.ActualHeight / rows -0.75;
                    r.Fill = Brushes.Black;
                    panelGame.Children.Add(r);
                    Canvas.SetLeft(r, j * panelGame.ActualWidth / columns);
                    Canvas.SetTop(r, i * panelGame.ActualHeight / rows);
                    r.MouseDown += R_MouseDown;

                    grid[i, j] = r;

                }

            }
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Black) ? Brushes.White : Brushes.Black;
        }

        private void simulateButton_Click(object sender, RoutedEventArgs e)
        {
            int[,] panel = new int[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {

                    int superior = i - 1;

                    //boundary

                    int left = i - 1;
                    int right = i + 1;
                    int down = j - 1;
                    int up = j + 1;

                    if (left < 0)
                    {
                        left = columns - 1;
                    }
                    if (right >= columns)
                    {
                        right = 0;
                    }
                    if (down < 0 )
                    {
                        down = rows - 1;
                    }
                    if (up >= rows)
                    {
                        up = 0;
                    }

                    // Count neighbours

                    int neighbours = 0;
                    if (grid[left  ,up ].Fill == Brushes.White)
                    { neighbours++; }
                    if (grid[i , up].Fill == Brushes.White)
                    { neighbours++; }
                    if (grid[right, up].Fill == Brushes.White)
                    { neighbours++; }
                    if (grid[left, j ].Fill == Brushes.White)
                    { neighbours++; }
                    if (grid[right, j ].Fill == Brushes.White)
                    { neighbours++;  }
                    if (grid[left, down].Fill == Brushes.White)
                    { neighbours++; }
                    if (grid[i , down].Fill == Brushes.White)
                    { neighbours++;  }
                    if (grid[right, down].Fill == Brushes.White)
                    { neighbours++; }

                    panel[i, j] = neighbours;
                }
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (panel[i, j] < 2 || panel[i,j] > 3)
                    {
                        grid[i, j].Fill = Brushes.Black;
                    }
                    else if (panel[i,j] == 3)
                    {
                        grid[i, j].Fill = Brushes.White;
                    }
                }
            }
        }
    }
}

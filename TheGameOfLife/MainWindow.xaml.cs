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
        public int columns;
        public int rows;
        Rectangle[,] grid;
        DispatcherTimer timer = new DispatcherTimer();
        bool stopped = false;

        Stack<MatrizdeCells> stackmatrices = new Stack<MatrizdeCells>();
        List<CellType> listCellTypes = new List<CellType>();


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(1 / Convert.ToDouble(speedSlider.Value));

            MatrizdeCells oldmatrix = stackmatrices.Pop();
            MatrizdeCells newmatrix = new MatrizdeCells(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (oldmatrix.matrix[i, j].alive == true && (oldmatrix.CountAliveNeibourghs(i, j) == 2 || oldmatrix.CountAliveNeibourghs(i, j) == 3))
                    {
                        grid[i, j].Fill = Brushes.White;
                        newmatrix.matrix[i, j].alive = true;
                    }
                    else
                    {
                        newmatrix.matrix[i, j].alive = false;
                        grid[i, j].Fill = Brushes.Black;
                    }

                    if (oldmatrix.matrix[i, j].alive == false && oldmatrix.CountAliveNeibourghs(i, j) == 3)
                    {
                        grid[i, j].Fill = Brushes.White;
                        newmatrix.matrix[i, j].alive = true;
                    }
                }
            }
            stackmatrices.Push(oldmatrix);
            stackmatrices.Push(newmatrix);
        }


        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            rows = Convert.ToInt32(tb_Rows.Text);
            columns = Convert.ToInt32(tb_Columns.Text);
            grid = new Rectangle[rows, columns];
            MatrizdeCells newMatrix = new MatrizdeCells(rows, columns);  // Cuando creamos la grid todas las celulas las inicializamos muertas (al crear un obj matriz ya creamos una matriz de celulas muertas)
            stackmatrices.Push(newMatrix);

            // Inicializamos la parte grafica
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
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Black) ? (Brushes.White) : Brushes.Black;

            // creamos nuevo objeto matrix, le aplicamos los cambios hechos por el Mouse Click y la añadimos al stack de matrices
            MatrizdeCells newMatrix = new MatrizdeCells(rows, columns);
            for (int i = 0; i < rows; i++) // Cada vezs que hacemos un cambio en la grid la recorremos y metemos los cambios en la Matriz de celulas
            {
                for (int j = 0; j < columns; j++)
                {
                    if (grid[i, j].Fill == Brushes.White) { newMatrix.matrix[i, j].alive = true; }
                    else if (grid[i, j].Fill == Brushes.Black) { newMatrix.matrix[i, j].alive = false; }
                }
            }
            stackmatrices.Push(newMatrix);
        }

        private void simulateButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (stopped == false)
            { 
                timer.Stop();
                stopped = true;
            }
            else if(stopped)
            { 
                timer.Start();
                stopped = false;
            }
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            for(int i = stackmatrices.Count; i>1; i--)
            {
                stackmatrices.Pop();
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    grid[i, j].Fill = Brushes.Black;
                }
            }
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            MatrizdeCells oldmatrix = stackmatrices.Pop();
            MatrizdeCells newmatrix = new MatrizdeCells(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (oldmatrix.matrix[i,j].alive==true && (oldmatrix.CountAliveNeibourghs(i, j) == 2 || oldmatrix.CountAliveNeibourghs(i, j) == 3))
                    {
                        grid[i, j].Fill = Brushes.White;
                        newmatrix.matrix[i, j].alive = true;
                    }
                    else 
                    {
                        newmatrix.matrix[i, j].alive = false;
                        grid[i, j].Fill = Brushes.Black;
                    }

                    if (oldmatrix.matrix[i, j].alive == false && oldmatrix.CountAliveNeibourghs(i, j) == 3)
                    {
                        grid[i, j].Fill = Brushes.White;
                        newmatrix.matrix[i, j].alive = true;
                    }
                }
            }
            stackmatrices.Push(oldmatrix);
            stackmatrices.Push(newmatrix);
        }

        private void tb_Rows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (stackmatrices.Count > 0)
            {
                MatrizdeCells matrix1 = stackmatrices.Pop();

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix1.matrix[i, j].alive == true) { grid[i, j].Fill = Brushes.White; }
                        else { grid[i, j].Fill = Brushes.Black; }
                    }
                }
            }

            else
            {
                MessageBox.Show("There are no previous steps.");
            }
        }

        private void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            for (int i = 0; i < listCellTypes.Count(); i++)
            {
                ComboBox_TypeofCell.Items.Add(listCellTypes[i].Name);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void _Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void bt_CreateNewCell_Click_1(object sender, RoutedEventArgs e)
        {
            CreateNewCellType CreateNewCellTye1 = new CreateNewCellType(listCellTypes);
            CreateNewCellTye1.ShowDialog();
            listCellTypes = CreateNewCellTye1.listaTypeofCells;
        }
    }
}

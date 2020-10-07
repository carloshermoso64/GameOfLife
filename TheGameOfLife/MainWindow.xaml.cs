using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                    if (oldmatrix.matrix[i, j].celtype == null) // si la cell no tiene tipo (e el timpo de celula basico) y usamos las reglas del ppio
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
                    else
                    {
                        int k = 0;
                        while (k < listCellTypes.Count)
                        {
                            if (j == 1 && i == 1)
                            {

                            }
                            if (listCellTypes[k].Name == oldmatrix.matrix[i, j].celtype.Name.ToString())
                            {
                                CellType celltype1 = listCellTypes[k];

                                if (oldmatrix.isAlive_UpLeft(i, j) == celltype1.reviveif_top_left_alive && oldmatrix.isAlive_Up(i, j) == celltype1.reviveif_top_alive && oldmatrix.isAlive_UpRight(i, j) == celltype1.reviveif_top_right_alive && oldmatrix.isAlive_Left(i, j) == celltype1.reviveif_left_alive && oldmatrix.isAlive_BottomRight(i, j) == celltype1.reviveif_bottom_right_alive && oldmatrix.isAlive_BottomLeft(i, j) == celltype1.reviveif_bottmo_left_alive && oldmatrix.isAlive_Bottom(i, j) == celltype1.reviveif_bottom_alive && oldmatrix.matrix[i, j].alive == false)
                                {
                                    grid[i, j].Fill = Brushes.White;
                                    newmatrix.matrix[i, j].alive = true;
                                    newmatrix.matrix[i, j].celtype = listCellTypes[k];
                                }

                                if (oldmatrix.isAlive_UpLeft(i, j) == celltype1.killif_top_left_alive && oldmatrix.isAlive_Up(i, j) == celltype1.killif_top_alive && oldmatrix.isAlive_UpRight(i, j) == celltype1.killif_top_right_alive && oldmatrix.isAlive_Left(i, j) == celltype1.killif_left_alive && oldmatrix.isAlive_BottomRight(i, j) == celltype1.killif_bottom_right_alive && oldmatrix.isAlive_BottomLeft(i, j) == celltype1.killif_bottmo_left_alive && oldmatrix.isAlive_Bottom(i, j) == celltype1.killif_bottom_alive && oldmatrix.matrix[i, j].alive == true)
                                {
                                    grid[i, j].Fill = Brushes.Black;
                                    newmatrix.matrix[i, j].alive = false;
                                    newmatrix.matrix[i, j].celtype = listCellTypes[k];
                                }
                            }
                            k = k + 1;
                        }
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
            stackmatrices.Clear();
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

                    newMatrix.matrix[i, j].celtype = stackmatrices.First().matrix[i, j].celtype; // las celulas nuevas son del mismo tipo que en la iteracion anterior (a no ser que lo clickemos y lo cambiemos, que es de lo que va el resto del codigo)

                    if(grid[i, j].Fill == Brushes.White && stackmatrices.First().matrix[i,j].alive==false) // si una cell ahora esta viva y antes estaba muerta (es decir, le hemos dado vida clickando encima, asignale el celltype que hay en el combobox ahora mismo )
                    {
                        if (ComboBox_TypeofCell.Text != "Classic Cell")
                        {
                            // si en el combobox no estamos en "classic cell" (estamos poniendo otro tipo de cell) buscamos ese nombre en la lista de tipos y le asignamos ese tipo a esa celula
                            int k = 0;
                            while (k < listCellTypes.Count)
                            {
                                if (listCellTypes[k].Name == ComboBox_TypeofCell.Text)
                                {
                                    newMatrix.matrix[i, j].celtype = listCellTypes[k];
                                    break;
                                }
                                k = k + 1;
                            }
                        }
                    }
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
                    if (oldmatrix.matrix[i, j].celtype == null) // si la cell no tiene tipo (e el timpo de celula basico) y usamos las reglas del ppio
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
                    else
                    {
                        int k = 0;
                        while (k < listCellTypes.Count)
                        {
                            if (listCellTypes[k].Name == oldmatrix.matrix[i, j].celtype.Name.ToString())
                            {
                                CellType celltype1 = listCellTypes[k];
                                newmatrix.matrix[i, j].celtype = listCellTypes[k];

                                if (oldmatrix.isAlive_UpLeft(i, j) == celltype1.reviveif_top_left_alive && oldmatrix.isAlive_Up(i, j) == celltype1.reviveif_top_alive && oldmatrix.isAlive_UpRight(i, j) == celltype1.reviveif_top_right_alive && oldmatrix.isAlive_Left(i, j) == celltype1.reviveif_left_alive && oldmatrix.isAlive_BottomRight(i, j) == celltype1.reviveif_bottom_right_alive && oldmatrix.isAlive_BottomLeft(i, j) == celltype1.reviveif_bottmo_left_alive && oldmatrix.isAlive_Bottom(i, j) == celltype1.reviveif_bottom_alive && oldmatrix.matrix[i, j].alive == false)
                                {
                                    grid[i, j].Fill = Brushes.White;
                                    newmatrix.matrix[i, j].alive = true;
                                }

                                if (oldmatrix.isAlive_UpLeft(i, j) == celltype1.killif_top_left_alive && oldmatrix.isAlive_Up(i, j) == celltype1.killif_top_alive && oldmatrix.isAlive_UpRight(i, j) == celltype1.killif_top_right_alive && oldmatrix.isAlive_Left(i, j) == celltype1.killif_left_alive && oldmatrix.isAlive_BottomRight(i, j) == celltype1.killif_bottom_right_alive && oldmatrix.isAlive_BottomLeft(i, j) == celltype1.killif_bottmo_left_alive && oldmatrix.isAlive_Bottom(i, j) == celltype1.killif_bottom_alive && oldmatrix.matrix[i, j].alive == true)
                                {
                                    grid[i, j].Fill = Brushes.Black;
                                    newmatrix.matrix[i, j].alive = false;
                                }
                            }
                            k = k + 1;
                        }
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
            ComboBox_TypeofCell.Items.Add(listCellTypes.Last().Name);
        }

        private void ComboBox_TypeofCell_DropDownOpened(object sender, EventArgs e)
        {
        }

        private void bt_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string direccion;
            if(saveFileDialog1.ShowDialog() == true)
            {
                direccion = saveFileDialog1.FileName;
                char char1;
                int i = 0;
                bool bool1 = false;
                while (i < direccion.Length && direccion[i] != Convert.ToChar(":") && bool1 == false)
                {
                    try
                    {
                        char1 = Convert.ToChar(direccion[i]);
                    }
                    catch
                    {
                        bool1 = true;
                    }
                    i = i + 1;
                }
                char1 = Convert.ToChar(direccion[i + 1]);
                char char2 = Convert.ToChar("/");

                direccion = direccion.Replace(char1, char2);

                StreamWriter file1 = new StreamWriter(direccion);

                // Escribimos info basica del grid:
                file1.WriteLine("Rows =" + rows);
                file1.WriteLine("Columns =" + columns);

                // Escribimos clases de cells;
                file1.WriteLine();
                file1.WriteLine();
                file1.WriteLine("CELL TYPES");
                file1.WriteLine();
                file1.WriteLine("Number of Cell tyoes =" + listCellTypes.Count());
                for (int k=0; k<listCellTypes.Count();k++)
                {
                    CellType celltype1 = listCellTypes[k];

                    file1.WriteLine("Name =" + celltype1.Name);

                    file1.WriteLine("Revive if Top Alive =" + celltype1.reviveif_top_alive.ToString());
                    file1.WriteLine("Revive if Top Left Alive =" + celltype1.reviveif_top_left_alive.ToString());
                    file1.WriteLine("Revive if Top Right Alive =" + celltype1.reviveif_top_right_alive.ToString());

                    file1.WriteLine("Revive if Left Alive =" + celltype1.reviveif_left_alive.ToString());
                    file1.WriteLine("Revive if Right Alive =" + celltype1.reviveif_right_alive.ToString());

                    file1.WriteLine("Revive if Bottom Alive =" + celltype1.reviveif_bottom_alive.ToString());
                    file1.WriteLine("Revive if Bottom Left Alive =" + celltype1.reviveif_bottmo_left_alive.ToString());
                    file1.WriteLine("Revive if Bottom Right Alive =" + celltype1.reviveif_bottom_right_alive.ToString());

                    file1.WriteLine("Kill if Top Alive =" + celltype1.killif_top_alive.ToString());
                    file1.WriteLine("Kill if Top Left Alive =" + celltype1.killif_top_left_alive.ToString());
                    file1.WriteLine("Kill if Top Right Alive =" + celltype1.killif_top_right_alive.ToString());

                    file1.WriteLine("Kill if Left Alive =" + celltype1.killif_left_alive.ToString());
                    file1.WriteLine("Kill if Right Alive =" + celltype1.killif_right_alive.ToString());

                    file1.WriteLine("Kill if Bottom Alive =" + celltype1.killif_bottom_alive.ToString());
                    file1.WriteLine("Kill if Bottom Left Alive =" + celltype1.killif_bottmo_left_alive.ToString());
                    file1.WriteLine("Kill if Bottom Right Alive =" + celltype1.killif_bottom_right_alive.ToString());

                    file1.WriteLine();
                }


                //Escribimos las iteraciones en orden
                file1.WriteLine("STEPS");
                file1.WriteLine("Number of Steps =" + stackmatrices.Count());
                file1.WriteLine();

                int iterations = stackmatrices.Count();
                Stack<MatrizdeCells> newstackmatrices = stackmatrices;
                newstackmatrices.Reverse();

                for (int a=0; a<iterations; a++)
                {
                    MatrizdeCells newMatrix1 = newstackmatrices.Pop();

                    for (int b = 0; b < rows; b++)
                    {
                        for (int c = 0; c < columns; c++)
                        {
                            if(newMatrix1.matrix[b, c].celtype == null)
                            {
                                file1.WriteLine(newMatrix1.matrix[b, c].alive.ToString());
                            }
                            else
                            {
                                file1.WriteLine(newMatrix1.matrix[b, c].alive.ToString() + "+" + newMatrix1.matrix[b, c].celtype.ToString());
                            }
                        }
                    }
                    file1.WriteLine();
                    file1.WriteLine("----------");
                    file1.WriteLine();
                }
                file1.WriteLine("END");
                file1.Close();
            }
        }

        private void bt_Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string direccion;
            if (openFileDialog1.ShowDialog() == true)
            {
                direccion = openFileDialog1.FileName;
                char char1;
                int i = 0;
                bool bool1 = false;
                while (i < direccion.Length && direccion[i] != Convert.ToChar(":") && bool1 == false)
                {
                    try
                    {
                        char1 = Convert.ToChar(direccion[i]);
                    }
                    catch
                    {
                        bool1 = true;
                    }
                    i = i + 1;
                }
                char1 = Convert.ToChar(direccion[i + 1]);
                char char2 = Convert.ToChar("/");

                direccion = direccion.Replace(char1, char2);

                StreamReader sr1 = new StreamReader(direccion);

                rows = Convert.ToInt32(sr1.ReadLine().Split(Convert.ToChar("="))[1].ToString());
                columns = Convert.ToInt32(sr1.ReadLine().Split(Convert.ToChar("="))[1].ToString());

                //Leemos Cell Types
                sr1.ReadLine();
                sr1.ReadLine();
                sr1.ReadLine();
                sr1.ReadLine();
                int numtypeofcells = Convert.ToInt32(sr1.ReadLine().Split(Convert.ToChar("="))[1].ToString());
                listCellTypes.Clear();
                for(i=0; i< numtypeofcells; i++)
                {
                    string name;

                    bool reviveif_top_alive;
                    bool reviveif_top_right_alive;
                    bool reviveif_top_left_alive;
                    bool reviveif_left_alive;
                    bool reviveif_right_alive;
                    bool reviveif_bottom_alive;
                    bool reviveif_bottmo_left_alive;
                    bool reviveif_bottom_right_alive;

                    bool killif_top_alive;
                    bool killif_top_left_alive;
                    bool killif_top_right_alive;
                    bool killif_left_alive;
                    bool killif_right_alive;
                    bool killif_bottom_alive;
                    bool killif_bottmo_left_alive;
                    bool killif_bottom_right_alive;

                    name = sr1.ReadLine().Split(Convert.ToChar("="))[1];

                    string a1 = sr1.ReadLine();
                    if(a1.Split(Convert.ToChar("="))[1] == "True") { reviveif_top_alive = true; }
                    else { reviveif_top_alive = false; }

                    string a2 = sr1.ReadLine();
                    if (a2.Split(Convert.ToChar("="))[1] == "True") { reviveif_top_left_alive = true; }
                    else { reviveif_top_left_alive = false; }

                    string a3 = sr1.ReadLine();
                    if (a3.Split(Convert.ToChar("="))[1] == "True") { reviveif_top_right_alive = true; }
                    else { reviveif_top_right_alive = false; }

                    string a4 = sr1.ReadLine();
                    if (a4.Split(Convert.ToChar("="))[1] == "True") { reviveif_left_alive = true; }
                    else { reviveif_left_alive = false; }

                    string a5 = sr1.ReadLine();
                    if (a5.Split(Convert.ToChar("="))[1] == "True") { reviveif_right_alive = true; }
                    else { reviveif_right_alive = false; }

                    string a6 = sr1.ReadLine();
                    if (a6.Split(Convert.ToChar("="))[1] == "True") { reviveif_bottom_alive = true; }
                    else { reviveif_bottom_alive = false; }

                    string a7 = sr1.ReadLine();
                    if (a7.Split(Convert.ToChar("="))[1] == "True") { reviveif_bottmo_left_alive = true; }
                    else { reviveif_bottmo_left_alive = false; }

                    string a8 = sr1.ReadLine();
                    if (a8.Split(Convert.ToChar("="))[1] == "True") { reviveif_bottom_right_alive = true; }
                    else { reviveif_bottom_right_alive = false; }


                    string a9 = sr1.ReadLine();
                    if (a9.Split(Convert.ToChar("="))[1] == "True") { killif_top_alive = true; }
                    else { killif_top_alive = false; }

                    string a10 = sr1.ReadLine();
                    if (a10.Split(Convert.ToChar("="))[1] == "True") { killif_top_left_alive = true; }
                    else { killif_top_left_alive = false; }

                    string a11 = sr1.ReadLine();
                    if (a11.Split(Convert.ToChar("="))[1] == "True") { killif_top_right_alive = true; }
                    else { killif_top_right_alive = false; }

                    string a12 = sr1.ReadLine();
                    if (a12.Split(Convert.ToChar("="))[1] == "True") { killif_left_alive = true; }
                    else { killif_left_alive = false; }

                    string a13 = sr1.ReadLine();
                    if (a13.Split(Convert.ToChar("="))[1] == "True") { killif_right_alive = true; }
                    else { killif_right_alive = false; }

                    string a14 = sr1.ReadLine();
                    if (a14.Split(Convert.ToChar("="))[1] == "True") { killif_bottom_alive = true; }
                    else { killif_bottom_alive = false; }

                    string a15 = sr1.ReadLine();
                    if (a15.Split(Convert.ToChar("="))[1] == "True") { killif_bottmo_left_alive = true; }
                    else { killif_bottmo_left_alive = false; }

                    string a16 = sr1.ReadLine();
                    if (a16.Split(Convert.ToChar("="))[1] == "True") { killif_bottom_right_alive = true; }
                    else { killif_bottom_right_alive = false; }

                    CellType newCellType1 = new CellType(name, reviveif_top_alive, reviveif_top_right_alive, reviveif_top_left_alive, reviveif_left_alive, reviveif_right_alive, reviveif_bottmo_left_alive, reviveif_bottom_right_alive, reviveif_bottom_alive, killif_top_alive, killif_top_right_alive, killif_top_left_alive, killif_left_alive, killif_right_alive, killif_bottmo_left_alive, killif_bottom_right_alive, killif_bottom_alive);
                    listCellTypes.Add(newCellType1);

                    sr1.ReadLine();
                }

                // Leemos las iteraciones
                stackmatrices.Clear();
                string a = sr1.ReadLine();
                double numberofiterations = Convert.ToInt32(sr1.ReadLine().Split(Convert.ToChar("="))[1].ToString());
                sr1.ReadLine();

                for(i =0; i<numberofiterations;i++)
                {
                    if(i==63)
                    {

                    }

                    MatrizdeCells newMatrix = new MatrizdeCells(rows, columns);

                    for(int j =0; j<rows;j++)
                    {
                        for (int k = 0; k < columns; k++)
                        {
                            string linea = sr1.ReadLine();
                            if (linea == "END") { break; }

                            if (linea.Contains(Convert.ToChar("+")) == false)
                            {
                                if(linea=="True")
                                {
                                    newMatrix.matrix[j, k].alive = true;
                                }
                                else
                                {
                                    newMatrix.matrix[j, k].alive = false;
                                }
                            }
                            else
                            {
                                string bool11 = linea.Split(Convert.ToChar("+"))[0].ToString();
                                string celltype1 = linea.Split(Convert.ToChar("+"))[1].ToString();

                                if (bool11=="True")
                                {
                                    newMatrix.matrix[j, k].alive = true;
                                }
                                else
                                {
                                    newMatrix.matrix[j, k].alive = false;
                                }

                                // buscamos el celltype en la lista de celltypes
                                for(int p = 0; p<listCellTypes.Count(); p++)
                                {
                                    if(celltype1==listCellTypes[p].Name)
                                    {
                                        newMatrix.matrix[j, k].celtype = listCellTypes[p];
                                    }
                                }
                            }
                        }
                    }
                    stackmatrices.Push(newMatrix);
                    sr1.ReadLine();
                    sr1.ReadLine();
                    sr1.ReadLine();
                }

                string[] file1 = File.ReadAllLines(direccion);

                Stack<MatrizdeCells> newstack = new Stack<MatrizdeCells>();
                while(stackmatrices.Count>0)
                {
                    newstack.Push(stackmatrices.Pop());
                }
                stackmatrices = newstack;
            }
        }
    }
}

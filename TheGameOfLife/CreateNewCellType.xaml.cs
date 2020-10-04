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
using System.Windows.Shapes;

namespace TheGameOfLife
{
    /// <summary>
    /// Lógica de interacción para CreateNewCellType.xaml
    /// </summary>
    public partial class CreateNewCellType : Window
    {
        public List<CellType> listaTypeofCells = new List<CellType>();

        public CreateNewCellType(List<CellType> listaTypeofCells)
        {
            InitializeComponent();
            this.listaTypeofCells = listaTypeofCells;
        }

        private void bt_CreateNewCell_Click(object sender, RoutedEventArgs e)
        {
            List<string> listanombres = new List<string>();

            for (int i = 0; i < listaTypeofCells.Count(); i++)
            {
                listanombres.Add(listaTypeofCells[i].Name);
            }

            if (listanombres.Contains(tb_Name.Text) == false && tb_Name.Text.Length>0)
            {
                string name = tb_Name.Text;

                bool reviveif_top_alive;
                bool reviveif_top_right_alive;
                bool reviveif_top_left_alive;
                bool reviveif_left_alive;
                bool reviveif_right_alive;
                bool reviveif_bottom_left_alive;
                bool reviveif_bottom_right_alive;
                bool reviveif_bottom_alive;

                bool killif_top_alive;
                bool killif_top_right_alive;
                bool killif_top_left_alive;
                bool killif_left_alive;
                bool killif_right_alive;
                bool killif_bottom_left_alive;
                bool killif_bottom_right_alive;
                bool killif_bottom_alive;

                if (Reviveif_TopLeft_alive.Text == "Alive") { reviveif_top_left_alive = true; }
                else { reviveif_top_left_alive = false; }

                if (Reviveif_TopRight_alive.Text == "Alive") { reviveif_top_right_alive = true; }
                else { reviveif_top_right_alive = false; }

                if (Reviveif_Top_alive.Text == "Alive") { reviveif_top_alive = true; }
                else { reviveif_top_alive = false; }

                if (Reviveif_Left_alive.Text == "Alive") { reviveif_left_alive = true; }
                else { reviveif_left_alive = false; }

                if (Reviveif_Right_alive.Text == "Alive") { reviveif_right_alive = true; }
                else { reviveif_right_alive = false; }

                if (Reviveif_BottomLeft_alive.Text == "Alive") { reviveif_bottom_left_alive = true; }
                else { reviveif_bottom_left_alive = false; }

                if (Reviveif_BottomRight_alive.Text == "Alive") { reviveif_bottom_right_alive = true; }
                else { reviveif_bottom_right_alive = false; }

                if (Reviveif_Bottom_alive.Text == "Alive") { reviveif_bottom_alive = true; }
                else { reviveif_bottom_alive = false; }

                if (Killif_TopLeft_alive.Text == "Alive") { killif_top_left_alive = true; }
                else { killif_top_left_alive = false; }

                if (Killif_TopRight_alive.Text == "Alive") { killif_top_right_alive = true; }
                else { killif_top_right_alive = false; }

                if (Killif_Top_alive.Text == "Alive") { killif_top_alive = true; }
                else { killif_top_alive = false; }

                if (Killif_Left_alive.Text == "Alive") { killif_left_alive = true; }
                else { killif_left_alive = false; }

                if (Killif_Right_alive.Text == "Alive") { killif_right_alive = true; }
                else { killif_right_alive = false; }

                if (Killif_BottomLeft_alive.Text == "Alive") { killif_bottom_left_alive = true; }
                else { killif_bottom_left_alive = false; }

                if (Killif_BottomRight_alive.Text == "Alive") { killif_bottom_right_alive = true; }
                else { killif_bottom_right_alive = false; }

                if (Killif_Bottom_alive.Text == "Alive") { killif_bottom_alive = true; }
                else { killif_bottom_alive = false; }


                CellType newCellType = new CellType(name, reviveif_top_alive, reviveif_top_right_alive, reviveif_top_left_alive, reviveif_left_alive, reviveif_right_alive, reviveif_bottom_left_alive, reviveif_bottom_right_alive, reviveif_bottom_alive, killif_top_alive, killif_top_right_alive, killif_top_left_alive, killif_left_alive, killif_right_alive, killif_bottom_left_alive, killif_bottom_right_alive, killif_bottom_alive);
                listaTypeofCells.Add(newCellType);

                this.Close();
            }
            else
            {
                MessageBox.Show("This name is already in use. Please write a different one.");
            }
        }
    }
}

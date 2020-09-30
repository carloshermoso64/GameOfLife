using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheGameOfLife
{
    class Cell
    {
        public static List<Cell> Grid = new List<Cell>();  
        public bool alive { get; set; }
        public int coorX { get; set; }
        public int coorY { get; set; }
        public Button cellButton { get; set; }
        public string Name { get; set; }

        public enum CellAction
        {
            Die,
            Live,
        }

        private CellAction nextAction;

        public Cell(Button cellBtn)
        {
            cellButton = cellBtn;
            cellButton.Background = Brushes.Black;
            Grid.Add(this);

        }

        public void TurnOn()
        {
            cellButton.Background = Brushes.White;
            alive = true;

        }

        public void TurnOff()
        {
            cellButton.Background = Brushes.Black;
            alive = false;
        }

        public void NextAction(CellAction action)
        {
            nextAction = action;
        }

        public int CountNeighbours()
        {
            int count = 0;
            Cell up = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 0 && c.coorY == coorY + 1);
            Cell down = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 0 && c.coorY == coorY - 1);
            Cell left = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 1 && c.coorY == coorY + 0);
            Cell right = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 1 && c.coorY == coorY + 0);
            Cell upleft = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 1 && c.coorY == coorY + 1);
            Cell upright = Cell.Grid.FirstOrDefault(c => c.coorX == coorX + 1 && c.coorY == coorY + 1);
            Cell downleft = Cell.Grid.FirstOrDefault(c => c.coorX == coorX - 1 && c.coorY == coorY - 1);
            Cell downright = Cell.Grid.FirstOrDefault(c => c.coorX == coorX + 1 && c.coorY == coorY - 1);

            if (up.alive) { count++; }
            if (down.alive) { count++; }
            if (left.alive) { count++; }
            if (right.alive) { count++; }
            if (upleft.alive) { count++; }
            if (upright.alive) { count++; }
            if (downleft.alive) { count++; }
            if (downright.alive) { count++; }

            return count;
        }

        public void StepRules()
        {
            int number_of_neighbours = CountNeighbours();
            if (alive)
            {
                if (number_of_neighbours == 2 || number_of_neighbours == 3)
                {
                    NextAction(CellAction.Live);
                }
                else
                {
                    NextAction(CellAction.Die);
                }
            }
            else
            {
                if (number_of_neighbours == 3)
                {
                    NextAction(CellAction.Live);
                }
            }
        }

    }
}

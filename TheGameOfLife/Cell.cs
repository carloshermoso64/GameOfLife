﻿using System;
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
        public bool state { get; set; }
        public int coorX { get; set; }
        public int coorY { get; set; }
        public Button cellButton { get; set; }
        public enum CellAction
        {
            Die,
            Revive,
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
            state = true;

        }

        public void TurnOff()
        {
            cellButton.Background = Brushes.Black;
            state = false;
        }


    }
}
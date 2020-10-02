using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGameOfLife
{
    class MatrizdeCells
    {
        public int rows;
        public int columns;
        public Cell[,] matrix;


        public MatrizdeCells(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            Cell[,] matrix = new Cell[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = new Cell();
                }
            }

            this.matrix = matrix;
        }

        public int CountAliveNeibourghs(int i, int j) // meto matriz y coordenadas y devuelve el num de vecinos vivos
        {
            //boundary

            int left = j - 1;
            int right = j + 1;
            int down = i + 1;
            int up = i - 1;


            if (left < 0)
            {
                left = columns - 1;
            }
            if (right >= columns)
            {
                right = 0;
            }
            if (up < 0)
            {
                up = rows - 1;
            }
            if (down >= rows)
            {
                down = 0;
            }

            // Count neighbours

            int neighbours = 0;
            if (this.matrix[up, left].alive == true)
            { neighbours++; }
            if (this.matrix[up, j].alive == true)
            { neighbours++; }
            if (this.matrix[up, right].alive == true)
            { neighbours++; }
            if (this.matrix[i, left].alive == true)
            { neighbours++; }
            if (this.matrix[i, right].alive == true)
            { neighbours++; }
            if (this.matrix[down, left].alive == true)
            { neighbours++; }
            if (this.matrix[down, j].alive == true)
            { neighbours++; }
            if (this.matrix[down, right].alive == true)
            { neighbours++; }

            return neighbours;
        }
    }
}

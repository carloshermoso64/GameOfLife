using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TheGameOfLife
{
    class Cell
    {
        public bool alive;
        public Cell()
        {
            this.alive = false;
        }
    }
}

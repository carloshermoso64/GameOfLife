using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TheGameOfLife
{
    public class CellType
    {
        public string Name;

        public bool reviveif_top_alive;
        public bool reviveif_top_right_alive;
        public bool reviveif_top_left_alive;
        public bool reviveif_left_alive;
        public bool reviveif_right_alive;
        public bool reviveif_bottmo_left_alive;
        public bool reviveif_bottom_right_alive;
        public bool reviveif_bottom_alive;

        public bool killif_top_alive;
        public bool killif_top_right_alive;
        public bool killif_top_left_alive;
        public bool killif_left_alive;
        public bool killif_right_alive;
        public bool killif_bottmo_left_alive;
        public bool killif_bottom_right_alive;
        public bool killif_bottom_alive;

        public CellType(string Name, bool reviveif_top_alive, bool reviveif_top_right_alive, bool reviveif_top_left_alive, bool reviveif_left_alive, bool reviveif_right_alive, bool reviveif_bottmo_left_alive, bool reviveif_bottom_right_alive, bool reviveif_bottom_alive, bool killif_top_alive, bool killif_top_right_alive, bool killif_top_left_alive, bool killif_left_alive, bool killif_right_alive, bool killif_bottmo_left_alive, bool killif_bottom_right_alive, bool killif_bottom_alive)
        {
            this.Name = Name;

            this.reviveif_top_alive = reviveif_top_alive;
            this.reviveif_top_right_alive = reviveif_top_right_alive;
            this.reviveif_top_left_alive = reviveif_top_left_alive;
            this.reviveif_left_alive = reviveif_left_alive;
            this.reviveif_right_alive = reviveif_right_alive;
            this.reviveif_bottmo_left_alive = reviveif_bottmo_left_alive;
            this.reviveif_bottom_right_alive = reviveif_bottom_right_alive;
            this.reviveif_bottom_alive = reviveif_bottom_alive;

            this.killif_top_alive = killif_top_alive;
            this.killif_top_right_alive = killif_top_right_alive;
            this.killif_top_left_alive = killif_top_left_alive;
            this.killif_left_alive = killif_left_alive;
            this.killif_right_alive = killif_right_alive;
            this.killif_bottmo_left_alive = killif_bottmo_left_alive;
            this.killif_bottom_right_alive = killif_bottom_right_alive;
            this.killif_bottom_alive = killif_bottom_alive;
        }

    }
}

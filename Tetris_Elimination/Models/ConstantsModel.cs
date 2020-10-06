using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Models
{
    public static class ConstantsModel
    {
        public enum Screens
        {
            MENU,
            SINGLEPLAYER,
            MULTIPLAYER,
            SETTINGS
        }

        public enum Tetremino
        {
            PURPLE_T,
            BLUE_L,
            RED_S,
            GREEN_Z,
            YELLOW_O,
            BLUE_I,
            ORANGE_J
        }

        public enum Move
        {
            LEFT,
            RIGHT,
            DOWN
        }

        public static string cur_version = "Version: 0.4B";
    }
}

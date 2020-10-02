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

        public static string cur_version = "Version: 0.3B";
    }
}

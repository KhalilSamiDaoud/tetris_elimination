

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
            DOWN,
            ROTATE,
            SPAWN
        }

        public enum Sound
        {
            DROP,
            ROTATE,
            CLEARED_ROW,
            TIMER,
            TIMER_END,
            INTRO
        }
    }
}

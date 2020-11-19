using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace Tetris_Elimination.Models
{
    public static class ConstantsModel
    {

        public static string VISIBLE = "Visible";
        public static string HIDDEN  = "Hidden";

        public static string BACKGROUND          = "pack://application:,,,/Assets/Images/Background.png";
        public static string BACKGROUND_SETTINGS = "pack://application:,,,/Assets/Images/Background_Settings.png";
        public static string LOGO                = "pack://application:,,,/Assets/Images/Blue_Bottle.png";

        public static ImageBrush BORDER_TILE     = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Border_Tile.png", UriKind.Absolute)));
        public static ImageBrush BACKGROUND_TILE = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Background_Tile.png", UriKind.Absolute)));
        public static ImageBrush PURPLE_TILE     = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Purple_Tile.png", UriKind.Absolute)));
        public static ImageBrush BLUE_TILE       = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dark_Blue_Tile.png", UriKind.Absolute)));
        public static ImageBrush RED_TILE        = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Red_Tile.png", UriKind.Absolute)));
        public static ImageBrush GREEN_TILE      = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Green_Tile.png", UriKind.Absolute)));
        public static ImageBrush YELLOW_TILE     = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Yellow_Tile.png", UriKind.Absolute)));
        public static ImageBrush LIGHT_BLUE_TILE = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Blue_Tile.png", UriKind.Absolute)));
        public static ImageBrush ORANGE_TILE     = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Orange_Tile.png", UriKind.Absolute)));

        public static ImageBrush[] TILE_ARRAY = { BORDER_TILE, BACKGROUND_TILE,  PURPLE_TILE, BLUE_TILE, RED_TILE, GREEN_TILE, YELLOW_TILE, LIGHT_BLUE_TILE, ORANGE_TILE};

        public enum Screens
        {
            MENU,
            SINGLEPLAYER,
            MULTIPLAYER,
            MULTIPLAYER_MENU,
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

        public enum KeyBind
        {
            ROTATE,
            DOWN,
            LEFT,
            RIGHT,
            DROP,
            HOLD,
            PAUSE
        }
    }
}

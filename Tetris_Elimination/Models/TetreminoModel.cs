using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    class TetreminoModel : ITetreminoModel
    {
        private ImageBrush image = new ImageBrush();
        private Point position;
        private Point[] shape;
        public TetreminoModel(Tetremino piece)
        {
            spawn(piece);
        }
        public ImageBrush getBrush()
        {
            return image;
        }

        public Point getPosition()
        {
            return position;
        }

        public Point[] getShape()
        {
            return shape;
        }

        public Point[] spawn(Tetremino _piece)
        {

            position = new Point(0, -1); //all peices spawn at the same point 

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Purple_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.PURPLE_T);
                    return getShape();
                case Tetremino.BLUE_I:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Blue_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.BLUE_I);
                    return getShape();
                case Tetremino.BLUE_L:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Dark_Blue_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.BLUE_L);
                    return getShape();
                case Tetremino.GREEN_Z:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Green_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.GREEN_Z);
                    return getShape();
                case Tetremino.ORANGE_J:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Orange_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.ORANGE_J);
                    return getShape();
                case Tetremino.RED_S:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Red_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.RED_S);
                    return getShape();
                case Tetremino.YELLOW_O:
                    image.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Yellow_Tile.png", UriKind.Relative));
                    shape = initialize(Tetremino.YELLOW_O);
                    return getShape();
                default:
                    return null;
            }
        }

        public void move(Move direction)
        {
            switch (direction)
            {
                case Move.LEFT:
                    position.X--;
                    break;
                case Move.RIGHT:
                    position.X++;
                    break;
                case Move.DOWN:
                    position.Y++;
                    break;
                case Move.ROTATE:
                    for (int i = 0; i < shape.Length; i++)
                    {
                        double x = shape[i].X;
                        shape[i].X = shape[i].Y * -1;
                        shape[i].Y = x;
                    }
                    break;
            }
        }

        public Point[] initialize(Tetremino _piece)
        {

            Point[] temp = new Point[4];

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    temp[0] = new Point(0, 0);
                    temp[1] = new Point(-1, 0);
                    temp[2] = new Point(0, -1);
                    temp[3] = new Point(1, 0);

                    return temp;

                case Tetremino.BLUE_I:
                    temp[0] = new Point(-1, 0);
                    temp[1] = new Point(-1, -1);
                    temp[2] = new Point(-1, 1);
                    temp[3] = new Point(-1, 2);

                    return temp;

                case Tetremino.BLUE_L:
                    temp[0] = new Point(1, -1);
                    temp[1] = new Point(-1, 0);
                    temp[2] = new Point(0, 0);
                    temp[3] = new Point(1, 0);

                    return temp;

                case Tetremino.GREEN_Z:
                    temp[0] = new Point(0, 0);
                    temp[1] = new Point(-1, 0);
                    temp[2] = new Point(0, -1);
                    temp[3] = new Point(1, -1);

                    return temp;

                case Tetremino.ORANGE_J:
                    temp[0] = new Point(0, 0);
                    temp[1] = new Point(-1, 0);
                    temp[2] = new Point(1, 0);
                    temp[3] = new Point(1, -1);

                    return temp;
                case Tetremino.RED_S:
                    temp[0] = new Point(0, -1);
                    temp[1] = new Point(-1, -1);
                    temp[2] = new Point(0, 0);
                    temp[3] = new Point(1, 0);

                    return temp;

                case Tetremino.YELLOW_O:
                    temp[0] = new Point(-1, 0);
                    temp[1] = new Point(-1, -1);
                    temp[2] = new Point(0, 0);
                    temp[3] = new Point(0, -1);

                    return temp;

                default:
                    return null;
            }
        }
    }
}

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

            position = new Point(0, 0); //all peices spawn at the same point 

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Purple_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.PURPLE_T);
                    return getShape();
                case Tetremino.BLUE_I:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Blue_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.BLUE_I);
                    return getShape();
                case Tetremino.BLUE_L:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dark_Blue_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.BLUE_L);
                    return getShape();
                case Tetremino.GREEN_Z:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Green_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.GREEN_Z);
                    return getShape();
                case Tetremino.ORANGE_J:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Orange_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.ORANGE_J);
                    return getShape();
                case Tetremino.RED_S:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Red_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.RED_S);
                    return getShape();
                case Tetremino.YELLOW_O:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Yellow_Tile.png", UriKind.Absolute));
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
                        shape[i] = rotatePoint(shape[i], shape[2]);
                    }
                    break;
            }
        }
        //credit to Fraser StackOverflow
        public Point rotatePoint(Point pointToRotate, Point centerPoint)
        {
            double angleInRadians = 90 * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        public Point[] initialize(Tetremino _piece)
        {

            Point[] temp = new Point[4];

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    temp[0] = new Point(5, 2);
                    temp[1] = new Point(4, 1);
                    temp[2] = new Point(5, 1);
                    temp[3] = new Point(6, 1);

                    return temp;

                case Tetremino.BLUE_I:
                    temp[0] = new Point(4, 1);
                    temp[1] = new Point(5, 1);
                    temp[2] = new Point(6, 1);
                    temp[3] = new Point(7, 1);

                    return temp;

                case Tetremino.BLUE_L:
                    temp[0] = new Point(6, 1);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(5, 2);
                    temp[3] = new Point(6, 2);

                    return temp;

                case Tetremino.GREEN_Z:
                    temp[0] = new Point(5, 2);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(5, 1);
                    temp[3] = new Point(6, 1);

                    return temp;

                case Tetremino.ORANGE_J:
                    temp[0] = new Point(5, 2);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(6, 2);
                    temp[3] = new Point(6, 1);

                    return temp;
                case Tetremino.RED_S:
                    temp[0] = new Point(5, 1);
                    temp[1] = new Point(4, 1);
                    temp[2] = new Point(5, 2);
                    temp[3] = new Point(6, 2);

                    return temp;

                case Tetremino.YELLOW_O:
                    temp[0] = new Point(6, 1);
                    temp[1] = new Point(5, 1);
                    temp[2] = new Point(6, 2);
                    temp[3] = new Point(5, 2);

                    return temp;

                default:
                    return null;
            }
        }
    }
}

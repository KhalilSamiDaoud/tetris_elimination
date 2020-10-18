

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    public class TetreminoModel : ITetreminoModel
    {
        private ImageBrush image = new ImageBrush();
        private Point position;
        private Point[] shape;
        private Tetremino type;
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

        public Tetremino getType()
        {
            return type;
        }

        private void spawn(Tetremino _piece)
        {

            position = new Point(0, 0);
            type = _piece;

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Purple_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.PURPLE_T);
                    break;
                case Tetremino.BLUE_I:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Blue_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.BLUE_I);
                    break;
                case Tetremino.BLUE_L:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Dark_Blue_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.BLUE_L);
                    break;
                case Tetremino.GREEN_Z:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Green_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.GREEN_Z);
                    break;
                case Tetremino.ORANGE_J:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Orange_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.ORANGE_J);
                    break;
                case Tetremino.RED_S:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Red_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.RED_S);
                    break;
                case Tetremino.YELLOW_O:
                    image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Yellow_Tile.png", UriKind.Absolute));
                    shape = initialize(Tetremino.YELLOW_O);
                    break;
                default:
                    break;
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
        //credit to Fraser at StackOverflow for this algorithm!
        public Point rotatePoint(Point pointToRotate, Point centerPoint)
        {
            double angleInRadians = 90 * (Math.PI / 180);
            double cosTheta       = Math.Cos(angleInRadians);
            double sinTheta       = Math.Sin(angleInRadians);
            return new Point
            {
                X = (int)
                    Math.Round((cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X)),
                Y = (int)
                    Math.Round((sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y))
            };
        }

        private Point[] initialize(Tetremino _piece)
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
                    temp[0] = new Point(4, 1);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(5, 2);
                    temp[3] = new Point(6, 2);

                    return temp;

                case Tetremino.GREEN_Z:
                    temp[0] = new Point(5, 1);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(5, 2);
                    temp[3] = new Point(6, 1);

                    return temp;

                case Tetremino.ORANGE_J:
                    temp[0] = new Point(6, 2);
                    temp[1] = new Point(4, 2);
                    temp[2] = new Point(5, 2);
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

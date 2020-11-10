using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Media;
using System.Windows;
using System;

namespace Tetris_Elimination.Models
{
    public class TetreminoModel
    {
        private ImageBrush image;
        private Tetremino type;
        private Point position;
        private Point[] shape;
        public TetreminoModel(Tetremino piece)
        {
            image = new ImageBrush();

            Spawn(piece);
        }
        public ImageBrush GetBrush()
        {
            return image;
        }

        public Point GetPosition()
        {
            return position;
        }

        public Point[] GetShape()
        {
            return shape;
        }

        public new Tetremino GetType()
        {
            return type;
        }

        private void Spawn(Tetremino _piece)
        {

            position = new Point(0, 0);
            type     = _piece;

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    image = PURPLE_TILE;
                    shape = Initialize(Tetremino.PURPLE_T);
                    break;
                case Tetremino.BLUE_I:
                    image = LIGHT_BLUE_TILE;
                    shape = Initialize(Tetremino.BLUE_I);
                    break;
                case Tetremino.BLUE_L:
                    image = BLUE_TILE;
                    shape = Initialize(Tetremino.BLUE_L);
                    break;
                case Tetremino.GREEN_Z:
                    image = GREEN_TILE;
                    shape = Initialize(Tetremino.GREEN_Z);
                    break;
                case Tetremino.ORANGE_J:
                    image = ORANGE_TILE;
                    shape = Initialize(Tetremino.ORANGE_J);
                    break;
                case Tetremino.RED_S:
                    image = RED_TILE;
                    shape = Initialize(Tetremino.RED_S);
                    break;
                case Tetremino.YELLOW_O:
                    image = YELLOW_TILE;
                    shape = Initialize(Tetremino.YELLOW_O);
                    break;
                default:
                    break;
            }
        }

        public void MovePoint(Move direction)
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
                        shape[i] = RotatePoint(shape[i], shape[2]);
                    }
                    break;
            }
        }

        //credit to Fraser at StackOverflow for this algorithm!
        public Point RotatePoint(Point pointToRotate, Point centerPoint)
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

        private Point[] Initialize(Tetremino _piece)
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

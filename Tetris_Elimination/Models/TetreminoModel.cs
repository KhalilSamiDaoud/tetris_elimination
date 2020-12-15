using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Media;
using System.Windows;
using System;

namespace Tetris_Elimination.Models
{
    /// <summary>The TetreminoModel is a data type used by ViewModels. It contains the pieces position, color, type, and shape.</summary>
    public class TetreminoModel
    {
        private ImageBrush image;
        private Tetremino type;
        private Point position;
        private Point[] shape;

        /// <summary>Initializes a new instance of the <see cref="TetreminoModel" /> class.</summary>
        /// <param name="piece">The piece.</param>
        public TetreminoModel(Tetremino piece)
        {
            image = new ImageBrush();

            Spawn(piece);
        }

        /// <summary>Gets the brush.</summary>
        /// <returns>The Image Object.</returns>
        public ImageBrush GetBrush()
        {
            return image;
        }

        /// <summary>Gets the position.</summary>
        /// <returns>The position of the piece as a Point.</returns>
        public Point GetPosition()
        {
            return position;
        }

        /// <summary>Gets the shape.</summary>
        /// <returns>The shape of the piece as a Point array.</returns>
        public Point[] GetShape()
        {
            return shape;
        }

        /// <summary>Gets the type.</summary>
        /// <returns>The type of the piece as a Tetremino Enum</returns>
        public new Tetremino GetType()
        {
            return type;
        }

        /// <summary>Spawns the specified piece.</summary>
        /// <param name="_piece">The piece to be created.</param>
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

        /// <summary>Change the pieces position based on the direction.</summary>
        /// <param name="direction">The direction to move, or to rotate.</param>
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


        /// <summary>Rotates the piece around a point.</summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A new Point object.</returns>
        /// <remarks>credit to Fraser at StackOverflow for this algorithm!</remarks>
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

        /// <summary>Initializes the specified piece with a Point array.</summary>
        /// <param name="_piece">The piece to initialize.</param>
        /// <returns></returns>
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

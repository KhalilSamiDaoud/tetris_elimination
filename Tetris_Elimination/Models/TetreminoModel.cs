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
        private ImageBrush image;
        private Point position;
        private Point[] shape;
        public TetreminoModel(Tetremino piece)
        {
            spawn(piece); //will set the image, position, and shape based on the enum
        }
        public ImageBrush getBrush()
        {
            return this.image;
        }

        public Point getPosition()
        {
            return this.position;
        }

        public Point[] getShape()
        {
            return this.shape;
        }

        public Point[] spawn(Tetremino _piece)
        {

            this.position = new Point(0, -1); //all peices spawn at the same point 

            switch (_piece)
            {
                case Tetremino.PURPLE_T:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Purple_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.PURPLE_T);
                    return getShape();
                case Tetremino.BLUE_I:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Blue_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.BLUE_I);
                    return getShape();
                case Tetremino.BLUE_L:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Dark_Blue_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.BLUE_L);
                    return getShape();
                case Tetremino.GREEN_Z:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Green_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.GREEN_Z);
                    return getShape();
                case Tetremino.ORANGE_J:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Orange_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.ORANGE_J);
                    return getShape();
                case Tetremino.RED_S:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Red_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.RED_S);
                    return getShape();
                case Tetremino.YELLOW_O:
                    this.image.ImageSource = new BitmapImage(new Uri("/Assets/Images/Yellow_Tile.png", UriKind.Relative));
                    this.shape = initialize(Tetremino.YELLOW_O);
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
            }
        }

        public void rotate()
        {
            for (int i = 0; i < shape.Length; i++)
            {
                double x = shape[i].X;
                shape[i].X = shape[i].Y * -1;
                shape[i].Y = x;
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
                    temp[3] = new Point(1, -1);

                    return temp;

                case Tetremino.BLUE_I:
                    temp[0] = new Point(0, -1);
                    temp[1] = new Point(-1, -1);
                    temp[2] = new Point(1, 1);
                    temp[3] = new Point(2, -1);

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
                    temp[3] = new Point(1, 0);

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
                    temp[0] = new Point(0, 0);
                    temp[1] = new Point(0, -1);
                    temp[2] = new Point(1, 0);
                    temp[3] = new Point(1, -1);

                    return temp;

                default:
                    return null;
            }
        }
    }
}

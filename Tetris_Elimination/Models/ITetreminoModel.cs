using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    interface ITetreminoModel
    {
        ImageBrush getBrush();

        Point getPosition();

        Point[] getShape();

        Tetremino getType();

        void move(Move direction);

        Point[] spawn(Tetremino piece);




    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToeLib.AI
{
    interface IMoveGenerator
    {
        Point Generate(Board board);
    }
}

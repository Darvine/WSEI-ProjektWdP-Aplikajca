using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToeLib.AI
{
    class RandomMoveGenerator : IMoveGenerator
    {
        public Point Generate(Board board)
        {
            Random random = new Random();
            List<Point> freeSlots = new List<Point>();
            
            for (int x = 0; x < board.Size; x++)
            {
                for (int y = 0; y < board.Size; y++)
                {
                    if (board.GetSlotValue(x,y) == Enum.BoardSlotValues.NONE)
                        freeSlots.Add(new Point(x, y));
                }
            }

            return freeSlots[random.Next(0, freeSlots.Count - 1)];
        }
    }
}

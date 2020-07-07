using System;
using TicTacToeLib.Enum;

namespace TicTacToeLib.Event
{
    public class SlotValueChangedEventArgs : EventArgs
    {
        public int x { get; set; }
        public int y { get; set; }
        public BoardSlotValues value { get; set; }
    }
}

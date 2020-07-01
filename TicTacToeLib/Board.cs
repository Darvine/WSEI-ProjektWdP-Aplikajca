using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLib
{
    public class Board
    {
        private int Size;

        public enum Values
        {
            NONE = 0,
            CROSS,
            CICRLE
        }

        private List<Values> Slots = new List<Values>();

        public Board(int size)
        {
            size = Size;
            Init();

        }

        private void Init()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Slots.Add(Values.NONE);
                }
            }
        }

        public Values GetSlotValue(int x, int y)
        {
            return Slots[y * Size + x];
        }

        public void SetSlotValue(int x, int y, Values value)
        {
             Slots[y * Size + x] = value;
        }

        public void CheckGameEnded()
        {
            bool isEmpty = false;
            foreach(Values value in Slots)
            {
                if (value == Values.NONE)
                    isEmpty = true;
            }
            if (!isEmpty)
            {

            }
        }

        public bool CheckIsWon()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Slots.Add(Values.NONE);
                }
            }
        }


    }
}

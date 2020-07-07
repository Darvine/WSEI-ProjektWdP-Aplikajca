using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLib.Enum;
using TicTacToeLib.Event;

namespace TicTacToeLib
{
    public class Board
    {
        private int Size;
        private GameStates _state;
        public GameStates State
        {
            get => _state;
            private set
            {
                _state = value;
                OnStateChanged();
            }
        }

        private BoardSlotValues PlayerValue = BoardSlotValues.CROSS;

        public event EventHandler<SlotValueChangedEventArgs> OnSlotValueChange;
        public event EventHandler<EventArgs> OnStateChange;
        private List<BoardSlotValues> Slots = new List<BoardSlotValues>();

        public Board(int size)
        {
            Size = size;
            State = GameStates.InProgress;
            Init();
        }

        private void Init(bool propagateChange = false)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Slots.Add(BoardSlotValues.NONE);
                    if (propagateChange)
                        OnSlotValueChanged(new SlotValueChangedEventArgs() { x = x, y = y, value = BoardSlotValues.NONE });
                }
            }
        }

        public void Clear()
        {
            Slots.Clear();
            Init(true);
        }

        public BoardSlotValues GetSlotValue(int x, int y)
        {
            return Slots[y * Size + x];
        }

        public void SetSlotValue(int x, int y, BoardSlotValues value)
        {
            Slots[y * Size + x] = value;
            OnSlotValueChanged(new SlotValueChangedEventArgs() { x = x, y = y, value = value });

            CheckIsWon(x, y, value);
            CheckGameEnded();
        }

        public void CheckGameEnded()
        { 
            if (State != GameStates.InProgress)
                return;

            bool isEmpty = false;
            foreach(BoardSlotValues value in Slots)
            {
                if (value == BoardSlotValues.NONE)
                    isEmpty = true;
            }

            if (!isEmpty)
            {
                State = GameStates.Draw;
            }  
        }
       
        public void CheckIsWon(int x, int y, BoardSlotValues value)
        {
            for (int i = 0; i < Size; i++)
            {
                if (GetSlotValue(x, i) != value)
                    break;

                if (i == Size-1)
                {
                    SetWon(value);
                    return;
                }
            }

            for (int i = 0; i < Size; i++)
            {
                if (GetSlotValue(i, y) != value)
                    break;

                if (i == Size-1)
                {
                    SetWon(value);
                    return;
                }
            }

            if (x == y)
            {
                for (int i = 0; i < Size; i++)
                {
                    if (GetSlotValue(i, i) != value)
                        break;

                    if (i == Size - 1)
                    {
                        SetWon(value);
                        return;
                    }
                }
            }

            if (x + y == Size-1)
            {
                for (int i = 0; i < Size; i++)
                {
                    if (GetSlotValue(i, (Size-1)-i) != value)
                        break;

                    if (i == Size - 1)
                    {
                        SetWon(value);
                        return;
                    }
                }
            }
        }

        public void SetWon(BoardSlotValues value)
        {
            if (value == PlayerValue)
                State = GameStates.Won;
            else
                State = GameStates.Lost;
        }

        protected virtual void OnSlotValueChanged(SlotValueChangedEventArgs e)
        {
            EventHandler<SlotValueChangedEventArgs> handler = OnSlotValueChange;
            handler?.Invoke(this, e);
        }

        protected virtual void OnStateChanged()
        {
            EventHandler<EventArgs> handler = OnStateChange;
            handler?.Invoke(this, new EventArgs());
        }
    }
}

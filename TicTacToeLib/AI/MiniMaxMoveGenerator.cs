using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TicTacToeLib.Enum;

namespace TicTacToeLib.AI
{
    class MiniMaxMoveGenerator : IMoveGenerator
    {
        public Point Generate(Board board)
        {
            BoardSlotValues[,] slotsArray = ConvertSlotsToArray(board.Slots);
            int[] bestMove = MiniMax(board, slotsArray, GetFreeSlotsCount(slotsArray), board.AIValue);

            return new Point(bestMove[0], bestMove[1]);
        }

        private int[] MiniMax(Board board, BoardSlotValues[,] slots, int emptySlots, BoardSlotValues playerType)
        {
            int[] bestMove;

            if(playerType == board.AIValue)
                bestMove = new int[3] { -1, -1, -1000 };
            else
                bestMove = new int[3] { -1, -1, 1000 };

            if (emptySlots == 0 || IsGameOver(slots, board.AIValue) || IsGameOver(slots, board.PlayerValue))
            {
                return EvaluateScore(board, slots);
            }

            foreach (KeyValuePair<Point, BoardSlotValues> kv in GetFreeSlots(slots))
            {
                int x = kv.Key.X;
                int y = kv.Key.Y;

                slots[x, y] = playerType;
                int[] score = MiniMax(board, slots, emptySlots - 1, playerType == board.AIValue ? board.PlayerValue : board.AIValue);
                slots[x, y] = 0;
                score[0] = x;
                score[1] = y;

                if (playerType == board.AIValue && score[2] > bestMove[2])
                    bestMove = score;
                else if(score[2] < bestMove[2])
                    bestMove = score;
            }

            return bestMove;
        }

        private int[] EvaluateScore(Board board, BoardSlotValues[,] slots)
        {
            int score = 0;

            if (IsGameOver(slots, board.AIValue))
                score = 1;
            else if (IsGameOver(slots, board.PlayerValue))
                score = -1;
            else
                score = 0;

            return new int[3] { -1, -1, score};
        }

        private BoardSlotValues[,] ConvertSlotsToArray(List<BoardSlotValues> slots)
        {
            int size = (int)Math.Sqrt(slots.Count);
            BoardSlotValues[,] slotsArray = new BoardSlotValues[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    slotsArray[x, y] = slots[y * size + x];
                }
            }

            return slotsArray;
        }

        private Dictionary<Point, BoardSlotValues> GetFreeSlots(BoardSlotValues[,] slotsArray)
        {
            Dictionary<Point, BoardSlotValues> freeSlots = new Dictionary<Point, BoardSlotValues>();

            for (int x = 0; x < slotsArray.GetLength(0); x++)
            {
                for (int y = 0; y < slotsArray.GetLength(1); y++)
                {
                    if (slotsArray[x, y] == Enum.BoardSlotValues.NONE)
                        freeSlots.Add(new Point(x, y), slotsArray[x, y]);
                }
            }

            return freeSlots;
        }

        private int GetFreeSlotsCount(BoardSlotValues[,] slotsArray)
        {
            int count = 0;

            for (int x = 0; x < slotsArray.GetLength(0); x++)
            {
                for (int y = 0; y < slotsArray.GetLength(1); y++)
                {
                    if (slotsArray[x, y] == Enum.BoardSlotValues.NONE)
                        count++;
                }
            }

            return count;
        }

        private bool IsGameOver(BoardSlotValues[,] slotsArray, BoardSlotValues playerType)
        {
            BoardSlotValues[,] winStates = new BoardSlotValues[8,3] {
                { slotsArray[0,0], slotsArray[0,1], slotsArray[0,2] },
                { slotsArray[1,0], slotsArray[1,1], slotsArray[1,2] },
                { slotsArray[2,0], slotsArray[2,1], slotsArray[2,2] },
                { slotsArray[0,0], slotsArray[1,0], slotsArray[2,0] },
                { slotsArray[0,1], slotsArray[1,1], slotsArray[2,1] },
                { slotsArray[0,2], slotsArray[1,2], slotsArray[2,2] },
                { slotsArray[0,0], slotsArray[1,1], slotsArray[2,2] },
                { slotsArray[2,0], slotsArray[1,1], slotsArray[0,2] }
            };

            for(int i = 0;i < winStates.GetLength(0); i++)
            {
                int filled = 0;
                for(int j = 0; j < winStates.GetLength(1); j++)
                {
                    if (winStates[i,j] == playerType)
                    {
                        filled++;
                    }
                }

                if (filled == 3)
                    return true;
            }

            return false;
        }
    }

   
}

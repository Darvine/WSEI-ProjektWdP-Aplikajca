using System.Collections.Generic;
using TicTacToeLib.Enum;
using LibGameStates = TicTacToeLib.Enum.GameStates;
using LibBoardSlotValues = TicTacToeLib.Enum.BoardSlotValues;

namespace Projekt
{
    public static class Translator
    {
        public static Dictionary<LibGameStates, string> GameStates = new Dictionary<LibGameStates, string>() 
        {
            { LibGameStates.InProgress, "W trakcie" },
            { LibGameStates.Draw, "Remis" },
            { LibGameStates.Won, "Wygrana" },
            { LibGameStates.Lost, "Przegrana" }
        };

        public static Dictionary<LibBoardSlotValues, string> BoardSlotValues = new Dictionary<LibBoardSlotValues, string>()
        {
            { LibBoardSlotValues.CICRLE, "O" },
            { LibBoardSlotValues.CROSS, "X" },
            { LibBoardSlotValues.NONE, "" }
        };
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TicTacToeLib;
using TicTacToeLib.Enum;
using TicTacToeLib.Event;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();

        private Board GameBoard;

        private Dictionary<GameStates, int> Scores = new Dictionary<GameStates, int>() {
            { GameStates.Won, 0 },
            { GameStates.Lost, 0 },
            { GameStates.Draw, 0 }
        };

        public MainWindow()
        {
            InitializeComponent();
            GameBoard = new Board(3);
            GameBoard.OnSlotValueChange += OnBoardPlaceValueChange;
            GameBoard.OnStateChange += OnGameStateChange;
            UpdateScores();
        }

        private void BoardPlace_Click(object sender, RoutedEventArgs e)
        {
            int x = (int)(sender as FrameworkElement).GetValue(Grid.ColumnProperty);
            int y = (int)(sender as FrameworkElement).GetValue(Grid.RowProperty);

            GameBoard.SetSlotValue(x, y, BoardSlotValues.CROSS);
        }

        private void OnBoardPlaceValueChange(object sender, SlotValueChangedEventArgs e)
        {
            GetBoardPlace(e.x, e.y).SetValue(Button.ContentProperty, Translator.BoardSlotValues[e.value]);
            GetBoardPlace(e.x, e.y).IsEnabled = false;
        }

        private void OnGameStateChange(object sender, EventArgs e)
        {
            if (GameBoard.State == GameStates.Won)
            {
                InfoWin.Visibility = Visibility.Visible;
                Scores[GameStates.Won]++; 
            }
            else if (GameBoard.State == GameStates.Lost)
            {
                InfoDefeat.Visibility = Visibility.Visible;
                Scores[GameStates.Lost]++;
            }
            else if (GameBoard.State == GameStates.Draw)
            {
                InfoDraw.Visibility = Visibility.Visible;
                Scores[GameStates.Draw]++;
            }

            UpdateScores();
        }

        private void UpdateScores()
        {
            ScoreWin.Text = $"Wygrano {Scores[GameStates.Won]}";
            ScoreDefeat.Text = $"Przegrano {Scores[GameStates.Lost]}";
            ScoreDraw.Text = $"Remis {Scores[GameStates.Draw]}";
        }

        private FrameworkElement GetBoardPlace(int x, int y)
        {
            foreach (FrameworkElement el in GameArea.Children)
            {
                int cx = (int)el.GetValue(Grid.ColumnProperty);
                int cy = (int)el.GetValue(Grid.RowProperty);

                if (cx == x && cy == y)
                    return el;
            }

            throw new Exception("Board place not found");
        }

        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            InfoDraw.Visibility = Visibility.Hidden;
            InfoWin.Visibility = Visibility.Hidden;
            InfoDefeat.Visibility = Visibility.Hidden;

            GameBoard.Clear();

            foreach (FrameworkElement el in GameArea.Children)
            {
                el.IsEnabled = true;
            }
        }
    }
}

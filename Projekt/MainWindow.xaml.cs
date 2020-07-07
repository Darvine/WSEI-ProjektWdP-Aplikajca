using System;
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

        public MainWindow()
        {
            InitializeComponent();
            GameBoard = new Board(3);
            GameBoard.OnSlotValueChange += OnBoardPlaceValueChange;
            GameBoard.OnStateChange += OnGameStateChange;
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
        }

        private void OnGameStateChange(object sender, EventArgs e)
        {
            if (GameBoard.State != GameStates.InProgress)
                MessageBox.Show(Translator.GameStates[GameBoard.State]);
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
            GameBoard.Clear();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true; // Флаг, указывающий, чей ход (true - Х, false - О)
        private bool isGameEnded = false; // Флаг, указывающий, закончилась ли игра
        private Button[] buttons; // Массив кнопок игрового поля

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            foreach (Button button in buttons)
            {
                button.IsEnabled = true;
                button.Content = string.Empty;
                button.Click += Button_Click;
            }
            start.IsEnabled = true;
            clear.IsEnabled = true;
            isGameEnded = false;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            isPlayerXTurn = true; 
            start.IsEnabled = true;
            clear.IsEnabled = true;
            if (!isPlayerXTurn)
            {
                MakeRobotMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!isGameEnded)
            {
                Button button = (Button)sender;
                if (button.IsEnabled)
                {
                    if (isPlayerXTurn)
                    {
                        button.Content = "X";
                    }
                    else
                    {
                        button.Content = "O";
                    }
                    button.IsEnabled = false;
                    CheckGameStatus();
                    isPlayerXTurn = !isPlayerXTurn;
                    if (!isGameEnded && !isPlayerXTurn)
                    {
                        MakeRobotMove();
                    }
                }
            }
        }

        private void MakeRobotMove()
        {
            Random random = new Random();
            int index = random.Next(0, 9);
            while (!buttons[index].IsEnabled)
            {
                index = random.Next(0, 9);
            }
            buttons[index].Content = "O";
            buttons[index].IsEnabled = false;
            CheckGameStatus();
            isPlayerXTurn = !isPlayerXTurn; 
        }

    private void CheckGameStatus()
    {
        string[,] board = new string[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = (string)buttons[i * 3 + j].Content;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != "" && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                EndGame(board[i, 0] + " выиграли!");
                return;
            }
            if (board[0, i] != "" && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                EndGame(board[0, i] + " выиграли!");
                return;
            }
        }

        if (board[0, 0] != "" && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            EndGame(board[0, 0] + " выиграли!");
            return;
        }
        if (board[0, 2] != "" && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            EndGame(board[0, 2] + " выиграли!");
            return;
        }

        bool isBoardFull = true;
        foreach (Button button in buttons)
        {
            if (button.Content.ToString() == "")
            {
                isBoardFull = false;
                break;
            }
        }
        if (isBoardFull)
        {
            EndGame("Ничья!");
            return;
        }
    }

    private void EndGame(string result)
    {
        isGameEnded = true;
        MessageBox.Show(result);
    }

}
}


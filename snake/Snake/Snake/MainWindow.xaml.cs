using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace Snake
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private fields
        private int row = 22, col = 22;
        private Direction direction = Direction.Left;
        private DispatcherTimer timer = new DispatcherTimer();
        private int canvasWidth = 1251, canvasHeight = 660;
        Board game;
        private string playerName;
        private int level;

        public MainWindow()
        {
            InitializeComponent();
            // Initalize game board and timer
            game = new Board(row, col);
            PaintBoard(game.GameBoard);

            // Initalize game board and timer
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += timer_Tick;
            EnsureGameWindowFocus();
            MakeComboBox();
        }

        private void EnsureGameWindowFocus()
        {
            GameWindow.Focusable = true;
            LevelBox.Focusable = false;
            startButton.Focusable = false;
            highScore.Focusable = false;
            ApplesLabel.Focusable = false;
            FocusManager.SetIsFocusScope(GameWindow, true);
            Keyboard.Focus(GameWindow);
        }

        private void MakeComboBox()
        {
            for (int i = 1; i < 6; i++)
            {
                LevelBox.Items.Add("Level " + i);
            }

        }

        // This is the event handler for the timer, moving the snake
        // and updating its position on the board.  The Canvas is cleared
        // off and is repainted again.
        private void timer_Tick(object sender, EventArgs e)
        {
            game.UpdateBoard(direction);
            ApplesLabel.Content = game.ApplesEaten;
            Canvas.Children.Clear();
            PaintBoard(game.GameBoard);

            if (game.HasCrashed)
            { 
                MessageBox.Show(string.Format("Game Over! Apples eaten: {0}", game.ApplesEaten));
                timer.Stop();
            }

        }

        // Takes in a game piece and initalizes its height, width, and color
        // Sets the game piece coordinates and adds it to the 
        // gui window
        private void PaintGamePiece(Shape gamePiece, int height, int width, int xCoord, int yCoord, Brush color)
        {
            gamePiece.Height = height;
            gamePiece.Width = width;
            gamePiece.Fill = color;

            Canvas.SetLeft(gamePiece, xCoord);
            Canvas.SetBottom(gamePiece, yCoord);

            Canvas.Children.Add(gamePiece);

        }

        // Correlates each value of the board to a
        // component: blue square for wall, 
        // green square for snake body, red ellipse
        // for apple, and yellow ellipse for bomb.
        // the x and y coordinates are based on the height
        // and width of the Canvas in MainWindow.xaml
        private void PaintBoard(SnakePiece[,] board)
        {
            int startX = canvasWidth / 8;
            int yCoord = canvasHeight;
            int wallWidth = 40;
            int wallHeight = 30;
            int snakeSize = wallHeight - 2;
            int appleSize = wallWidth - 15;


            for (int i = 0; i < board.GetLength(1); i++)
            {
                int xCoord = startX;

                for (int j = 0; j < board.GetLength(0); j++)
                {
                    // draw a blue rectangle for the wall
                    if (board[i, j] == SnakePiece.Wall)
                    {

                        Rectangle wall = new Rectangle();

                        PaintGamePiece(wall, wallHeight, wallWidth, xCoord, yCoord, Brushes.Blue);

                    }
                    // draw a green rectangle for the snake body
                    else if (board[i, j] == SnakePiece.Body)
                    {
                        Rectangle snakeBody = new Rectangle();

                        PaintGamePiece(snakeBody, snakeSize, snakeSize + 7, xCoord, yCoord, Brushes.Green);

                    }
                    // draw a red ellipse for an apple
                    else if (board[i, j] == SnakePiece.Apple)
                    {
                        Ellipse apple = new Ellipse();

                        PaintGamePiece(apple, appleSize, appleSize, xCoord, yCoord, Brushes.Red);

                    }
                    // draw a yellow ellipse for a bomb
                    else if (board[i, j] == SnakePiece.Bomb)
                    {
                        Ellipse bomb = new Ellipse();

                        PaintGamePiece(bomb, appleSize, appleSize, xCoord, yCoord, Brushes.Yellow);

                    }

                    // update x and y coordinates
                    xCoord += wallWidth;
                }

                yCoord -= wallHeight;
            }
        }

        // Key listener handler event from the Window's property
        // Detect arrows key being pushed and correlates the 
        // direction of the snake with the keys.
        private void MoveSnake(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                direction = Direction.Up;

            }
            else if (e.Key == Key.Down)
            {
                direction = Direction.Down;

            }
            else if (e.Key == Key.Right)
            {
                direction = Direction.Right;

            }
            else if (e.Key == Key.Left)
            {
                direction = Direction.Left;
            }
        }

        // Event handler for the Start button in the Gui, able to restart the 
        // game after the snake has crashed.
        private void StartGame(object sender, RoutedEventArgs e)
        {
            if (game.HasCrashed)
            {
                game = new Board(row, col);
                direction = Direction.Left;
            }

            playerName = Interaction.InputBox("Player's name: ", "Snake!", "Challenger");


            if (playerName != "")
            {
                PlayerLabel.Content = playerName;
                timer.Start();

            }

        }

        private void HighScore(object sender, RoutedEventArgs e)
        {
            HighScore newWindow = new HighScore();
            newWindow.Show();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            level = LevelBox.SelectedIndex;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200 - (level * 35));
        }

    }
}


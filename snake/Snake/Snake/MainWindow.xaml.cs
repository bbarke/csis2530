﻿using System;
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
using System.Threading;


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
        private int level;
        Board game;
        Player p1 = new Player();
        private Random rand = new Random();
        private string playerName = "MrSnake";
        private SolidColorBrush snakeColor = Brushes.Green;

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
            ColorBox.Focusable = false;
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

            ColorBox.Items.Add("Gold");
            ColorBox.Items.Add("Black");
            ColorBox.Items.Add("Green");
            ColorBox.Items.Add("Orange");
            ColorBox.Items.Add("Turquoise");
            ColorBox.Items.Add("Blue Violet");
            ColorBox.Items.Add("Violet");
            ColorBox.Items.Add("Pink");
            
           
            ColorBox.SelectedIndex = 2;
           

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
                p1.SavePlayerScore(playerName, game.ComputeScore());
                MessageBox.Show("Game Over!");
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

        private void PaintPic(string source, int xCoord, int yCoord, int width, int height)
        {
            string ImagesPath = source;
            Uri uri = new Uri(ImagesPath, UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmap;
            
            img.Width = width;
            img.Height = height;
            Canvas.SetLeft(img, xCoord);
            Canvas.SetBottom(img, yCoord);

            Canvas.Children.Add(img);

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

                        //PaintGamePiece(wall, wallHeight, wallWidth, xCoord, yCoord, Brushes.Blue);
                        PaintPic("Images/wall2.jpg", xCoord, yCoord, 40, 30);

                    }
                    // draw a green rectangle for the snake body
                    else if (board[i, j] == SnakePiece.Body)
                    {
                        Rectangle snakeBody = new Rectangle();

                        PaintGamePiece(snakeBody, snakeSize, snakeSize + 7, xCoord, yCoord, snakeColor);

                    }
                    // draw a red ellipse for an apple
                    else if (board[i, j] == SnakePiece.Apple)
                    {
                        Ellipse apple = new Ellipse();

                        //PaintGamePiece(apple, appleSize, appleSize, xCoord, yCoord, Brushes.Red);
                        PaintPic("Images/apple.jpg", xCoord, yCoord, 37, 30);

                    }
                    // draw a yellow ellipse for a bomb
                    else if (board[i, j] == SnakePiece.Bomb)
                    {
                        Ellipse bomb = new Ellipse();

                        //PaintGamePiece(bomb, appleSize, appleSize, xCoord, yCoord, Brushes.Yellow);
                        PaintPic("Images/bomb.jpg", xCoord, yCoord, 37, 30);

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

            playerName = Interaction.InputBox("Welcome to Snake! Use the arrow keys to move the " + 
                "snake around and eat as many red apples as possible to increase your score. Game " + 
                "is over when the snake runs into a yellow circle, the wall or its own body. Try " + 
                "to beat the high score and have fun! \n\n\nPlayer's name: ", "Snake!", playerName);

            if (playerName != "")
            {
                PlayerLabel.Content = playerName;
                Thread.Sleep(1000);
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

        private void ColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(ColorBox.SelectedIndex)
            {
                case 0:
                    snakeColor = Brushes.Gold;
                    break;
                case 1:
                    snakeColor = Brushes.Black;
                    break;
                case 2:
                    snakeColor = Brushes.Green;
                    break;
                case 3:
                    snakeColor = Brushes.OrangeRed;
                    break;
                case 4:
                    snakeColor = Brushes.Aqua;
                    break;
                case 5:
                    snakeColor = Brushes.BlueViolet;
                    break;
                case 6:
                    snakeColor = Brushes.Violet;
                    break;
                case 7:
                    snakeColor = Brushes.DeepPink;
                    break;

            }
        }

    }
}


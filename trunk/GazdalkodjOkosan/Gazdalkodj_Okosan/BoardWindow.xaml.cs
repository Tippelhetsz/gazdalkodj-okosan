using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private PlayerBoardStatus[] PlayerPieces;

        private Storyboard PieceAnimationStory;

        private DispatcherTimer AnimationTimer;

        private int Rolled, ToGo, CurrentPlayer, TotalPlayers;

        public BoardWindow(int NumerOfPlayers = 4)
        {
            TotalPlayers = NumerOfPlayers;
            CurrentPlayer = 0;
            InitializeComponent();
            InitializePieceList();
            AnimationTimer = new DispatcherTimer();
            AnimationTimer.Interval = TimeSpan.FromSeconds(1.5);
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            //MovePiece();
        }

        private void InitializePieceList()
        {
            PlayerPieces = new PlayerBoardStatus[6];
            PlayerPieces[0] = new PlayerBoardStatus(1, 0, Player1Piece);
            PlayerPieces[1] = new PlayerBoardStatus(2, 0, Player2Piece);
            PlayerPieces[2] = new PlayerBoardStatus(3, 0, Player3Piece);
            PlayerPieces[3] = new PlayerBoardStatus(4, 0, Player4Piece);
            PlayerPieces[4] = new PlayerBoardStatus(5, 0, Player5Piece);
            PlayerPieces[5] = new PlayerBoardStatus(6, 0, Player6Piece);
            for (int i = TotalPlayers; i < 6; ++i)
            {
                PlayerPieces[i].PlayerEllipse.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (ToGo > 1)
            {
                ToGo--;
                MovePiece();
            }
            else
            {
                AnimationTimer.Stop();
                CurrentPlayer = (CurrentPlayer + 1) % TotalPlayers;
            }
        }

        private void RollDice()
        {
            Random rnd = new Random();
            Rolled = rnd.Next(1, 7);
            //Rolled = 10;
            ToGo = Rolled;
            AnimationTimer.Interval = TimeSpan.FromSeconds(1.5/Rolled);
            MovePiece();
            AnimationTimer.Start();
        }


        private static void FieldToCanvasProperty(int field, int playerID, out int Left, out int Top)
        {
            Left = Top = 0;
            if (field >= 0 && field < 13)
            {
                switch (playerID)
                { 
                    case 0:
                        Left = 800 - field * 50;
                        Top = 544;
                        break;
                    case 1:
                        Left = 800 - field * 50;
                        Top = 564;
                        break;
                    case 2:
                        Left = 800 - field * 50;
                        Top = 584;
                        break;
                    case 3:
                        Left = 800 - field * 50;
                        Top = 604;
                        break;
                    case 4:
                        Left = 800 - field * 50;
                        Top = 624;
                        break;
                    case 5:
                        Left = 800 - field * 50;
                        Top = 644;
                        break;
                }
            }
            else if (field == 13)
            {
                switch (playerID)
                {
                    case 0:
                        Left = 163;
                        Top = 544;
                        break;
                    case 1:
                        Left = 143;
                        Top = 544;
                        break;
                    case 2:
                        Left = 123;
                        Top = 544;
                        break;
                    case 3:
                        Left = 103;
                        Top = 544;
                        break;
                    case 4:
                        Left = 83;
                        Top = 544;
                        break;
                    case 5:
                        Left = 63;
                        Top = 544;
                        break;
                }
            }
            else if (field > 13 && field < 21)
            {
                switch (playerID)
                {
                    case 0:
                        Left = 163;
                        Top = 544 - (field - 13) *50;
                        break;
                    case 1:
                        Left = 143;
                        Top = 544 - (field - 13) * 50;
                        break;
                    case 2:
                        Left = 123;
                        Top = 544 - (field - 13) * 50;
                        break;
                    case 3:
                        Left = 103;
                        Top = 544 - (field - 13) * 50;
                        break;
                    case 4:
                        Left = 83;
                        Top = 544 - (field - 13) * 50;
                        break;
                    case 5:
                        Left = 63;
                        Top = 544 - (field - 13) * 50;
                        break;
                }
            }
            else if (field == 21)
            {
                switch (playerID)
                {
                    case 0:
                        Left = 169;
                        Top = 169;
                        break;
                    case 1:
                        Left = 169;
                        Top = 149;
                        break;
                    case 2:
                        Left = 169;
                        Top = 129;
                        break;
                    case 3:
                        Left = 169;
                        Top = 109;
                        break;
                    case 4:
                        Left = 169;
                        Top = 89;
                        break;
                    case 5:
                        Left = 169;
                        Top = 69;
                        break;
                }

            }
            else if (field > 21 && field < 34)
            {
                Left = 169 + (field - 21) * 50;
                switch (playerID)
                {
                    case 0:
                        Top = 169;
                        break;
                    case 1:
                        Top = 149;
                        break;
                    case 2:
                        Top = 129;
                        break;
                    case 3:
                        Top = 109;
                        break;
                    case 4:
                        Top = 89;
                        break;
                    case 5:
                        Top = 69;
                        break;
                }
            }
            else if (field == 34)
            {
                Top = 169;
                switch (playerID)
                {
                    case 0:
                        Left = 800;
                        
                        break;
                    case 1:
                        Left = 820;
                        break;
                    case 2:
                        Left = 840;
                        break;
                    case 3:
                        Left = 860;
                        break;
                    case 4:
                        Left = 880;
                        break;
                    case 5:
                        Left = 900;
                        break;
                }

            }
            else if (field > 34 && field < 42)
            {
                Top = 169 + (field - 34) * 50;
                switch (playerID)
                {
                    case 0:
                        Left = 800;
                        break;
                    case 1:
                        Left = 820;
                        break;
                    case 2:
                        Left = 840;
                        break;
                    case 3:
                        Left = 860;
                        break;
                    case 4:
                        Left = 880;
                        break;
                    case 5:
                        Left = 900;
                        break;
                }
            }            
        }

        private void MovePiece()
        { 
            int NewField = (PlayerPieces[CurrentPlayer].CurrentField + 1) % 42;

            int TopFrom, LeftFrom;
            int TopTo, LeftTo;

            FieldToCanvasProperty(PlayerPieces[CurrentPlayer].CurrentField, CurrentPlayer, out LeftFrom, out TopFrom);
            FieldToCanvasProperty(NewField, CurrentPlayer, out LeftTo, out TopTo);

            PlayerPieces[CurrentPlayer].CurrentField = NewField;

            DoubleAnimation HorizontalAnimation = new DoubleAnimation();
            DoubleAnimation VerticalANimation = new DoubleAnimation();

            HorizontalAnimation.From = LeftFrom;
            HorizontalAnimation.To = LeftTo;

            VerticalANimation.From = TopFrom;
            VerticalANimation.To = TopTo;


            HorizontalAnimation.Duration = VerticalANimation.Duration =TimeSpan.FromSeconds(1.4 / Rolled);

            Storyboard PieceHorizontalStory = new Storyboard();
            PieceHorizontalStory.Children.Add(HorizontalAnimation);

            Storyboard PieceVerticalStory = new Storyboard();
            PieceVerticalStory.Children.Add(VerticalANimation);

            Storyboard.SetTarget(PieceHorizontalStory, PlayerPieces[CurrentPlayer].PlayerEllipse);
            Storyboard.SetTarget(PieceVerticalStory, PlayerPieces[CurrentPlayer].PlayerEllipse);
            Storyboard.SetTargetProperty(PieceHorizontalStory, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTargetProperty(PieceVerticalStory, new PropertyPath(Canvas.TopProperty));

            if (LeftFrom != LeftTo)
                PieceHorizontalStory.Begin();

            if (TopFrom != TopTo)
                PieceVerticalStory.Begin();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
        }

      /*  private void MovePiece(int PlayerId, int Rolled)
        {
                int oldlocation = PlayerPieces[PlayerId].CurrentField;
                int newlocation = PlayerPieces[PlayerId].CurrentField + Rolled;

                DoubleAnimation myAnimation = new DoubleAnimation();
                double from = Convert.ToDouble(PlayerPieces[PlayerId].PlayerEllipse.GetValue(Canvas.LeftProperty));
                double to = from - 50 * Rolled;
                if (to < 150)
                {
                    to = 150;  
                }

                myAnimation.From = from;
                myAnimation.To = to;

                myAnimation.Duration = TimeSpan.FromSeconds(1);

                PieceAnimationStory = new Storyboard();
                PieceAnimationStory.Children.Add(myAnimation);

                Storyboard.SetTarget(PieceAnimationStory, PlayerPieces[PlayerId].PlayerEllipse);
                Storyboard.SetTargetProperty(PieceAnimationStory, new PropertyPath(Canvas.LeftProperty));

                PieceAnimationStory.Begin();
        }*/
    }

    struct PlayerBoardStatus
    {
        public int Id;
        public int CurrentField;
        public Ellipse PlayerEllipse;

        public PlayerBoardStatus(int id, int field, Ellipse piece)
        {
            Id = id;
            CurrentField = field;
            PlayerEllipse = piece;
        }
    }
}

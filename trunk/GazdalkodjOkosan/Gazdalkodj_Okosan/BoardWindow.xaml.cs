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
using System.IO;
using Gazdalkodj_Okosan.Model.Game;

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
            AddFurnitureToCanvas(EFurnitureType.Radio);
            AddFurnitureToCanvas(EFurnitureType.Kitchen);
            ImageOfHouse.Source = CanvasToRenderBitmap(HouseCanvas);
            
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
                String squarename = "img/square" + PlayerPieces[CurrentPlayer].CurrentField.ToString() + ".png";
                squarename = System.IO.Path.GetFullPath(squarename);
                BitmapImage bit = new BitmapImage(new Uri(squarename));
                SquareImage.Source = bit;
                SquareImage.Height = 1;
                SquareImage.Visibility = System.Windows.Visibility.Visible;
                AnimateSquareOpen();
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


        private static void FieldToCanvasProperty(int field, int player, out int Left, out int Top)
        {
            Left = Top = 0;
            if (field > 0 && field < 13)
            {
                Left = 758 - (field - 1) * 50;
                Top = 450 + player * 20;
            }
            else if (field == 13)
            {
                Left = 175 - player * 15;
                Top = 450 + player * 20;
            }
            else if (field > 13 && field < 20)
            {
                Left = 185 - player * 20;
                Top = 410 - (field - 14) * 50;
            }
            else if (field == 20)
            {
                Left = 185 - player * 20;
                Top = 115 - player * 20;
            }
            else if (field > 20 && field < 23)
            {
                Left = 235 + (field - 21) * 50;
                Top = 115 - player * 20;
            }
            else if (field == 23)
            {
                Left = 320 + player * 20;
                Top = 115 - player * 20;
            }
            else if (field == 24)
            {
                Left = 320 + player * 20;
                Top = 160;
            }
            else if (field == 25)
            {
                Left = 420 - player * 20;
                Top = 270 - player * 10;
            }
            else if (field > 25 && field < 28)
            {
                Left = 465 + (field-26)*50;
                Top = 270 - player * 20;
            }
            else if (field == 28)
            {
                Left = 645 - player * 20;
                Top = 200 + player * 10;
            }
            else if (field == 29)
            {
                Left = 645 - player * 20;
                Top = 160;
            }
            else if (field == 30)
            {
                Left = 645 - player * 20;
                Top = 120 - player * 20;
            }
            else if (field > 30 && field < 33)
            {
                Left = 695 + (field - 31) * 50;
                Top = 120 - player * 20;
            }
            else if (field == 33)
            {
                Left = 785 + player * 20;
                Top = 120 - player * 20;
            }
            else if (field > 33 && field < 40)
            {
                Left = 785 + player * 20;
                Top = 160 + (field - 34) * 50;
            }
            else
            { 
                switch (player)
                {
                    case 0:
                        Left = 840;
                        Top = 515;
                        break;
                    case 1:
                        Left = 869;
                        Top = 470;
                        break;
                    case 2:
                        Left = 868;
                        Top = 500;
                        break;
                    case 3:
                        Left = 812;
                        Top = 470;
                        break;
                    case 4:
                        Left = 812;
                        Top = 500;
                        break;
                    case 5:
                        Left = 840;
                        Top = 540;
                        break;
                   
                }
            }
        }

        private void MovePiece()
        { 
            int NewField = (PlayerPieces[CurrentPlayer].CurrentField + 1) % 40;

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

        private bool IsLargeField(int field)
        {
            if (field == 0 ||
                field == 13 ||
                field == 20 ||
                field == 25 ||
                field == 28 ||
                field == 30 ||
                field == 33)
                return true;
            return false;
        }

        public static RenderTargetBitmap CanvasToRenderBitmap(Canvas surface)
        {
            Size size = new Size(surface.Width, surface.Height);

            surface.Measure(size);
            surface.Arrange(new Rect(size));

            // Create a render bitmap and push the surface to it
            RenderTargetBitmap renderBitmap =
              new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            return renderBitmap;
            
        }

        private void AnimateSquareOpen(bool Open = true)
        {

            if (Open)
            {
                if (IsLargeField(PlayerPieces[CurrentPlayer].CurrentField))
                {
                    SquareImage.Width = 520;
                    Canvas.SetLeft(SquareImage, 245);
                    
                }
                else if (System.Convert.ToInt32(SquareImage.GetValue(Canvas.LeftProperty)) != 405)
                {
                    SquareImage.Width = 200;
                    Canvas.SetLeft(SquareImage, 405);
                }
            }

            DoubleAnimation HorizontalAnimation = new DoubleAnimation();
            DoubleAnimation VerticalAnimation = new DoubleAnimation();

            HorizontalAnimation.From = 1;
            HorizontalAnimation.To = 520;

            VerticalAnimation.From = 330;
            VerticalAnimation.To = 70;

            if (!Open)
            {
                HorizontalAnimation.To = 0;
                HorizontalAnimation.From = 520;

                VerticalAnimation.From = 70;
                VerticalAnimation.To = 330;
            }

            HorizontalAnimation.Duration = VerticalAnimation.Duration = TimeSpan.FromSeconds(0.5);

            Storyboard SquareOpenStory = new Storyboard();
            Storyboard SquareMoveStory = new Storyboard();
            SquareOpenStory.Children.Add(HorizontalAnimation);
            SquareMoveStory.Children.Add(VerticalAnimation);

            Storyboard.SetTarget(SquareOpenStory, SquareImage);
            Storyboard.SetTargetProperty(SquareOpenStory, new PropertyPath(Image.HeightProperty));

            Storyboard.SetTarget(SquareMoveStory, SquareImage);
            Storyboard.SetTargetProperty(SquareMoveStory, new PropertyPath(Canvas.TopProperty));

            SquareOpenStory.Begin();
            SquareMoveStory.Begin();
        }

        private void AnimateHouseOpen(bool Open = true)
        {
            if (Open)
            {
                if (IsLargeField(PlayerPieces[CurrentPlayer].CurrentField))
                {
                    SquareImage.Width = 520;
                    Canvas.SetLeft(SquareImage, 245);

                }
                else if (System.Convert.ToInt32(SquareImage.GetValue(Canvas.LeftProperty)) != 405)
                {
                    SquareImage.Width = 200;
                    Canvas.SetLeft(SquareImage, 405);
                }
            }

            DoubleAnimation HorizontalAnimation = new DoubleAnimation();
            DoubleAnimation VerticalAnimation = new DoubleAnimation();
            DoubleAnimation HeightAnimation = new DoubleAnimation();
            DoubleAnimation WidthAnimation = new DoubleAnimation();

            HorizontalAnimation.From = 585;
            HorizontalAnimation.To = 100;

            VerticalAnimation.From = 615;
            VerticalAnimation.To = 100;

            HeightAnimation.From = 85;
            HeightAnimation.To = 572;

            WidthAnimation.From = 124;
            WidthAnimation.To = 835;

            if (!Open)
            {
                HorizontalAnimation.To = 585;
                HorizontalAnimation.From = 100;

                VerticalAnimation.To = 615;
                VerticalAnimation.From = 100;

                HeightAnimation.To = 85;
                HeightAnimation.From = 572;

                WidthAnimation.To = 124;
                WidthAnimation.From = 835;
            }

            HorizontalAnimation.Duration = VerticalAnimation.Duration = 
             HeightAnimation.Duration = WidthAnimation.Duration =  TimeSpan.FromSeconds(0.5);

            Storyboard HouseTopStory = new Storyboard();
            Storyboard HouseLeftStory = new Storyboard();
            Storyboard HouseWidthStory = new Storyboard();
            Storyboard HouseHeightStory = new Storyboard();
            HouseLeftStory.Children.Add(HorizontalAnimation);
            HouseTopStory.Children.Add(VerticalAnimation);
            HouseWidthStory.Children.Add(WidthAnimation);
            HouseHeightStory.Children.Add(HeightAnimation);

            Storyboard.SetTarget(HouseHeightStory, ImageOfHouse);
            Storyboard.SetTargetProperty(HouseHeightStory, new PropertyPath(Image.HeightProperty));

            Storyboard.SetTarget(HouseTopStory, ImageOfHouse);
            Storyboard.SetTargetProperty(HouseTopStory, new PropertyPath(Canvas.TopProperty));

            Storyboard.SetTarget(HouseWidthStory, ImageOfHouse);
            Storyboard.SetTargetProperty(HouseWidthStory, new PropertyPath(Image.WidthProperty));

            Storyboard.SetTarget(HouseLeftStory, ImageOfHouse);
            Storyboard.SetTargetProperty(HouseLeftStory, new PropertyPath(Canvas.LeftProperty));

            HouseHeightStory.Begin();
            HouseTopStory.Begin();
            HouseWidthStory.Begin();
            HouseLeftStory.Begin();
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
        }

        private void SquareImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AnimateSquareOpen(false);
        }

        private void SquareImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SquareImage.Height == 0)
            {
                SquareImage.Visibility = System.Windows.Visibility.Hidden;
            }

        }

        private void AddFurnitureToCanvas(EFurnitureType FurType)
        {
            string path = "img/";
            int left = 0, top = 0;
            switch (FurType)
            { 
                case EFurnitureType.Bathroom:
                    path += "moso.png";
                    left = 0;
                    top = 0;
                    break;
                case EFurnitureType.Bicycle:
                    path += "bike.png";
                    left = 0;
                    top = 402;
                    break;
                case EFurnitureType.Fridge:
                    path += "huto.png";
                    left = 737;
                    top = 286;
                    break;
                case EFurnitureType.Kitchen:
                    path += "kitchen.png";
                    left = 427;
                    top = 286;
                    break;
                case EFurnitureType.Livingroom:
                    path += "szobawithoutradio.png";
                    left = 300;
                    top = 0;
                    break;
                case EFurnitureType.Radio:
                    path += "radio.png";
                    left = 634;
                    top = 0;
                    break;
                case EFurnitureType.TableTennis:
                    path += "pptable.png";
                    left = 164;
                    top = 286;
                    break;
                case EFurnitureType.Television:
                    path += "tvset.png";
                    left = 757;
                    top = 0;
                    break;
                case EFurnitureType.VacuumCleaner:
                    path += "vacuum.png";
                    left = 0;
                    top = 286;
                    break;
            }
            path = System.IO.Path.GetFullPath(path);
            BitmapImage bit = new BitmapImage(new Uri(path));
            Image im = new Image();
            im.Source = bit;
            HouseCanvas.Children.Add(im);
            Canvas.SetLeft(im, left);
            Canvas.SetTop(im, top);
            
        }

        private void ImageOfHouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ImageOfHouse.Height == 572)
            {
                AnimateHouseOpen(false);
            }
            else
                AnimateHouseOpen();
        }

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

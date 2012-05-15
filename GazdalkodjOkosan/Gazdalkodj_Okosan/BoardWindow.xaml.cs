using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using GazdalkodjOkosan.Model.Game;

namespace Gazdalkodj_Okosan
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private PlayerBoardStatus[] PlayerPieces;

        private ImageSource StatusTick;

        private DispatcherTimer AnimationTimer;

        private int Rolled, ToGo, CurrentPlayer, TotalPlayers;

        private Ellipse[] PlayerEllipses;

        private Label[] StatusColumnLabels;

        private bool squareopened = false;

        public BoardWindow(int NumerOfPlayers = 5)
        {
            TotalPlayers = NumerOfPlayers;
            CurrentPlayer = 0;
            InitializeComponent();
            PlayerEllipses = new Ellipse[6] { Player1Piece, Player2Piece, Player3Piece, Player4Piece, Player5Piece, Player6Piece };
            StatusColumnLabels = new Label[TotalPlayers];
            InitializePieceList();
            StatusTick = StatusImage.Source;
            AnimationTimer = new DispatcherTimer();
            AnimationTimer.Interval = TimeSpan.FromSeconds(1.5);
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            AddFurnitureToCanvas(EFurnitureType.Radio);
            AddFurnitureToCanvas(EFurnitureType.Kitchen);
            ImageOfHouse.Source = CanvasToRenderBitmap(HouseCanvas);
           /* PurchaseDialog d = new PurchaseDialog(
            new FurnitureShop(new PieceOfFurniture[] {
                new PieceOfFurniture(6000, EFurnitureType.Television),
                new PieceOfFurniture(2000, EFurnitureType.Radio),
                new PieceOfFurniture(4000, EFurnitureType.Fridge),
                new PieceOfFurniture(5000, EFurnitureType.Bathroom),
                new PieceOfFurniture(1000, EFurnitureType.VacuumCleaner)}));
            d.Show();*/
            PlayerStatusColumn();
            UpdateMoneyLabel(4500);
            
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
                if (CurrentPlayer == 0)
                    AnimateSquareOpen();
                CurrentPlayer = (CurrentPlayer + 1) % TotalPlayers;
                PlayerStatusColumn();
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
                        Left = 850;
                        Top = 515;
                        break;
                    case 1:
                        Left = 880;
                        Top = 470;
                        break;
                    case 2:
                        Left = 880;
                        Top = 500;
                        break;
                    case 3:
                        Left = 820;
                        Top = 470;
                        break;
                    case 4:
                        Left = 820;
                        Top = 500;
                        break;
                    case 5:
                        Left = 850;
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
            squareopened = Open;
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
        private void AnimateStatusOpen(bool Open = true)
        {

            DoubleAnimation HorizontalAnimation = new DoubleAnimation();
            DoubleAnimation VerticalAnimation = new DoubleAnimation();
            DoubleAnimation HeightAnimation = new DoubleAnimation();
            DoubleAnimation WidthAnimation = new DoubleAnimation();

            HorizontalAnimation.From = 750;
            HorizontalAnimation.To = 100;

            VerticalAnimation.From = 633;
            VerticalAnimation.To = 100;

            HeightAnimation.From = 40;
            HeightAnimation.To = 577;

            WidthAnimation.From = 80;
            WidthAnimation.To = 750;

            if (!Open)
            {
                HorizontalAnimation.To = 750;
                HorizontalAnimation.From = 100;

                VerticalAnimation.To = 633;
                VerticalAnimation.From = 100;

                HeightAnimation.To = 40;
                HeightAnimation.From = 577;

                WidthAnimation.To = 80;
                WidthAnimation.From = 750;
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

            Storyboard.SetTarget(HouseHeightStory, StatusImage);
            Storyboard.SetTargetProperty(HouseHeightStory, new PropertyPath(Image.HeightProperty));

            Storyboard.SetTarget(HouseTopStory, StatusImage);
            Storyboard.SetTargetProperty(HouseTopStory, new PropertyPath(Canvas.TopProperty));

            Storyboard.SetTarget(HouseWidthStory, StatusImage);
            Storyboard.SetTargetProperty(HouseWidthStory, new PropertyPath(Image.WidthProperty));

            Storyboard.SetTarget(HouseLeftStory, StatusImage);
            Storyboard.SetTargetProperty(HouseLeftStory, new PropertyPath(Canvas.LeftProperty));

            HouseHeightStory.Begin();
            HouseTopStory.Begin();
            HouseWidthStory.Begin();
            HouseLeftStory.Begin();
            StatusImage.Visibility = System.Windows.Visibility.Visible;
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (squareopened)
                AnimateSquareOpen(false);
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

        private void SetupStatus()
        {
            string path = "img/tick.png";
            path = System.IO.Path.GetFullPath(path);
            BitmapImage bit = new BitmapImage(new Uri(path));
           // Image im = new Image();
           // im.Source = bit;
            Ellipse[] ellipses = new Ellipse[6];
            for (int i = 0; i < 4; ++i)
            {
                ellipses[i] = new Ellipse();
                ellipses[i].Width = 20;
                ellipses[i].Height = 20;
                ellipses[i].Fill = PlayerEllipses[i].Fill;
                StatusCanvas.Children.Add(ellipses[i]);
                Canvas.SetLeft(ellipses[i], 186 + i * 95);
                Canvas.SetTop(ellipses[i], 5);
            }
            for (int i = 0; i < 12; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    Image toAdd = new Image();
                    toAdd.Source = bit;
                    StatusCanvas.Children.Add(toAdd);
                    Canvas.SetLeft(toAdd, 176 + j * 95);
                    Canvas.SetTop(toAdd, 30 + i * 45);
                }
            StatusImage.Visibility = System.Windows.Visibility.Hidden;
            StatusImage.Source = CanvasToRenderBitmap(StatusCanvas);
        }

        private void AddFurnitureToCanvas(EFurnitureType FurType)
        {
            string path = "img/";
            int left = 0, top = 0;
            GetFurnitureLocations(FurType,out left,out top,out path);
            path = System.IO.Path.GetFullPath(path);
            BitmapImage bit = new BitmapImage(new Uri(path));
            Image im = new Image();
            im.Source = bit;
            HouseCanvas.Children.Add(im);
            Canvas.SetLeft(im, left);
            Canvas.SetTop(im, top);
            
        }

        private void PlayerStatusColumn()
        {
            for (int i = 0; i < TotalPlayers; ++i)
            {
                Ellipse ellipses = new Ellipse();
                bool AddLabels = StatusColumnLabels[i] == null;
                if (AddLabels)
                {
                    StatusColumnLabels[i] = new Label();
                    StatusColumnLabels[i].Content = (i + 1) + ". Játékos";
                    StatusColumnLabels[i].FontFamily = new System.Windows.Media.FontFamily("ChessmasterX");
                    StatusColumnLabels[i].FontSize = 14;

                    ellipses.Height = 20;
                    ellipses.Width = 20;
                    ellipses.Fill = PlayerPieces[i].PlayerEllipse.Fill;

                    outer.Children.Add(ellipses);
                    outer.Children.Add(StatusColumnLabels[i]);
                    Canvas.SetLeft(ellipses, 10);
                    Canvas.SetTop(ellipses, 160 + i * 60);
                    Canvas.SetLeft(StatusColumnLabels[i], 10);
                    Canvas.SetTop(StatusColumnLabels[i], 130 + i * 60);
                }
  
                if (CurrentPlayer == i)
                    StatusColumnLabels[i].Foreground = Brushes.White;
                else
                    StatusColumnLabels[i].Foreground = Brushes.Gray;
            }
        }

        private void GetFurnitureLocations(EFurnitureType FurType, out int left, out int top, out String path)
        {
            path = "img/";
            left = 0; top = 0;
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
        }

        private void ImageOfHouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ImageOfHouse.Height == 572)
            {
                AnimateHouseOpen(false);
            }
            else
            {
                int zind = Canvas.GetZIndex(StatusImage);
                Canvas.SetZIndex(ImageOfHouse, zind + 1);
                AnimateHouseOpen();
            }
        }

        private void UpdateMoneyLabel(int Amount)
        {
            String sum = Amount.ToString();
            int lenght = sum.Length;
            int points = (lenght - 1) / 3;
            for (int i = 0; i < points; ++i)
            {
                sum = sum.Insert(lenght - (i + 1) * 3, ".");
            }

            MoneyLabel.Content = sum + ".- Ft";
        }

        private void StatusImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StatusImage.Height == 40)
            {
                SetupStatus();
                AnimateStatusOpen();
            }
            else
            {
                AnimateStatusOpen(false);
                
            }
        }

        private void StatusImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (StatusImage.Height == 40)
                StatusImage.Source = StatusTick;
        }

        private void ImageOfHouse_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ImageOfHouse.Height == 85)
            {
                int zind = Canvas.GetZIndex(StatusImage);
                Canvas.SetZIndex(ImageOfHouse, zind - 1);
            }
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

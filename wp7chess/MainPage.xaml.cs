using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Shell;

namespace wp7chess
{
    public partial class MainPage : PhoneApplicationPage
    {
        private string player_one = "";
        private string player_two = "";

        private Position position = new Position();
        private Searcher searcher;
        private DispatcherTimer timer = new DispatcherTimer();
        private bool isGameOver = false;

        ApplicationBarMenuItem human_vs_phone;
        ApplicationBarMenuItem human_vs_human;
        ApplicationBarMenuItem phone_phone;

        public MainPage()
        {
            InitializeComponent();

            Color color = new Color();
            color.A = 0;
            color.B = 0;
            color.G = 0;
            color.R = 0;

            ApplicationBar = new ApplicationBar();
            ApplicationBar.BackgroundColor = color;
            ApplicationBar.IsMenuEnabled = true;
            ApplicationBar.IsVisible = true;
            ApplicationBar.Opacity = 0.8;

            ApplicationBarIconButton refresh = new ApplicationBarIconButton(new Uri("/appbar_refresh.png", UriKind.Relative));
            refresh.Text = "new game";
            refresh.Click += new EventHandler(refresh_Click);

            ApplicationBar.Buttons.Add(refresh);

            human_vs_phone = new ApplicationBarMenuItem("» human vs phone");
            human_vs_phone.Click += new EventHandler(human_vs_phone_Click);
            ApplicationBar.MenuItems.Add(human_vs_phone);

            human_vs_human = new ApplicationBarMenuItem("human vs human");
            human_vs_human.Click += new EventHandler(human_vs_human_Click);
            ApplicationBar.MenuItems.Add(human_vs_human);

            phone_phone = new ApplicationBarMenuItem("phone vs phone");
            phone_phone.Click += new EventHandler(phone_phone_Click);
            ApplicationBar.MenuItems.Add(phone_phone);

            load();
        }

        void blankMenu()
        {
            phone_phone.Text = "phone vs phone";
            human_vs_phone.Text = "human vs phone";
            human_vs_human.Text = "human vs human";
        }

        void phone_phone_Click(object sender, EventArgs e)
        {
            blankMenu();
            phone_phone.Text = "» phone vs phone";

            blackSelector.setPlayer(1);
            whiteSelector.setPlayer(1);
        }

        void human_vs_phone_Click(object sender, EventArgs e)
        {
            blankMenu();
            human_vs_phone.Text = "» human vs phone";
            blackSelector.setPlayer(1);
            whiteSelector.setPlayer(0);
        }

        void human_vs_human_Click(object sender, EventArgs e)
        {
            blankMenu();
            human_vs_human.Text = "» human vs human";
            blackSelector.setPlayer(0);
            whiteSelector.setPlayer(0);
        }

        private void load()
        {
            this.position.ResetBoard();

            this.searcher = new Searcher(this.position);
            this.searcher.AllottedTime = 200;

            this.chessBoard.UserMove += this.OnUserMove;
            this.chessBoard.AnimationCompleted += this.OnChessBoardAnimationCompleted;

            this.whiteSelector.SelectedPlayer = 0;
            this.blackSelector.SelectedPlayer = 1;
            this.whiteSelector.SelectionChanged += this.OnSelectorSelectionChanged;
            this.blackSelector.SelectionChanged += this.OnSelectorSelectionChanged;

            this.timer.Interval = TimeSpan.FromMilliseconds(10);
            this.timer.Tick += this.OnTimerTick;

            this.LayoutUpdated += this.OnRootControlLayoutUpdated;

            this.Loaded += this.OnRootControlLoaded;
        }

        void refresh_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Refresh.xaml", UriKind.Relative));
        }

        private void OnRootControlLoaded(object sender, RoutedEventArgs e)
        {
            this.chessBoard.Position = this.position;
        }

        private void OnRootControlLayoutUpdated(object sender, EventArgs e)
        {
            this.whiteSelector.Width = this.chessBoard.ActualWidth;
            this.blackSelector.Width = this.chessBoard.ActualWidth;
        }

        private void OnUserMove(ushort move)
        {
            this.position.MakeMove(move);
            this.chessBoard.UpdateForNewPosition();
        }

        private void OnChessBoardAnimationCompleted()
        {
            // ushort moves = HtmlPage.Window.Invoke("GenerateValidMoves");


            if (this.position.GenerateValidMoves().Count == 0)
            // if (this.jscriptPlayer.ValidMoves().Count == 0)
            {
                if (this.isGameOver)
                {
                    return;
                }

                this.isGameOver = true;
                // Game over state
                //WinnerDisplayControl winner = new WinnerDisplayControl();
                //winner.Victor = this.position.ToMove == 0 ? "White" : "Black";
                //winner.Completed += this.OnWinnerCompleted;
                //winner.Width = 400;
                //winner.Height = 250;
                //this.LayoutRoot.Children.Add(winner);

                string winnerColor = this.position.ToMove == 0 ? "White" : "Black";
                MessageBox.Show(winnerColor + " wins!");

            }
            else
            {
                this.DoAITurn();
            }
        }

        private void OnWinnerCompleted(object sender, RoutedEventArgs e)
        {
            this.isGameOver = false;
            this.LayoutRoot.Children.Remove((UIElement)sender);

            this.position.ResetBoard();
            this.chessBoard.UpdateForNewPosition();
        }

        private void OnSelectorSelectionChanged(object sender, EventArgs e)
        {
            this.DoAITurn();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.chessBoard.UpdateForNewPosition();
        }

        private void DoAITurn()
        {
            int color = this.position.ToMove;
            PlayerSelector selector = color == 0 ? this.blackSelector : this.whiteSelector;

            if (selector.SelectedPlayer == 0)
            {
                this.UpdateNodesSec(color, "Human", 3, 1000);
                return;
            }

            if (selector.SelectedPlayer == 1)
            {
                this.position.MakeMove(this.searcher.Search());
                this.UpdateNodesSec(color, "C#", this.searcher.QNodeCount + this.searcher.NodeCount, this.searcher.AllottedTime);
            }
            else if (selector.SelectedPlayer == 2)
            {
                //this.jscriptPlayer.Fen = this.position.GetFen();
                //this.position.MakeMove(this.jscriptPlayer.MakeMove());
                //this.UpdateNodesSec(color, "JS", this.jscriptPlayer.Nodes, this.jscriptPlayer.Time);

            }

            this.timer.Start();
        }

        private void UpdateNodesSec(int color, string aiName, int nodes, int time)
        {
            long nodesSec = ((long)nodes * 1000) / time;
            if (color == 0)
            {
                this.blackNodesSec.Text = nodesSec.ToString();
                this.blackNodesTitle.Text = aiName + " Nodes/Sec.";
            }
            else
            {
                this.whiteNodesSec.Text = nodesSec.ToString();
                this.whiteNodesTitle.Text = aiName + " Nodes/Sec.";
            }
        }
    }
}
using System;
using System.Windows;
using System.Windows.Threading;
using ExtremeConfigAwait;

namespace ExtremeConfigAwaitWPFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        DispatcherTimer _timer = new DispatcherTimer();
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            int val = Int32.Parse(txtTicks.Text);
            val++;
            txtTicks.Text = val.ToString();
        }

        private async void bttnRun_Case0(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            var c = new Case0();
            Diag.PrintContext("BEFORE AWAIT");
            await c.Run();
            Diag.PrintContext("AFTER AWAIT");

            EnableButtons(true);
        }

        private async void bttnRun_Case1(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            var c = new Case1();
            Diag.PrintContext("BEFORE AWAIT");
            await c.Run();
            Diag.PrintContext("AFTER AWAIT");

            EnableButtons(true);
        }

        private async void bttnRun_Case2(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            var c = new Case2();
            Diag.PrintContext("BEFORE AWAIT");
            await c.Run();
            Diag.PrintContext("AFTER AWAIT");

            EnableButtons(true);
        }


        private async void bttnRun_Case3(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            var c = new Case3();
            Diag.PrintContext("BEFORE AWAIT");
            await c.Run();
            Diag.PrintContext("AFTER AWAIT");

            EnableButtons(true);
        }

        private async void bttnRun_Case4(object sender, RoutedEventArgs e)
        {
            EnableButtons(false);

            var c = new Case4();
            Diag.PrintContext("BEFORE AWAIT");
            await c.Run();
            Diag.PrintContext("AFTER AWAIT");

            EnableButtons(true);
        }

        private void EnableButtons(bool enabled)
        {
            bttnRunCase0.IsEnabled = enabled;
            bttnRunCase1.IsEnabled = enabled;
            bttnRunCase2.IsEnabled = enabled;
            bttnRunCase3.IsEnabled = enabled;
            bttnRunCase4.IsEnabled = enabled;
        }
    }
}

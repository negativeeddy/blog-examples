using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AsyncInfiniteLoops
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task DoWorkAsyncInfiniteLoop()
        {
            while (true)
            {
                // do the work in the loop
                string newData = DateTime.Now.ToLongTimeString();

                // update the UI
                txtTicks.Text = "ASYNC LOOP - " + newData;

                // don't run again for at least 200 milliseconds
                await Task.Delay(200);
            }
        }

        private void bttnStart_Click(object sender, RoutedEventArgs e)
        {
            //DoWorkPollingTask();
            //DoWorkTimer();

            // invoke RunTimer but DO NOT await it
            DoWorkAsyncInfiniteLoop();
        }

        #region POLLINGTASK METHOD
        void DoWorkPollingTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    // do the work in the loop
                    string newData = DateTime.Now.ToLongTimeString();

                    // marshal back over to the UI thread to update the UI
                    Dispatcher.Invoke(() => txtTicks.Text = "TASK - " + newData);

                    // don't run again for at least 200 milliseconds
                    await Task.Delay(200);
                }
            });
        }
        #endregion

        #region DISPATCHERTIMER METHOD
        DispatcherTimer _timer = new DispatcherTimer();

        void DoWorkTimer()
        {
            // execute at a minimum of 200 milliseconds between ticks
            _timer.Interval = TimeSpan.FromMilliseconds(200);
            _timer.Tick += _timer_Tick;
            _timer.IsEnabled = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            // do the work in the loop
            string newData = DateTime.Now.ToLongTimeString();

            // update the UI on the UI thread
            txtTicks.Text = "TIMER - " + newData;
        }
        #endregion
    }
}

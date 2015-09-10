using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AsyncIsNotParallel
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

        Stopwatch sw = new Stopwatch();
        private async void bttnStart_Click(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "Started";
            sw.Restart();
            Task t1 = DoWorkAsync(Colors.Red, 0, 200);
            Task t2 = DoWorkAsync(Colors.Blue, 1, 300);
            Task t3 = DoWorkAsync(Colors.Green, 2, 500);
            await Task.WhenAll(t1, t2, t3);
            sw.Stop();
            txtStatus.Text = $"Finished after {sw.ElapsedMilliseconds}";
        }

        private void bttnClear_Click(object sender, RoutedEventArgs e)
        {
            theRootCanvas.Children.Clear();
        }

        private async Task DoWorkAsync(Color color, int column, int delay)
        {
            for (int i = 0; i < 5; i++)
            {
                // perform some asynchronous task in various ways
                // await Task.Yield();       // yields immediately
                // await Task.Delay(0);      // continues synchronously
                // await Task.Delay(100);     // continues asynchronously on UI thread
                // await Task.Delay(0).ConfigureAwait(false);    // continues synchronously
                // await Task.Delay(100).ConfigureAwait(false);   // continues asynchronously on any thread

                var startTime = sw.ElapsedMilliseconds;

                // spin here for 'delay' milliseconds just to keep busy
                while (sw.ElapsedMilliseconds < startTime + delay) { /* do nothing */ }

                var endTime = sw.ElapsedMilliseconds;

                Dispatcher.BeginInvoke((Action)(() => AddTimeRectangle(
                                                startTime, endTime, color, column)));
            }
        }

        /// <summary>
        /// add a rectangle that whose height & position represents the start and end time
        /// of an activity
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="color"></param>
        /// <param name="column"></param>
        private void AddTimeRectangle(double startTime, double endTime, Color color, int column)
        {
            const int columnWidth = 50;
            Debug.WriteLine("Adding Rect {0} {1} {2}", startTime, endTime, color);

            double left = column * (columnWidth * 2 / 3);
            double height = endTime - startTime;

            Rectangle r = new Rectangle()
            {
                Fill = new SolidColorBrush(color),
                Width = columnWidth,
                Height = height,
            };
            Canvas.SetLeft(r, left);
            Canvas.SetTop(r, startTime);

            theRootCanvas.Children.Add(r);

            if (theRootCanvas.Height < endTime)
            {
                theRootCanvas.Height = endTime;
            }
            if (theRootCanvas.Width < left + columnWidth)
            {
                theRootCanvas.Width = left + columnWidth;
            }
        }
    }
}

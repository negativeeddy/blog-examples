using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    public class Case4
    {
        public async Task Run()
        {
            // async calls run on a completely different thread (which does
            // not capture the original SynchronizationContext)
            Diag.PrintContext("ENTER");
            await Task.Run(() => WorkWithoutCA());
            Diag.PrintContext("EXIT");
        }

        const int cycleCount = 15000;

        private async Task WorkWithoutCA()
        {
            Diag.PrintContext("ENTER");

            await DoWorkAsync(cycleCount).PrintContext();
            await DoWorkAsync(cycleCount).PrintContext();
            await DoWorkAsync(cycleCount).PrintContext();

            Diag.PrintContext("EXIT");
        }

        private async Task DoWorkAsync(uint cycles)
        {
            Diag.PrintContext("ENTER");
            await BlockingWorkAsync(cycles).PrintContext();
            Diag.PrintContext("EXIT");
        }

        private async Task BlockingWorkAsync(uint cycles)
        {
            // Perform both async blocking and synchronous blocking
            Diag.PrintContext("ENTER");
            await Task.Delay(1000).PrintContext();

            Thread.Sleep(1000);

            await Task.Delay(1000).PrintContext();
            Diag.PrintContext("EXIT");
        }
    }
}

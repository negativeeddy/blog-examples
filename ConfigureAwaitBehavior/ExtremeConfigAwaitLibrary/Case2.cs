using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    public class Case2
    {
        public async Task Run()
        {
            // async calls with all awaits marked with ConfigureAwait(false)
            // will try to not use the default synchronization context
            Diag.PrintContext("ENTER");
            await WorkWithFullCA();
            Diag.PrintContext("EXIT");
        }

        const int cycleCount = 15000;

        private async Task WorkWithFullCA()
        {
            Diag.PrintContext("ENTER");

            await DoWorkAsyncCA(cycleCount).PrintContext().ConfigureAwait(false);
            await DoWorkAsyncCA(cycleCount).PrintContext().ConfigureAwait(false);
            await DoWorkAsyncCA(cycleCount).PrintContext().ConfigureAwait(false);

            Diag.PrintContext("EXIT");
        }

        private async Task DoWorkAsyncCA(uint cycles)
        {
            Diag.PrintContext("ENTER");
            await BlockingWorkAsyncCA(cycles).PrintContext().ConfigureAwait(false);
            Diag.PrintContext("EXIT");
        }

        private async Task BlockingWorkAsyncCA(uint cycles)
        {
            // Perform both async blocking and synchronous blocking
            Diag.PrintContext("ENTER");

            await Task.Delay(1000).PrintContext().ConfigureAwait(false);

            Thread.Sleep(1000);

            await Task.Delay(1000).PrintContext().ConfigureAwait(false);
            Diag.PrintContext("EXIT");
        }
    }
}

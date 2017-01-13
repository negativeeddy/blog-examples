using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    public class Case1
    {
        public async Task Run()
        {
            // async calls with just the initial method's awaits marked 
            // with ConfigureAwait(false)
            // will try to not use the default synchronization context
            Diag.PrintContext("ENTER");
            await WorkWithShallowCA();
            Diag.PrintContext("EXIT");
        }

        const int cycleCount = 15000;

        private async Task WorkWithShallowCA()
        {
            // only has ConfigureAwait at this level, children do not.
            Diag.PrintContext("ENTER");

            await DoWorkAsync(cycleCount).PrintContext().ConfigureAwait(false);
            await DoWorkAsync(cycleCount).PrintContext().ConfigureAwait(false);
            await DoWorkAsync(cycleCount).PrintContext().ConfigureAwait(false);

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

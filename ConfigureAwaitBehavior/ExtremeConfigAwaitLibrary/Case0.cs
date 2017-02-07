using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    public class Case0
    {
        public async Task Run()
        {
            // regular async calls with no use of ConfigureAwait(false) 
            // will use the default synchronization context
            Diag.PrintContext("ENTER");
            await WorkWithoutCA();
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

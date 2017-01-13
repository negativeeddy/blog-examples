using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    public class Case3
    {
   
        public async Task Run()
        {
            // async calls with the initial call stripped of its SynchronizationContext
            Diag.PrintContext("ENTER");
            await RunAsyncWithoutContext2(WorkWithoutCA);
            Diag.PrintContext("EXIT");
        }

        public async Task RunAsyncWithoutContext2(Func<Task> asyncMethod)
        {
            SynchronizationContext ctx = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(null);
                await asyncMethod();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(ctx);
            }
        }

        private Task RunAsyncWithoutContext(Func<Task> asyncMethod)
        {
            // work without configure await but trying to get 
            // off the synchronization context

            // Cant use the async/await infrastructure here or it will propogate the
            // null Context back to the caller at the first await
            // "Dont use the plumbing while you are changing the plumbing"
            Task t = null;
            Diag.PrintContext("ENTER");
            var oldContext = SynchronizationContext.Current;
            try
            {
                SynchronizationContext.SetSynchronizationContext(null);
                t = asyncMethod();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(oldContext);
            }

            Diag.PrintContext("EXIT");
            return t;
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

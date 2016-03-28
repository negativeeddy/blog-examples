using System.Threading.Tasks;

namespace AsyncOverSimplified
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleMainAsync().Wait();
            //ComplexMainAsync().Wait();
        }

        /// <summary>
        /// Runs all 3 variations of the Simple code to ensure
        /// they all have the same result
        /// </summary>
        /// <returns></returns>
        static async Task SimpleMainAsync()
        {
            var worker = new SimpleAsyncWorker();
            await worker.DoWork();
            await worker.DoWorkExplicitTasks();
            await worker.DoWorkWithoutAwait();
        }

        /// <summary>
        /// Runs all 3 variations of the MoreRealistic code to ensure
        /// they all have the same result
        /// </summary>
        /// <returns></returns>
        static async Task MoreRealisticMainAsync()
        {
            var worker = new MoreRealisticAsyncWorker();
            await worker.DoWork();
            await worker.DoWorkExplicitTasks();
            await worker.DoWorkWithoutAwait();
        }
    }
}

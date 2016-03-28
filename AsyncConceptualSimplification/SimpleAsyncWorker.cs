using System;
using System.Threading.Tasks;

namespace AsyncOverSimplified
{
    public class SimpleAsyncWorker
    {
        public async Task<int> DoWork()
        {
            int count = 0;

            int result = await GetCountAsync("x");
            count = count + result;
            Console.WriteLine("X Count is " + count);
            return count;
        }

        public async Task<int> DoWorkExplicitTasks()
        {
            int count = 0;

            Task<int> countTask = GetCountAsync("x");
            int result = await countTask;
            count = count + result;
            Console.WriteLine("X Count is " + count);
            return count;
        }

        public Task<int> DoWorkWithoutAwait()
        {
            int count = 0;

            Task<int> countTask = GetCountAsync("x");

            Task<int> final =
            countTask.ContinueWith(_ =>
            {
                int result = countTask.Result;
                count = count + result;
                Console.WriteLine("X Count is " + count);
                return count;
            });

            return final;
        }


        private async Task<int> GetCountAsync(string v)
        {
            await Task.Delay(100);
            return (int)v[0];
        }
    }
}

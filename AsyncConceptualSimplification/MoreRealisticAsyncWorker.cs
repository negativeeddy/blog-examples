using System;
using System.Threading.Tasks;

namespace AsyncOverSimplified
{
    public class MoreRealisticAsyncWorker
    {
        public async Task<int> DoWork()
        {
            int count = 0;


            int resultX = await GetCountAsync("x");
            count = count + resultX;
            Console.WriteLine("X Count is " + count);


            int resultY = await GetCountAsync("y");
            count = count + resultY;
            Console.WriteLine("Y Count is " + count);


            int resultZ = await GetCountAsync("z");
            count = count + resultZ;
            Console.WriteLine("Z Count is " + count);

            return count;
        }

        public async Task<int> DoWorkExplicitTasks()
        {
            int count = 0;

            Task<int> taskX = GetCountAsync("x");
            int resultX = await taskX;
            count = count + resultX;
            Console.WriteLine("X Count is " + count);

            Task<int> taskY = GetCountAsync("y");
            int resultY = await taskY;
            count = count + resultY;
            Console.WriteLine("Y Count is " + count);

            Task<int> taskZ = GetCountAsync("z");
            int resultZ = await taskZ;
            count = count + resultZ;
            Console.WriteLine("Z Count is " + count);

            return count;
        }

        public Task<int> DoWorkWithoutAwait()
        {
            int count = 0;
            Task<int> final;

            Task<int> startTask = GetCountAsync("x");

            final =
            startTask.ContinueWith(taskX =>
            {
                int resultX = taskX.Result;
                count = count + resultX;
                Console.WriteLine("X Count is " + count);
            })
            .ContinueWith(tmp =>
            {
                return GetCountAsync("y").Result;
            })
            .ContinueWith(ty =>
            {
                int resultY = ty.Result;
                count = count + resultY;
                Console.WriteLine("Y Count is " + count);
            })
            .ContinueWith(tmp =>
            {
                return GetCountAsync("z").Result;
            })
            .ContinueWith(tz =>
            {
                int resultZ = tz.Result;
                count = count + resultZ;
                Console.WriteLine("Z Count is " + count);
            })
            .ContinueWith(_ => count);

            return final;
        }

        /// <summary>
        /// This method just returns a number for demonstration purposes
        /// It doesnt actually count anything
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private async Task<int> GetCountAsync(string v)
        {
            await Task.Delay(100);
            return (int)v[0];
        }
    }
}

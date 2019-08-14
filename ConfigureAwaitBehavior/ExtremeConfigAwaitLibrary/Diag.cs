using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ExtremeConfigAwait
{
    static public class Diag
    {
        static public void PrintContext(string message, [CallerMemberName]string callerName = null)
        {

            var ctx = SynchronizationContext.Current;
            if (ctx != null)
            {
                
                Debug.WriteLine("{0}: {1} 0x{2:X8} TID:{3} TSCHED:0x{4}", callerName, message, ctx.GetHashCode(), Thread.CurrentThread.ManagedThreadId, TaskScheduler.Current);
            }
            else
            {
                Debug.WriteLine("{0}: {1} <NO CONTEXT> TID:{2} TSCHED:{3}", callerName, message, Thread.CurrentThread.ManagedThreadId, TaskScheduler.Current);
            }
        }
    }
}

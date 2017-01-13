using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
                Console.WriteLine("{0}: {1} 0x{2:X8} TID:{3}", callerName, message, ctx.GetHashCode(), Thread.CurrentThread.ManagedThreadId);
            }
            else
            {
                Console.WriteLine("{0}: {1} <NO CONTEXT> TID:{2}", callerName, message, Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}

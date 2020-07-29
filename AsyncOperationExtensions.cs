using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common.Framework
{
    public static class AsyncOperationExtensions
    {
        public static async Task Await(this AsyncOperation asyncOperation)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            asyncOperation.completed += obj =>
            {
                autoResetEvent.Set();
            };

            while (!autoResetEvent.WaitOne(0))
                await Task.Yield();
        }
    }
}

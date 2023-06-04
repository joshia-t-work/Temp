using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace DKP.TaskManager
{
    public static class TaskSystem
    {
        public static Task SafelyThread(Task t)
        {
            return Task.Run(async () =>
            {
                try
                {
                    await t;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
        public static async Task WithAggregateException(this Task source)
        {
            try
            {
                await source.ConfigureAwait(false);
            }
            catch
            {
                if (source.Exception == null) throw;
                ExceptionDispatchInfo.Capture(source.Exception).Throw();
            }
        }

    }
}
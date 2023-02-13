namespace StepsRecorderAdvanced.Core.Models.Extensions
{
    public static class TaskExtensions
    {
        public async static void SafeFireAndForget(this Task task, bool continueOnCapturedContext = true, Action<Exception>? onException = null){
            try{
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException is not null){
                onException(ex);
            }
        }
    }
}
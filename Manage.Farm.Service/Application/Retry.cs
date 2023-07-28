namespace Manage.Farm.Service.API.Application;

public static class Retry
{
    public static async Task Do(
        Task task,
        TimeSpan retryInterval,
        int retryCount = 3)
    {
        var exceptions = new List<Exception>();

        for (var tried = 0; tried < retryCount; tried++)
        {
            try
            {
                if (tried > 0)
                {
                    await Task.Delay(retryInterval);
                }

                await task;
                return;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }

        throw new AggregateException(exceptions);
    }
}

using Hangfire;


public class SendEmailsJob
{
    public SendEmailsJob()
    {
        // You can ask for configuration or any other
        // dependency the job might need via Dependency Injection
    }

    [JobDisplayName("Send {0} emails")]
    [AutomaticRetry(Attempts = 3)]
    public async Task Execute(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await Task.Delay(1000);
        }
    }
}

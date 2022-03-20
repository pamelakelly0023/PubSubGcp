using Google.Cloud.PubSub.V1;

public class PullMessages
{
    string projectId = "name-project-id";
    string subscriptionId = "my-sub";
    bool acknowledge = false;
    public async Task<List<PubsubMessage>> PullMessagesAsync()
    {
        
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(projectId, subscriptionId);

            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

            var messages = new List<PubsubMessage>();
             
            Task startTask = subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
            {
    
                string text = System.Text.Encoding.UTF8.GetString(message.Data.ToArray());
                Console.WriteLine($"Mensagem {message.MessageId}: {text}");
                if (message.Attributes != null)
                {
                    foreach (var attribute in message.Attributes)
                    {
                        Console.WriteLine($"{attribute.Key} = {attribute.Value}");
                    }
                }
                return Task.FromResult(acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack);
            });
            
            await Task.Delay(7000);
            await subscriber.StopAsync(CancellationToken.None);
            
            await startTask;
            return messages;
            
    }

    public static async Task Main()
    {
        var sub = new PullMessages();
        await sub.PullMessagesAsync();
    }
}
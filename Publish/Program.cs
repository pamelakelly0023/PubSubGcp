using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Grpc.Core;

public class Pub
{
    string projectId = "name-project-ig";
    string topicId= "my-topic";
    string messageText = "Sua menssagem aqui";

    public async Task PublishMessageAsync()
    {
        try{
            TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            var pubsubMessage = new PubsubMessage
            {
                
                Data = ByteString.CopyFromUtf8(messageText),
                
                Attributes =
                {
                    { "year", "2022" },
                    { "author", "unknown" }
                }
            };
            string message = await publisher.PublishAsync(pubsubMessage);

            Console.WriteLine($"mensagem publicada: {message}");


        }catch(RpcException e){
           Console.WriteLine("Erro");
        }

    }
    public static async Task Main()
    {
        var pub = new Pub();
        await pub.PublishMessageAsync();
    }
    
}  


    

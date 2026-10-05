using RabbitMQ.Client;

ConnectionFactory connectionFactory = new()
{
    HostName = "localhost",
    Port = 5672,
    UserName = "guest",
    Password = "guest"
};
// ConnectionFactory connectionFactory = new()
// {
//     Uri = new Uri("amqp://guest:guest@localhost:5672/")
// };

using IConnection connection = await connectionFactory.CreateConnectionAsync();
using IChannel channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "test-queue",
    exclusive: false,
    autoDelete: false
);

string message = "Hello from Producer!";
byte[] body = System.Text.Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
    exchange: string.Empty,
    routingKey: "test-queue",
    body: body
);
Console.WriteLine(" [x] Sent {0}", message);
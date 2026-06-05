using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

AsyncEventingBasicConsumer consumer = new(channel);

await channel.BasicConsumeAsync(
    queue: "test-queue",
    autoAck: true,
    consumer: consumer
);

consumer.ReceivedAsync += async (sender, ev) =>
{
    byte[] body = ev.Body.ToArray();
    string message = System.Text.Encoding.UTF8.GetString(body);
    //await channel.BasicAckAsync(ev.DeliveryTag, multiple: false);
    //await channel.BasicNackAsync(ev.DeliveryTag, multiple: false, requeue: true);
    Console.WriteLine(" [x] Received {0}", message);
};
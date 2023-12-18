using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace GarbageMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"

███████████████████████████████████████████▀█████████████████████████████▀███████
█▄─▄███▄─▄▄─█─▄─▄─█─▄─▄─█▄─▄▄▀█▄─▄▄─███─▄▄▄▄██▀▄─██▄─▄▄▀█▄─▄─▀██▀▄─██─▄▄▄▄█▄─▄▄─█
██─██▀██─▄█▀███─█████─████─▄─▄██─▄█▀███─██▄─██─▀─███─▄─▄██─▄─▀██─▀─██─██▄─██─▄█▀█
▀▄▄▄▄▄▀▄▄▄▄▄▀▀▄▄▄▀▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▀▄▄▀▄▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀");

            Random rand = new Random();
            while (true) 
            {
                Thread.Sleep(rand.Next(1000, 10000));
                ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connexion = factory.CreateConnection())
                {
                    using (IModel channel = connexion.CreateModel())
                    {
                        channel.QueueDeclare(queue: "m06-comptes",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );


                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.TypeNameHandling = TypeNameHandling.Auto;
                        settings.Formatting = Newtonsoft.Json.Formatting.Indented;

                        string garbage = "Hello World!";

                        string jsonString = JsonConvert.SerializeObject(garbage, settings);
                        byte[] body = Encoding.UTF8.GetBytes(jsonString);

                        channel.BasicPublish(exchange: "", routingKey: "m06-comptes", body: body);

                        Console.WriteLine("Garbage sended");

                    }
                }
            
            }
        }
    }
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

namespace Traitement_Lettres_Mortes
{
    internal class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine(@"

█████████████████████████████████████████████████████████████████████████████████████████████████████
█─▄─▄─█▄─▄▄▀██▀▄─██▄─▄█─▄─▄─█▄─▄▄─█▄─▀█▀─▄█▄─▄▄─█▄─▀█▄─▄█─▄─▄─███▄─▄███▄─▄▄─█─▄─▄─█─▄─▄─█▄─▄▄▀█▄─▄▄─█
███─████─▄─▄██─▀─███─████─████─▄█▀██─█▄█─███─▄█▀██─█▄▀─████─██████─██▀██─▄█▀███─█████─████─▄─▄██─▄█▀█
▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▀▄▄▀▄▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▀▄▄▄▀▀▀▀▄▄▄▄▄▀▄▄▄▄▄▀▀▄▄▄▀▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀
████████████████████████████████
█▄─▀█▀─▄█─▄▄─█▄─▄▄▀█─▄─▄─█▄─▄▄─█
██─█▄█─██─██─██─▄─▄███─████─▄█▀█
▀▄▄▄▀▄▄▄▀▄▄▄▄▀▄▄▀▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀");

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    string directoryName = Path.Combine(Environment.CurrentDirectory, "LettreMorte");
                    channel.QueueDeclare(queue: "m06-comptes-lettres-mortes", durable: false, exclusive: false,
                    autoDelete: false, arguments: null
                    );

                    EventingBasicConsumer consommateur = new EventingBasicConsumer(channel);
                    consommateur.Received += (model, ea) =>
                    {
                        byte[] donnees = ea.Body.ToArray();
                        string compte = Encoding.UTF8.GetString(donnees);
                        string fileName = $"{DateTime.Now:yyyyMMddHHmmss}--{Guid.NewGuid()}.bin";
                        Console.WriteLine(fileName);
                        if (!Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        File.WriteAllBytes(Path.Combine(directoryName, fileName), donnees);

                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue: "m06-comptes-lettres-mortes",
                    autoAck: false,
                    consumer: consommateur
                    );
                    waitHandle.WaitOne();
                }
            }
        }
    }
}
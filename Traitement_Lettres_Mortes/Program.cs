
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
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: "m06-comptes-lettres-mortes", durable: false, exclusive: false,
                    autoDelete: false, arguments: null
                    );

                    EventingBasicConsumer consommateur = new EventingBasicConsumer(channel);
                    consommateur.Received += (model, ea) =>
                    {
                        byte[] donnees = ea.Body.ToArray();
                        string compte = Encoding.UTF8.GetString(donnees);
                        string fileName = $"{DateTime.Now}-{new Guid()}.bin"
;
                        using (FileStream fs = File.Create(fileName))
                        {
                            fs.Write(donnees, 0, donnees.Length);
                        }

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
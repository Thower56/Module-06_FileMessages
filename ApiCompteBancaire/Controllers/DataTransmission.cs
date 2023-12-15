using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IConnection = RabbitMQ.Client.IConnection;
using IModel = RabbitMQ.Client.IModel;

namespace ApiCompteBancaire.Controllers
{
    public static class DataTransmission
    {
        public static void Traitement(Object? p_object)
        {
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

                    string jsonString = JsonConvert.SerializeObject(p_object, settings);
                    byte[] body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish(exchange: "", routingKey: "m06-comptes", body: body);

                }
            }
        }
    }
}


using DAL_Client_SQL_Server;
using DAL_Compte_Bancaire_SQL_Server;
using Entity_Compte_Bancaire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

namespace Traitement_Creations_Modifications
{
    internal class Program
    {
        

        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName).AddJsonFile("appsettings.json").Build();
            ApplicationDbContext m_Dbcontext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(config.GetConnectionString
                ("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).Options);

            IDepotCompteBancaire depotCompteBancaire = new DepotCompteBancaireSQL(m_Dbcontext);
            ManipulationCompteBancaire manipulationCompteBancaire = new ManipulationCompteBancaire(depotCompteBancaire);

            IDepotTransaction depotTransaction = new DepotTransactionSQL(m_Dbcontext);
            ManipulationTransaction manipulationTransaction = new ManipulationTransaction(depotTransaction);

            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: "m06-comptes", durable: false, exclusive: false,
                    autoDelete: false, arguments: null
                    );

                    EventingBasicConsumer consommateur = new EventingBasicConsumer(channel);
                    consommateur.Received += (model, ea) =>
                    {
                        byte[] donnees = ea.Body.ToArray();
                        string enveloppe = Encoding.UTF8.GetString(donnees);


                        EnveloppeCompteBancaire enveloppeRecu = JsonConvert.DeserializeObject<EnveloppeCompteBancaire>(enveloppe);
                        Console.Out.WriteLine("Enveloppe recu !");

                        if (enveloppeRecu.Action == "Create" && enveloppeRecu.ActionEntity == "Compte")
                        {
                            Console.Out.WriteLine("Action Create !");
                            manipulationCompteBancaire.AddCompte(enveloppeRecu.CompteBancaire);

                        }
                        else if (enveloppeRecu.Action == "Update" && enveloppeRecu.ActionEntity == "Compte")
                        {
                            Console.Out.WriteLine("Action Update !");
                            manipulationCompteBancaire.UpdateCompte(enveloppeRecu.CompteBancaire);
                        }

                        else if (enveloppeRecu.Action == "Create" && enveloppeRecu.ActionEntity == "Transaction")
                        {
                            Console.Out.WriteLine("Action Create !");
                            manipulationTransaction.AddTransaction(enveloppeRecu.Transaction);
                        }
                        else 
                        {
                            Console.Out.WriteLine("Action inconnue !");
                            DeadLetterTraitement(donnees);
                        }
                        

                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue: "m06-comptes",
                    autoAck: false,
                    consumer: consommateur
                    );
                    waitHandle.WaitOne();
                }
            }
        }

        private static void DeadLetterTraitement(Object? p_object)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: "m06-comptes-lettres-mortes",
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

                    channel.BasicPublish(exchange: "", routingKey: "m06-comptes-lettres-mortes", body: body);
                    
                }
            }
        }
    }
}
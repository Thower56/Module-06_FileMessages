using Entity_Compte_Bancaire;
using MyNamespace;

namespace NSwaggerClient
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(@"
█████████████████████████████████████████████████████████████████▀█████▀█████████████
█─▄▄▄─█▄─▄███▄─▄█▄─▄▄─█▄─▀█▄─▄█─▄─▄─███─▄▄▄▄█▄─█▀▀▀█─▄██▀▄─██─▄▄▄▄█─▄▄▄▄█▄─▄▄─█▄─▄▄▀█
█─███▀██─██▀██─███─▄█▀██─█▄▀─████─█████▄▄▄▄─██─█─█─█─███─▀─██─██▄─█─██▄─██─▄█▀██─▄─▄█
▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▀▄▄▄▀▀▀▀▄▄▄▄▄▀▀▄▄▄▀▄▄▄▀▀▄▄▀▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀");
            Thread compte = new Thread(CompteMaker);
            Thread transaction = new Thread(TransactionMaker);
            compte.Start();
            transaction.Start();
            compte.Join();
            transaction.Join();
        }

        static void TransactionMaker()
        {
            Random rand = new Random();
            

            for (; ;)
            {
                int numeroCompte = rand.Next(1, 20);
                Thread.Sleep(rand.Next(1000, 10000));
                TransactionClient client = new TransactionClient();
                TransactionModel test = new TransactionModel() { TransactionType = "Debit", Date = DateTime.Now, Montant = (rand.Next(1, 1000)), CompteBancaireId = numeroCompte };
                Task task = client.PostAsync(numeroCompte, test);
                task.Wait();
                Console.WriteLine("Demande de creation transaction envoyer !");
            }
        }

        static void CompteMaker()
        {
            Random rand = new Random();
            for (; ;)
            {
                Thread.Sleep(rand.Next(1000, 10000));
                CompteBancaireClient client = new CompteBancaireClient();
                CompteBancaireModel test = new CompteBancaireModel() { Type = "Courant" };
                Task task = client.PostAsync(test);
                task.Wait();
                Console.WriteLine("Demande de creation de compte envoyer !");
            }  
        }
    }
}
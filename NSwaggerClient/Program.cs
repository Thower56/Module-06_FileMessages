using Entity_Compte_Bancaire;
using MyNamespace;

namespace NSwaggerClient
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            CompteBancaireClient client = new CompteBancaireClient();
            CompteBancaireModel test = new CompteBancaireModel() {Type = "Courant" };
            Task task = client.PostAsync(test);
            task.Wait();
            Console.WriteLine("Compte Create !");
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Compte_Bancaire
{
    public class CompteBancaire
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Courant";
        public IEnumerable<Transaction>? Transactions { get; set; }
        public CompteBancaire(int p_Id, string p_Type)
        {
            Id = p_Id;
            Type = p_Type;
        }
        
        public override string ToString()
        {
            string Compte = "Compte - Id : " + Id + " Courant : " + Type;
            if (Transactions != null)
            {
                foreach (Transaction transaction in Transactions)
                {
                    Compte += "\n" + transaction.ToString();
                }
            }
            return Compte;
        }
    }
}

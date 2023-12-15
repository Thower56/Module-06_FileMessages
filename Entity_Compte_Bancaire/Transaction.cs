using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Compte_Bancaire
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public int CompteBancaireId { get; set; }

        public Transaction(int p_Id, string p_type,decimal p_montant, DateTime p_date, int p_CompteBancaireId)
        {
            Id = p_Id;
            Type = p_type;
            Montant = p_montant;
            Date = p_date;
            CompteBancaireId = p_CompteBancaireId;
        }

        public override string ToString()
        {
            return "Transaction - Id : " + Id + " Type : " + Type + " Montant : " + Montant + " Date : " + Date;
        }
    }
}

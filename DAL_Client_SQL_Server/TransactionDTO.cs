using Entity_Compte_Bancaire;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Compte_Bancaire_SQL_Server
{
    public class TransactionDTO
    {
        [Key]
        public int Id { get; set; }
        public string type { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public int CompteBancaireId { get; set; }

        public TransactionDTO(int p_Id, string p_type, decimal p_montant, DateTime p_date, int p_CompteBancaireId)
        {
            Id = p_Id;
            Montant = p_montant;
            Date = p_date;
            CompteBancaireId = p_CompteBancaireId;
        }
        public TransactionDTO(Transaction p_transaction)
        {
            Id = p_transaction.Id;
            Montant = p_transaction.Montant;
            Date = p_transaction.Date;
            CompteBancaireId = p_transaction.CompteBancaireId;
        }

        public TransactionDTO()
        {
        }

        public override string ToString()
        {
            return "Transaction - Id : " + Id + " Type : " + type + " Montant : " + Montant + " Date : " + Date;
        }

        public Transaction ToEntity()
        {
            return new Transaction(Id, type, Montant, Date, CompteBancaireId);
        }
    }
}

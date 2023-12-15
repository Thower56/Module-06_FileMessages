using Entity_Compte_Bancaire;

namespace ApiCompteBancaire.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string type { get; set; }
        public decimal Montant { get; set; }
        public DateTime Date { get; set; }
        public int CompteBancaireId { get; set; }

        public TransactionModel(int p_Id, string p_type, decimal p_montant, DateTime p_date)
        {
            Id = p_Id;
            Montant = p_montant;
            Date = p_date;
        }
        public TransactionModel(Transaction p_transaction)
        {
            Id = p_transaction.Id;
            Montant = p_transaction.Montant;
            Date = p_transaction.Date;
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

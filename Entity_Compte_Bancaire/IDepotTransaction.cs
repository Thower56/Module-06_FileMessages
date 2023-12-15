using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Compte_Bancaire
{
    public interface IDepotTransaction
    {
        public void AddTransaction(Transaction transaction);
        public IEnumerable<Transaction> GetTransactions();
        public Transaction GetTransaction(int id);
        public void UpdateTransaction(Transaction transaction);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Compte_Bancaire
{
    public class ManipulationTransaction
    {
        private IDepotTransaction m_Transaction;

        public ManipulationTransaction(IDepotTransaction transaction)
        {
            m_Transaction = transaction;
        }

        public void AddTransaction(Transaction transaction)
        {
            m_Transaction.AddTransaction(transaction);
        }

        public IEnumerable<Transaction> GetTransactions() 
        {
            return m_Transaction.GetTransactions();
        }

        public Transaction GetTransaction(int id)
        {
            return m_Transaction.GetTransaction(id);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            m_Transaction.UpdateTransaction(transaction);
        }

    }
}

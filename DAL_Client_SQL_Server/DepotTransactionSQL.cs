using Entity_Compte_Bancaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Compte_Bancaire_SQL_Server
{
    public class DepotTransactionSQL : IDepotTransaction
    {

        private readonly ApplicationDbContext m_Dbcontext;

        public DepotTransactionSQL(ApplicationDbContext dbContext)
        {
            m_Dbcontext = dbContext;
        }
        public void AddTransaction(Transaction transaction)
        {
            m_Dbcontext.Transactions.Add(new TransactionDTO(transaction));
            m_Dbcontext.SaveChanges();
        }

        public Transaction GetTransaction(int id)
        {
            return m_Dbcontext.Transactions.Find(id).ToEntity();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return m_Dbcontext.Transactions.Select(t => t.ToEntity());
        }

        public void UpdateTransaction(Transaction transaction)
        {
           m_Dbcontext.Transactions.Update(new TransactionDTO(transaction));
           m_Dbcontext.SaveChanges();
        }

    }
}

using DAL_Compte_Bancaire_SQL_Server;
using Entity_Compte_Bancaire;

namespace DAL_Client_SQL_Server
{
    public class DepotCompteBancaireSQL : IDepotCompteBancaire
    {
        private readonly ApplicationDbContext m_Dbcontext;

        public DepotCompteBancaireSQL(ApplicationDbContext p_Dbcontext)
        {
            m_Dbcontext = p_Dbcontext;
        }

        public void AddCompte(CompteBancaire p_compteBancaire)
        {
            m_Dbcontext.CompteBancaire.Add(new CompteBancaireDTO(p_compteBancaire));
            m_Dbcontext.SaveChanges();
        }

        public CompteBancaire GetCompte(int id)
        {
            CompteBancaireDTO compte = m_Dbcontext.CompteBancaire.Where(c => c.Id == id).FirstOrDefault();
            if (compte is not null)
            {
                return compte.ToEntity();
            }

            return null;
        }

        public IEnumerable<CompteBancaire> GetComptes()
        {
            return m_Dbcontext.CompteBancaire.Select(c => c.ToEntity());
        }

        public void UpdateCompte(CompteBancaire compteBancaire)
        {
            m_Dbcontext.CompteBancaire.Update(new CompteBancaireDTO(compteBancaire));
            m_Dbcontext.SaveChanges();
        }

    }
}
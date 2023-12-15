using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Compte_Bancaire
{
    public class ManipulationCompteBancaire
    {
        private IDepotCompteBancaire depotCompteBancaire;

        public ManipulationCompteBancaire(IDepotCompteBancaire depotCompteBancaire)
        {
            this.depotCompteBancaire = depotCompteBancaire;
        }

        public void AddCompte(CompteBancaire p_compteBancaire)
        {
            depotCompteBancaire.AddCompte(p_compteBancaire);
        }

        public IEnumerable<CompteBancaire> GetComptes()
        {
            return depotCompteBancaire.GetComptes();
        }

        public CompteBancaire GetCompte(int p_id)
        {
            return depotCompteBancaire.GetCompte(p_id);
        }

        public void UpdateCompte(CompteBancaire p_compteBancaire)
        {
            depotCompteBancaire.UpdateCompte(p_compteBancaire);
        }

    }
}

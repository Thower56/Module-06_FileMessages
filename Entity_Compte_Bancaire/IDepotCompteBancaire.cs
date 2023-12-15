namespace Entity_Compte_Bancaire
{
    public interface IDepotCompteBancaire
    {
        public void AddCompte(CompteBancaire compteBancaire);
        public IEnumerable<CompteBancaire> GetComptes();
        public CompteBancaire GetCompte(int id);
        public void UpdateCompte(CompteBancaire compteBancaire);

    }
}
using Entity_Compte_Bancaire;

namespace ApiCompteBancaire.Models
{
    public class CompteBancaireModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Courant";
        public CompteBancaireModel(string p_Type)
        {
            Type = p_Type;
        }

        public CompteBancaireModel(CompteBancaire p_compte)
        {
            if (p_compte == null)
            {
                throw new System.ArgumentNullException(nameof(p_compte));
            }
            if (p_compte is not null)
            {
                Type = p_compte.Type;
            }
        }

        public CompteBancaireModel()
        {
        }

        public override string ToString()
        {
            return "Compte - Id : " + Id + " Courant : " + Type;
        }

        public CompteBancaire ToEntity()
        {
            return new CompteBancaire(Type);
        }
    }
}

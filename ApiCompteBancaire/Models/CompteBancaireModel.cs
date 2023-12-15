using Entity_Compte_Bancaire;

namespace ApiCompteBancaire.Models
{
    public class CompteBancaireModel
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Courant";
        public CompteBancaireModel(int p_Id, string p_Type)
        {
            Id = p_Id;
            Type = p_Type;
        }

        public CompteBancaireModel(CompteBancaire p_compte)
        {
            Id = p_compte.Id;
            Type = p_compte.Type;

        }

        public override string ToString()
        {
            return "Compte - Id : " + Id + " Courant : " + Type;
        }

        public CompteBancaire ToEntity()
        {
            return new CompteBancaire(Id, Type);
        }
    }
}

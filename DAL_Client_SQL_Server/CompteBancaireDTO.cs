using Entity_Compte_Bancaire;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Compte_Bancaire_SQL_Server
{
    public class CompteBancaireDTO
    {
        [Key]
        public int Id { get; set; }

        [Column("TypeCompte")]
        public string Type { get; set; } = "Courant";
        public CompteBancaireDTO(string p_Type)
        {
            Type = p_Type;
        }

        public CompteBancaireDTO(CompteBancaire p_compte)
        {
            Type = p_compte.Type;
        }

        public CompteBancaireDTO()
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

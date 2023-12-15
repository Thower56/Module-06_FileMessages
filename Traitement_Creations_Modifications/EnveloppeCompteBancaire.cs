using Entity_Compte_Bancaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traitement_Creations_Modifications
{
    public class EnveloppeCompteBancaire
    {
        public string Action { get; set; }
        public string ActionEntity { get; set; }
        public Guid ActionId { get; set; }
        public CompteBancaire? CompteBancaire { get; set; }
        public Transaction? Transaction { get; set; }

        public EnveloppeCompteBancaire(string p_action, string p_actionId, CompteBancaire? p_compteBancaire, Transaction? p_transaction)
        {
            Action = p_action;
            ActionEntity = p_actionId;
            CompteBancaire = p_compteBancaire;
            Transaction = p_transaction;
            ActionId = new Guid();
        }
    }
}

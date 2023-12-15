using ApiCompteBancaire.Controllers;
using ApiCompteBancaire.Models;
using Entity_Compte_Bancaire;
using Microsoft.AspNetCore.Mvc;
using Traitement_Creations_Modifications;

namespace ApiTransactionController.Controllers
{
    [Route("api/CompteBancaire/{CompteId}/Transactions/")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ManipulationTransaction m_manipulationTransaction;

        public TransactionController(ManipulationTransaction p_manipulationTransaction)
        {
            m_manipulationTransaction = p_manipulationTransaction;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<TransactionModel>> Get(int CompteId)
        {
            return Ok(m_manipulationTransaction.GetTransactions().Where(t => t.CompteBancaireId == CompteId).Select(t => new TransactionModel(t)).ToList());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CompteBancaireModel> Get(int CompteId, int Id)
        {

            TransactionModel transaction = new TransactionModel(m_manipulationTransaction.GetTransaction(Id));
            if (transaction != null || transaction.CompteBancaireId == CompteId)
            {
                return Ok(transaction);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public ActionResult Post(int CompteId, [FromBody] TransactionModel p_TransactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (p_TransactionModel.CompteBancaireId == CompteId)
            {
                EnveloppeCompteBancaire enveloppe = new EnveloppeCompteBancaire("Create", "Transaction", null, p_TransactionModel.ToEntity());
                DataTransmission.Traitement(enveloppe);
            }
            
            //m_manipulationTransaction.AddTransaction(p_TransactionModel.ToEntity());
            return Accepted();
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(403)]
        public ActionResult Put(int Id, [FromBody] TransactionModel p_TransactionModel)
        {
            return Forbid();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(403)]
        public ActionResult Delete(int Id)
        {
            return Forbid();
        }
    }
}

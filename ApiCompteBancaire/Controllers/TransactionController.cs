using ApiCompteBancaire.Controllers;
using ApiCompteBancaire.Models;
using Entity_Compte_Bancaire;
using Microsoft.AspNetCore.Mvc;
using Traitement_Creations_Modifications;

namespace ApiTransactionController.Controllers
{
    [Route("api/CompteBancaire/{CompteId}/Transactions/{TransactionId}")]
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
        public ActionResult<IEnumerable<TransactionModel>> Get()
        {
            return Ok(m_manipulationTransaction.GetTransactions().Select(t => new TransactionModel(t)).ToList());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CompteBancaireModel> Get(int Id)
        {
            TransactionModel transaction = new TransactionModel(m_manipulationTransaction.GetTransaction(Id));
            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] TransactionModel p_TransactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            EnveloppeCompteBancaire enveloppe = new EnveloppeCompteBancaire("Create", "Transaction", null, p_TransactionModel.ToEntity());
            DataTransmission.Traitement(enveloppe);
            //m_manipulationTransaction.AddTransaction(p_TransactionModel.ToEntity());
            return CreatedAtAction(nameof(Get), new { id = p_TransactionModel.Id }, p_TransactionModel);
        }


        [ProducesResponseType(403)]
        public ActionResult Put(int TransactionId, [FromBody] TransactionModel p_TransactionModel)
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

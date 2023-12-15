using ApiCompteBancaire.Models;
using Entity_Compte_Bancaire;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Traitement_Creations_Modifications;
using System.Text;

namespace ApiCompteBancaire.Controllers
{

    [Route("api/CompteBancaire/{CompteId}")]
    [ApiController]
    public class CompteBancaireController : ControllerBase
    {
        private ManipulationCompteBancaire m_manipulationCompteBancaire;

        public CompteBancaireController(ManipulationCompteBancaire p_manipulationCompteBancaire)
        {
            m_manipulationCompteBancaire = p_manipulationCompteBancaire;
        }
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<CompteBancaireModel>> Get()
        {
            return Ok(m_manipulationCompteBancaire.GetComptes().Select(m => new CompteBancaireModel(m)).ToList());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CompteBancaireModel> Get(int Id)
        {
            CompteBancaireModel compteBancaire = new CompteBancaireModel(m_manipulationCompteBancaire.GetCompte(Id));
            if (compteBancaire != null)
            {
                return Ok(compteBancaire);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Post([FromBody] CompteBancaireModel p_CompteBancaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            // A verifier si le compte existe deja
            EnveloppeCompteBancaire enveloppe = new EnveloppeCompteBancaire("Create", "Compte", p_CompteBancaire.ToEntity(), null);
            DataTransmission.Traitement(enveloppe);

            return CreatedAtAction(nameof(Get), new { id = p_CompteBancaire.Id }, p_CompteBancaire);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Put(int Id, [FromBody] CompteBancaireModel p_CompteBancaire)
        {
            if (!ModelState.IsValid || p_CompteBancaire.Id != Id)
            {
                return BadRequest();
            }

            int index = m_manipulationCompteBancaire.GetComptes().ToList().FindIndex(c => c.Id == Id);

            if (index < 0)
            {
                return NotFound();
            }
            EnveloppeCompteBancaire enveloppe = new EnveloppeCompteBancaire("Update", "Compte", p_CompteBancaire.ToEntity(), null);
            DataTransmission.Traitement(enveloppe);


            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(403)]
        public ActionResult Delete(int Id)
        {
            return Forbid();
        }

        
    }
}

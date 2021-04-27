using Aether.Controllers.Context;
using Aether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Aether.Controllers
{
    public class AdoptionsController : ApiController
    {
        private readonly DataBaseContext context;

        public AdoptionsController()
        {
            context = new DataBaseContext();
        }

        public IHttpActionResult Get(int Id)
        {
            try
            {
                Adoption adoption = context.Adoption.Find(Id);

                if (adoption == null)
                {
                    return NotFound();
                }

                return Ok(adoption);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Get()
        {
            try
            {
                IList<Adoption> list = context.Adoption.ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Post([FromBody] Adoption adoption)
        {
            try
            {
                ModelErrors(adoption);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.Add(adoption);
                context.SaveChanges();
                string location = Url.Link("DefaultApi", new { controller = "adoptions", id = adoption.Id });
                return Created(new Uri(location), adoption);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Put(int Id, [FromBody] Adoption adoption)
        {
            try
            {
                if (Id != adoption.Id)
                {
                    return BadRequest("Os IDs de identificação não podem ser diferentes");
                }

                Adoption adoptionOld = context.Adoption.Find(Id);

                if (adoptionOld == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (adoption.AdoptionStatusId == AdoptionStatus.CANCELED || adoption.AdoptionStatusId == AdoptionStatus.RETURNED)
                {
                    AdoptionQueue queue = context.AdoptionQueue.Find(adoption.AdoptionQueueId);
                    queue.IsActive = false;
                    context.AdoptionQueue.Update(queue);
                }

                adoptionOld.AdoptionStatusId = adoption.AdoptionStatusId;

                context.Adoption.Update(adoptionOld);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private void ModelErrors(Adoption adoption)
        {
            try
            {
                List<Adoption> userAdoptions = context.Adoption.Where(a =>
                    a.UserId == adoption.UserId &&
                    a.AdoptionStatusId == AdoptionStatus.RETURNED &&
                    a.CreatedAt <= DateTime.Today.AddMonths(1)
                ).ToList();

                if (userAdoptions.Count > 0)
                {
                    ModelState.AddModelError("adoption.UserId", "Usuários que devolveram animais devem aguardar um mês antes de iniciar uma nova adoção.");
                }

                AdoptionQueue queue = context.AdoptionQueue.Find(adoption.AdoptionQueueId);
                if (queue == null)
                {
                    ModelState.AddModelError("adoption.AdoptionQueueId", "Fila de adoção não enontrada. É necessário estar numa fila para iniciar uma adoção.");
                }

                if (!queue.IsActive)
                {
                    ModelState.AddModelError("adoption.AdoptionQueueId", "Essa fila de adoção foi cancelada.");
                }

                IList<Adoption> animalAdoptions = context.Adoption.Where(a => a.AnimalId == adoption.AnimalId).ToList();
                if (animalAdoptions.Any(a => a.AdoptionStatusId == AdoptionStatus.FINISHED || a.AdoptionStatusId == AdoptionStatus.WAITING))
                {
                    ModelState.AddModelError("adoption.AnimalId", "Esse animal já foi adotado ou está em processo de adoção.");
                }

                if (adoption.CreatedAt == null)
                {
                    ModelState.AddModelError("adoption.CreatedAt", "Data da criação obrigatória.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Message", e.Message);
            }
        }
    }
}

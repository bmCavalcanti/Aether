using Aether.Controllers.Context;
using Aether.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Aether.Controllers
{
    public class AdoptionsController : ApiController
    {
        private static string TYPE_BY_ID = "Id";
        private static string TYPE_BY_ADOPTER = "Adopter";
        private static string TYPE_BY_DONOR = "Donor";

        private readonly DataBaseContext context;

        public AdoptionsController()
        {
            context = new DataBaseContext();
        }

        [Route("api/Adoptions/{type}/{id}")]
        public IHttpActionResult Get(int Id, string type = "Id")
        {
            try
            {
                if (type == TYPE_BY_ID)
                {
                    Adoption adoption = context.Adoption
                        .Include(a => a.Animal)
                        .Include(a => a.AdoptionStatus)
                        .Include(a => a.User)
                        .FirstOrDefault(a => a.Id == Id)
                    ;

                    if (adoption == null)
                    {
                        return NotFound();
                    }

                    return Ok(adoption);
                }
                else if (type == TYPE_BY_ADOPTER)
                {
                    IList<Adoption> list = context.Adoption
                        .Include(a => a.Animal)
                        .Include(a => a.AdoptionStatus)
                        .Include(a => a.User)
                        .Where(a => a.UserId == Id)
                        .ToList()
                    ;

                    return Ok(list);
                }
                else if (type == TYPE_BY_DONOR)
                {
                    IList<Adoption> list = context.Adoption
                        .Include(a => a.Animal)
                        .Include(a => a.AdoptionStatus)
                        .Include(a => a.User)
                        .Where(a => a.Animal.UserId == Id)
                        .ToList()
                    ;

                    return Ok(list);
                }
                else
                {
                    return BadRequest("Tipo inválido");
                }
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

                if (queue.AnimalId != adoption.AnimalId || queue.UserId != adoption.UserId)
                {
                    ModelState.AddModelError("adoption.AdoptionQueueId", "Fila inválida: Animal ou adotante não podem ser diferentes da fila.");
                }

                Adoption queueAdoption = context.Adoption.FirstOrDefault(a => a.AdoptionQueueId == queue.Id);
                if (queueAdoption != null)
                {
                    ModelState.AddModelError("adoption.AdoptionQueueId", "Fila inválida: Essa fila já gerou uma adoção.");
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

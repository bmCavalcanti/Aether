using Aether.Controllers.Context;
using Aether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Aether.Controllers
{
    public class AdoptionQueuesController : ApiController
    {
        private readonly DataBaseContext context;

        public AdoptionQueuesController()
        {
            context = new DataBaseContext();
        }

        public IHttpActionResult Get(int Id)
        {
            try
            {
                AdoptionQueue adoptionQueue = context.AdoptionQueue.Find(Id);

                if (adoptionQueue == null)
                {
                    return NotFound();
                }

                return Ok(adoptionQueue);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Get()
        {
            try
            {
                IList<AdoptionQueue> list = context.AdoptionQueue.ToList();
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Post([FromBody] AdoptionQueue adoptionQueue)
        {
            try
            {
                ModelErrors(adoptionQueue);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                context.AdoptionQueue.Add(adoptionQueue);
                context.SaveChanges();
                string location = Url.Link("DefaultApi", new { controller = "adoptionqueues", id = adoptionQueue.Id });
                return Created(new Uri(location), adoptionQueue);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public IHttpActionResult Put(int Id, [FromBody] AdoptionQueue adoptionQueue)
        {
            try
            {
                if (Id != adoptionQueue.Id)
                {
                    return BadRequest("Os IDs de identificação não podem ser diferentes");
                }

                AdoptionQueue adoptionQueueOld = context.AdoptionQueue.Find(Id);

                if (adoptionQueueOld == null)
                {
                    return NotFound();
                }

                Adoption adoption = context.Adoption.FirstOrDefault(a => a.AdoptionQueueId == Id);
                if (
                    adoption != null && 
                    (adoption.AdoptionStatusId == AdoptionStatus.FINISHED ||adoption.AdoptionStatusId == AdoptionStatus.WAITING)
                )
                {
                    ModelState.AddModelError("adoptionQueue.AnimalId", "Esse animal já foi adotado ou está em processo de adoção e sua fila não pode mais se alterada.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                adoptionQueueOld.IsActive = adoptionQueue.IsActive;

                context.AdoptionQueue.Update(adoptionQueueOld);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private void ModelErrors(AdoptionQueue adoptionQueue)
        {
            try
            {
                List<Adoption> userAdoptions = context.Adoption.Where(a =>
                    a.UserId == adoptionQueue.UserId &&
                    a.AdoptionStatusId == AdoptionStatus.RETURNED &&
                    a.CreatedAt <= DateTime.Today.AddMonths(1)
                ).ToList();

                if (userAdoptions.Count > 0)
                {
                    ModelState.AddModelError("adoptionQueue.UserId", "Usuários que devolveram animais devem aguardar um mês antes de iniciar uma nova adoção.");
                }

                IList<Adoption> adoptions = context.Adoption.Where(a => a.AnimalId == adoptionQueue.AnimalId).ToList();
                if (adoptions != null)
                {
                    if (adoptions.Any(adoption => adoption.AdoptionStatusId == AdoptionStatus.FINISHED))
                    {
                        ModelState.AddModelError("adoptionQueue.AnimalId", "Esse animal já foi adotado.");
                    }

                    if (adoptions.Any(adoption => adoption.AdoptionStatusId == AdoptionStatus.WAITING))
                    {
                        ModelState.AddModelError("adoptionQueue.AnimalId", "Esse animal já está em processo de adoção.");
                    }
                }

                AdoptionQueue queue = context.AdoptionQueue.FirstOrDefault(q => 
                    q.IsActive == true && 
                    q.UserId == adoptionQueue.UserId && 
                    q.AnimalId == adoptionQueue.AnimalId
                );

                if (queue != null)
                {
                    ModelState.AddModelError("adoptionQueue.AnimalId", "Esse animal já esta na fila de adoção desse usuário.");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Message", e.Message);
            }
        }
    }
}

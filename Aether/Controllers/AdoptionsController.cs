using Aether.Controllers.Context;
using Aether.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return BadRequest();
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

                ModelErrors(adoption);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                adoptionOld.AdoptionStatusId = adoption.AdoptionStatusId;

                context.Adoption.Update(adoptionOld);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        private void ModelErrors(Adoption adoption)
        {
            try
            {
                //if (scheduling.SchedulingTypeId.Equals(SchedulingType.TECHNICAL_ASSISTANCE) && scheduling.ServiceId == null)
                //{
                //    ModelState.AddModelError("scheduling.ServiceId", "Agendamentos do tipo '1 - ASSISTÊNCIA TÉCNICA' devem possuir um serviço.");
                //}
            }
            catch (Exception)
            {
                ModelState.AddModelError("Message", "Erro interno.");
            }
        }
    }
}

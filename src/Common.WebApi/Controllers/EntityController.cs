using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Common;

namespace Common.WebApi.Controllers
{
    [Authorize]
    public class EntityController<TEntity> : ApiController
       where TEntity : Entity
    {
        public EntityController(IUnitOfWork unitOfWork, IRepository<TEntity> entityRepository)
        {
            UnitOfWork = unitOfWork;
            EntityRepository = entityRepository;
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public IRepository<TEntity> EntityRepository { get; set; }

        // GET api/entity
        public virtual IEnumerable<TEntity> Get()
        {
            return EntityRepository.All.ToList();
        }

        // GET api/entity/5
        public virtual TEntity Get(int id)
        {
            return EntityRepository.Find(id);
        }

        // POST api/entity
        public virtual HttpResponseMessage Post([FromBody] TEntity entity)
        {

            if (ModelState.IsValid)
            {
                EntityRepository.InsertOrUpdate(entity);
                UnitOfWork.Commit();
                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                var uri = Url.Link("DefaultApi", new { id = entity.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT api/entity/5
        public virtual HttpResponseMessage Put(int id, [FromBody] TEntity entity)
        {
            if (ModelState.IsValid)
            {
                entity.Id = id;
                EntityRepository.InsertOrUpdate(entity);
                UnitOfWork.Commit();

                var response = Request.CreateResponse(HttpStatusCode.OK);
                var uri = Url.Link("DefaultApi", new { id = entity.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // DELETE api/entity/5
        public virtual HttpResponseMessage Delete(int id)
        {
            EntityRepository.Delete(id);
            UnitOfWork.Commit();
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
    }
}

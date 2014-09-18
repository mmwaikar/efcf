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
    public class LookupController<TLookup> : ApiController
        where TLookup : Lookup
    {
        public LookupController(IUnitOfWork unitOfWork, ILookupRepository<TLookup> lookupRepository)
        {
            UnitOfWork = unitOfWork;
            LookupRepository = lookupRepository;
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public ILookupRepository<TLookup> LookupRepository { get; set; }

        // GET api/lookup
        public virtual IEnumerable<TLookup> Get()
        {
            return LookupRepository.All.ToList();
        }

        // GET api/lookup/5
        public virtual TLookup Get(int id)
        {
            return LookupRepository.Find(id);
        }

        // POST api/lookup
        public virtual HttpResponseMessage Post([FromBody] TLookup lookup)
        {
            if (ModelState.IsValid)
            {
                LookupRepository.InsertOrUpdate(lookup);
                UnitOfWork.Commit();

                var response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                var uri = Url.Link("DefaultApi", new { id = lookup.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT api/lookup/5
        public virtual HttpResponseMessage Put(int id, [FromBody] TLookup lookup)
        {
            if (ModelState.IsValid)
            {
                lookup.Id = id;
                LookupRepository.InsertOrUpdate(lookup);
                UnitOfWork.Commit();

                var response = Request.CreateResponse(HttpStatusCode.OK);
                var uri = Url.Link("DefaultApi", new { id = lookup.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // DELETE api/lookup/5
        public virtual HttpResponseMessage Delete(int id)
        {
            LookupRepository.Delete(id);
            UnitOfWork.Commit();
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
    }
}
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Yangaroo.Core;
using Yangaroo.Core.Models;

namespace Yangaroo.Controllers
{
    public class CupCakeApiController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CupCakeApiController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [ResponseType(typeof(CupCake[]))]
        public HttpResponseMessage Get()
        {
           using (var unitofWork = _unitOfWorkFactory.Create())
           {
               var cupcake = unitofWork.CupCakeRepository.GetAll();

               return Request.CreateResponse(HttpStatusCode.OK, cupcake.ToList());
           }
        }

        [HttpPost]
        public IHttpActionResult PostCupCake([FromUri] int id,
            [FromUri] string name,
            [FromUri] string price,
            [FromUri] string ingredients)
        {
            if (ModelState.IsValid)
            {
                using (var unitofWork = _unitOfWorkFactory.Create())
                {
                    unitofWork.CupCakeRepository.Create(MapPostRequest(id,
                        name,
                        price,
                        ingredients));
                }
            }

            return Ok();//Redirect(new Uri("/", UriKind.Relative));
        }

        [HttpPost]
        public IHttpActionResult Post([FromUri] int id,
            [FromUri] string name,
            [FromUri] string price,
            [FromUri] string ingredients)
        {
            if (ModelState.IsValid)
            {
                using (var unitofWork = _unitOfWorkFactory.Create())
                {
                    unitofWork.CupCakeRepository.Create(MapPostRequest(id,
                        name,
                        price,
                        ingredients));
                }
            }

            return Ok();//Redirect(new Uri("/", UriKind.Relative));
        }

        [HttpPost]
        public IHttpActionResult Delete([FromUri] int id)
        {
            using (var unitofWork = _unitOfWorkFactory.Create())
            {
                unitofWork.CupCakeRepository.Delete(MapPostRequest(id, "fake", "0", "fake"));

                return Redirect(new Uri("/", UriKind.Relative));
            }
        }

        private static CupCake MapPostRequest(int id, string name, string price, string ingredients) 
        {
            return new CupCake
            {
                Id = id,
                Name = name,
                Price = decimal.Parse(price),
                Ingredients = ingredients
            };
        }
    }
}

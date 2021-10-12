using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EspressoShop.Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Review[] _reviews = {
             new Review
                {
                    ProductId = 1,
                    Id=  1,
                    Headline = "Very bad coffee!",
                    ReviewText = "The quality and flavour are very bad",
                    CreationDate = DateTime.Today,
                    ReviewerName = "Anup",
                    Stars = 1

                },
                new Review
                {
                    ProductId = 1,
                    Id=  2,
                    Headline = "Very bad test...there was no milk in the coffee.",
                    ReviewText = "Not recommend to any one",
                    CreationDate = DateTime.Today.AddDays(-20),
                    ReviewerName = "Avyaan",
                    Stars = 2
                }

            };

        public ReviewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{productId}")]
        public ActionResult<IEnumerable<Review>> Get(int productId)
        {
            if(productId %2 == 0)
            {
                Thread.Sleep(6000);
            }
            var serviceVersion = _configuration.GetValue<string>("SERVICE_VERSION");
            if(serviceVersion == "v1")
            {
                return _reviews.Select(x => { x.Stars = null; return x; }).Where(x => x.ProductId == productId).ToArray();
            }
            else
            {
                return _reviews.Where(x => x.ProductId == productId).ToArray();
            }
            
        }
    }
    public class Review
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreationDate { get; set; }
        public int ProductId { get; set; }
        public string ReviewerName { get; set; }
        public int? Stars { get; set; }
    }
}

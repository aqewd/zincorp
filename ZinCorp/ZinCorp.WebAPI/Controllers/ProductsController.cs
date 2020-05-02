using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ZinCorp.BE;
using ZinCorp.BL.Products;
using ZinCorp.DAL.Models;

namespace ZinCorp.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductsBL _productBL;

        public ProductsController( IProductsBL productBl)
        {
            _productBL = productBl;
        }

        [HttpGet]
        public List<Product> Get()
        {
            return _productBL.Get();
        }
    }
}

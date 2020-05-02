using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZinCorp.BE;
using ZinCorp.DAL;

namespace ZinCorp.BL.Products
{
    public class ProductsBL : IProductsBL
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public ProductsBL(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Product> Get()
        {
            return _mapper.Map<List<Product>>(_context.Products.Include(p => p.ProductImages).ToList());
        }
    }
}

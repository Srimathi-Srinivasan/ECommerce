using AutoMapper;
using Core.Interface;
using Core.Models;
using Core.Specification;
using ecommerceapi.Dtos;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ecommerceapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> productsrepo;
        private readonly IGenericRepository<ProductBrand> productbrandrepo;
        private readonly IGenericRepository<ProductType> producttyperepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> _productsrepo,
            IGenericRepository<ProductBrand> _productbrandrepo,
            IGenericRepository<ProductType> _producttyperepo, IMapper _mapper)
        {
            productsrepo = _productsrepo;
            productbrandrepo = _productbrandrepo;
            producttyperepo = _producttyperepo;
            mapper = _mapper;
        }
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await productsrepo.ListAsync(spec);

            return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await productsrepo.GetModelWithSpec(spec);

            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await productbrandrepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await producttyperepo.ListAllAsync());
        }
    }
}

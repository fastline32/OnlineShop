using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Dtos;
using OnlineShop.Errors;
using OnlineShop.Helpers;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = WebContains.AdminRole + "," + WebContains.ManagerRole)]
    public class BrandController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        [Cache(600)]
        public async Task<IActionResult> GetBrands ()
        {
            var productBrands = await _unitOfWork.Repository<ProductBrand>().ListAllAsync();
            return Ok(productBrands);
        }

        [Cache(600)]
        [HttpPost]
        public async Task<IActionResult> CreateBrand(ProductBrand brand)
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().ListAllAsync();
            foreach (var brandc in brands)
            {
                if (brandc.Name == brand.Name)
                {
                    return BadRequest(new ApiResponse(400));
                }
            }
            _unitOfWork.Repository<ProductBrand>().Add(brand);
            await _unitOfWork.Complete();
            return Ok();
        }

        [Cache(600)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditBrand(BrandDto brand)
        {            
            if (brand == null) return BadRequest(new ApiResponse(400));
            var newBrand = _mapper.Map<ProductBrand>(brand);
            _unitOfWork.Repository<ProductBrand>().Update(newBrand);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);
            _unitOfWork.Repository<ProductBrand>().Delete(brand);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}
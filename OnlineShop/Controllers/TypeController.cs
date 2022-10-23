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
    public class TypeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }
        [HttpGet]
        [Cache(600)]
        public async Task<IActionResult> GetTypes()
        {
            var productBrands = await _unitOfWork.Repository<ProductType>().ListAllAsync();
            return Ok(productBrands);
        }

        [Cache(600)]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateType(TypeDto type)
        {
            var types = await _unitOfWork.Repository<ProductType>().ListAllAsync();
            foreach (var typeIncl in types)
            {
                if (typeIncl.Name == type.Name)
                {
                    return BadRequest(new ApiResponse(400,"The type already exist"));
                }
            }

            if (!ValidationChecker.IsValid(type))
            {
                return BadRequest(new ApiResponse(400));
            }
            var newType =_mapper.Map<ProductType>(type);
            _unitOfWork.Repository<ProductType>().Add(newType);
            await _unitOfWork.Complete();
            return Ok();
        }

        [Cache(600)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditType(TypeDto type)
        {
            if(type == null)return BadRequest(new ApiResponse(400));
            var newType = _mapper.Map<ProductType>(type);
            if(newType == null) return BadRequest(new ApiResponse(400));
            
            _unitOfWork.Repository<ProductType>().Update(newType);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);
            _unitOfWork.Repository<ProductType>().Delete(type);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}
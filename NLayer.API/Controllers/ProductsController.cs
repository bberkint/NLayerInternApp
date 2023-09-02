﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        public ProductsController(IService<Product> service, IMapper mapper) { 
            _mapper = mapper;
            _service = service; 
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetBbyId(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDtos = _mapper.Map<List<ProductDto>>(product);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200));
        }

        [HttpPost] 
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
           // var productsDtos = _mapper.Map<List<ProductDto>>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }

        
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
             await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
            

        [HttpDelete("{id}")] 
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
             await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
        }
    }
}

﻿using Ecommerce.Application.Features.Product.Command.CreateProduct;
using Ecommerce.Application.Features.Product.Command.DeleteProduct;
using Ecommerce.Application.Features.Product.Command.UpdateProduct;
using Ecommerce.Application.Features.Product.Query.GetAdminProducts;
using Ecommerce.Application.Features.Product.Query.GetFeaturedProducts;
using Ecommerce.Application.Features.Product.Query.GetProductById;
using Ecommerce.Application.Features.Product.Query.GetProducts;
using Ecommerce.Application.Features.Product.Query.GetProductsByCategory;
using Ecommerce.Application.Features.Product.Query.GetProductSearchSuggestions;
using Ecommerce.Application.Features.Product.Query.SearchProducts;
using Ecommerce.Shared.Product;
using Ecommerce.Shared.Response.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> GetAdminProducts()
        {
            var response = await _mediator.Send(new GetAdminProductsQueryRequest());
            return Ok(response);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> CreateProduct(ProductDto product)
        {
            var response = await _mediator.Send(new CreateProductCommandRequest(product));
            return Ok(response);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> UpdateProduct(ProductDto product)
        {
            var response = await _mediator.Send(new UpdateProductCommandRequest(product));
            return Ok(response);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> DeleteProduct(int id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequest(id));
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IResponse>> GetProducts()
        {
            var response = await _mediator.Send(new GetProductsQueryRequest());
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<IResponse>> GetProduct(int productId)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequest(productId));
            return Ok(response);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<IResponse>> GetProductsByCategory(string categoryUrl)
        {
            var response = await _mediator.Send(new GetProductsByCategoryQueryRequest(categoryUrl));
            return Ok(response);
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<IResponse>> SearchProducts(string searchText, int page = 1)
        {
            var response = await _mediator.Send(new SearchProductsQueryRequest(searchText, page));
            return Ok(response);
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<IResponse>> GetProductSearchSuggestions(string searchText)
        {
            var response = await _mediator.Send(new GetProductSearchSuggestionsQueryRequest(searchText));
            return Ok(response);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<IResponse>> GetFeaturedProducts()
        {
            var response = await _mediator.Send(new GetFeaturedProductsQueryRequest());
            return Ok(response);
        }
    }
}

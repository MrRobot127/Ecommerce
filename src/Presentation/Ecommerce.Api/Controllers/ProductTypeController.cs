﻿using Ecommerce.Application.Features.Category.Commands.UpdateCategory;
using Ecommerce.Application.Features.Category.Query.GetCategories;
using Ecommerce.Application.Features.ProductType.Command.AddProductType;
using Ecommerce.Shared.Response.Abstract;
using Ecommerce.Shared.Response.Concrete;
using Ecommerce.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IResponse>> GetProductTypes()
        {
            var response = await _mediator.Send(new GetAllProductTypeQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<IResponse>> AddProductType(ProductTypeDto productType)
        {
            await _mediator.Send(new AddProductTypeCommandRequest(productType));

            var response = await _mediator.Send(new GetAllProductTypeQueryRequest());
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<IResponse>> UpdateProductType(ProductTypeDto productType)
        {
            var result = await _mediator.Send(new UpdateProductTypeCommandRequest(productType));

            if (!result.Success)
            {
                var responseCast = (DataResponse<string>)result;

                return new DataResponse<List<ProductTypeDto>>(new List<ProductTypeDto>(), responseCast.StatusCode, responseCast.Messages.FirstOrDefault());
            }

            var response = await _mediator.Send(new GetAllProductTypeQueryRequest());
            return Ok(response);
        }
    }
}

﻿using Ecommerce.Application.Features.Category.Commands.AddCategory;
using Ecommerce.Application.Features.Category.Commands.DeleteCategory;
using Ecommerce.Application.Features.Category.Commands.UpdateCategory;
using Ecommerce.Application.Features.Category.Query.GetCategories;
using Ecommerce.Shared.Category;
using Ecommerce.Shared.Response.Abstract;
using Ecommerce.Shared.Response.Concrete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IResponse>> GetCategories()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest());
            return Ok(response);
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> GetAdminCategories()
        {
            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpDelete("admin/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> DeleteCategory(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommandRequest(id));

            if (!result.Success)
            {
                var responseCast = (DataResponse<string>)result;

                return new DataResponse<List<CategoryDto>>(new List<CategoryDto>(), responseCast.StatusCode, responseCast.Messages.FirstOrDefault());
            }

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> AddCategory(CategoryDto category)
        {
            await _mediator.Send(new AddCategoryCommandRequest(category));

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }

        [HttpPut("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IResponse>> UpdateCategory(CategoryDto category)
        {
            var result = await _mediator.Send(new UpdateCategoryCommandRequest(category));

            if (!result.Success)
            {
                var responseCast = (DataResponse<string>)result;

                return new DataResponse<List<CategoryDto>>(new List<CategoryDto>(), responseCast.StatusCode, responseCast.Messages.FirstOrDefault());
            }

            var response = await _mediator.Send(new GetAllCategoryQueryRequest(true));
            return Ok(response);
        }
    }
}

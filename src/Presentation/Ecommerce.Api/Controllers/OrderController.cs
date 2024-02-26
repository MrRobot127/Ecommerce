using Ecommerce.Application.Features.Order.Query.GetOrder;
using Ecommerce.Application.Features.Order.Query.GetOrderDetails;
using Ecommerce.Shared.Response.Abstract;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IResponse>> GetOrders()
        {
            var response = await _mediator.Send(new GetOrderQueryRequest());
            return Ok(response);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<IResponse>> GetOrdersDetails(int orderId)
        {
            var response = await _mediator.Send(new GetOrderDeatilsQueryRequest(orderId));
            return Ok(response);
        }
    }
}

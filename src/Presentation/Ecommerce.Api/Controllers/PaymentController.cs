﻿using Ecommerce.Application.Contracts.Payment;
using Ecommerce.Application.Features.Cart.Query.GetDbCartProducts;
using Ecommerce.Application.Features.Order.Command.PlaceOrder;
using Ecommerce.Shared.Cart;
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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;

        public PaymentController(IPaymentService paymentService, IMediator mediator)
        {
            _paymentService = paymentService;
            _mediator = mediator;
        }

        [HttpPost("checkout"), Authorize]
        public async Task<ActionResult<IResponse>> CreateCheckoutSession()
        {
            var result = await _mediator.Send(new GetDbCartProductsQueryRequest());

            if (result.Success)
            {
                var resultCast = (DataResponse<List<CartProductResponse>>)result;
                var session = await _paymentService.CreateCheckoutSession(resultCast.Data);
                return Ok(session);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IResponse>> FulfillOrder()
        {
            var response = await _paymentService.FulfillOrder(Request);

            if (response.Success)
            {
                var result = await _mediator.Send(new GetDbCartProductsQueryRequest());

                if (result.Success)
                {
                    var resultCast = (DataResponse<List<CartProductResponse>>)result;
                    await _mediator.Send(new PlaceOrderCommandRequest(resultCast.Data));
                }
            }

            return Ok(response);
        }
    }
}

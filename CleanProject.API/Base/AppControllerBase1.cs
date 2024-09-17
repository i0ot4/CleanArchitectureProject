﻿using CleanProject.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanProject.API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase1 : ControllerBase
    {
        #region Fields
        private IMediator _mediatorInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        #endregion

        #region Actions
        public ObjectResult NewResult<T>(Result<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
        #endregion

    }
}

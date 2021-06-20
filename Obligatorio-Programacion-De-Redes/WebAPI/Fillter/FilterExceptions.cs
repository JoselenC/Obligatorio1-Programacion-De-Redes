using System;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Dto;

namespace WebAPI.Fillter
{
    public class FilterExceptions: Attribute,IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ErrorDto response = new ErrorDto()
            {
                IsSuccess = false,
                ErrorMessage = context.Exception.Message
            };

            RpcException error =(RpcException)context.Exception;
            if (error.StatusCode== StatusCode.NotFound)
            {
                response.Content = error.Message;
                response.Code = 404;
            }
            else if (error.StatusCode== StatusCode.AlreadyExists)
            {
                response.Content = error.Message;
                response.Code = 409;
            }
            else if (error.StatusCode== StatusCode.InvalidArgument)
            {
                response.Content = error.Message;
                response.Code = 422;
            }
            else
            {
                response.Content = error.Message;
                response.Code = 500;
                Console.WriteLine(context.Exception);
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Code
            };
        }

    }
}
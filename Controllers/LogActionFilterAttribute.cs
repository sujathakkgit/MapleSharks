using MapleSharks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using System;

namespace MapleSharks.Controllers
{
    internal class LogActionFilterAttribute : Attribute, IActionFilter

    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
            UserModel user = (UserModel)((Controller)context.Controller).ViewData.Model;

          // ILogger.GetInstance().Info("Leaving ");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
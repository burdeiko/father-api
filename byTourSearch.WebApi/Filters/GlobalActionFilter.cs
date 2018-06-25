using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace byTourSearch.WebApi.Filters
{
    public class GlobalActionFilter: ActionFilterAttribute
    {
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi.Tests;

public static class Extensions
{
    public static T GetObjectResult<T>(this ActionResult<T> actionResult)
    {
        if (actionResult.Result != null) return (T)((ObjectResult)actionResult.Result!).Value!;
        return actionResult.Value!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Test
{
    public static class ActionResultExtensions
    {
        public static T? GetValue<T>(this ActionResult<T> result) => result.Result is null
            ? result.Value
            : (T?)(result.Result as ObjectResult)?.Value;
    }
}

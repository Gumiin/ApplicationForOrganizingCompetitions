using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Server.Infrastructure
{
    public static class ApiResultExtensions
    {
        public static IActionResult ToGetResult(this ControllerBase ctrl, GeneralResponse result)
        {
            if (result is null) return ctrl.NotFound();
            if (result.Success) return ctrl.Ok(result);
            if (IsNotFound(result.Message)) return ctrl.NotFound(result);
            return ctrl.BadRequest(result);
        }

        public static IActionResult ToCreateResult(this ControllerBase ctrl, GeneralResponse result, string routeName, object routeValues)
        {
            if (result is null) return ctrl.BadRequest();
            if (result.Success) return ctrl.CreatedAtRoute(routeName, routeValues, result);
            if (IsConflict(result.Message)) return ctrl.Conflict(result);
            if (IsNotFound(result.Message)) return ctrl.NotFound(result);
            return ctrl.BadRequest(result);
        }

        public static IActionResult ToUpdateResult(this ControllerBase ctrl, GeneralResponse result)
        {
            if (result is null) return ctrl.BadRequest();
            if (result.Success) return ctrl.NoContent();
            if (IsNotFound(result.Message)) return ctrl.NotFound(result);
            if (IsConflict(result.Message)) return ctrl.Conflict(result);
            return ctrl.BadRequest(result);
        }

        public static IActionResult ToDeleteResult(this ControllerBase ctrl, GeneralResponse result)
        {
            if (result is null) return ctrl.BadRequest();
            if (result.Success) return ctrl.NoContent();
            if (IsNotFound(result.Message)) return ctrl.NotFound(result);
            return ctrl.BadRequest(result);
        }

        private static bool IsNotFound(string? msg)
            => msg?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true;

        private static bool IsConflict(string? msg)
            => msg?.Contains("exists", StringComparison.OrdinalIgnoreCase) == true
            || msg?.Contains("conflict", StringComparison.OrdinalIgnoreCase) == true;
    }
}
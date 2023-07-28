using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Manage.Farm.Service.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/manage/farm")]
public class BaseController : ControllerBase
{
    private const string ContentType = "application/json";

    protected static async Task<JsonResult> JsonResult<T>(Task<T> task) =>
        new(new Response<T>(await task)) { ContentType = ContentType };

    protected static JsonResult JsonResult<T>(T obj) =>
        new(new Response<T>(obj)) { ContentType = ContentType };

    protected static async Task<JsonResult> JsonResult(Task task, string? message = "",
        HttpStatusCode? httpStatusCode = HttpStatusCode.OK)
    {
        await task;
        return new JsonResult(new Response(true, message, (int)httpStatusCode)) { ContentType = ContentType };
    }
}

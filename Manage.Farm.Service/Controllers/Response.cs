using System.Net;

namespace Manage.Farm.Service.API.Controllers;

public record Response<T>(T Data,
bool Success = true,
string Message = "OK",
int StatusCode = (int)HttpStatusCode.OK);

public record Response(bool Success, string Message = "", int StatusCode = (int)HttpStatusCode.OK);



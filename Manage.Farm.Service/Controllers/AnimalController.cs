using Manage.Farm.Service.API.Controllers.Request;
using Manage.Farm.Service.API.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Manage.Farm.Service.API.Controllers;

public class AnimalController : BaseController
{
    private readonly IAnimalFacade _animalFacade;

    public AnimalController(IAnimalFacade animalFacade)
    {
        _animalFacade = animalFacade;
    }

    [HttpGet("animals")]
    public async Task<JsonResult> List() =>
        JsonResult(await _animalFacade.List());

    [HttpPost("animal")]
    public async Task<JsonResult> Add(AnimalRequest request) =>
        JsonResult(await _animalFacade.Add(request.ToEntity()));

    [HttpDelete("animal/{id}")]
    public async Task<JsonResult> Delete(string id) =>
        JsonResult(await _animalFacade.Delete(Guid.Parse(id)));
}
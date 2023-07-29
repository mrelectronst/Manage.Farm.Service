using Manage.Farm.Service.Domain;
using System.Net;

namespace Manage.Farm.Service.API.Application;
public class AnimalNotFoundException : BaseException
{
    public AnimalNotFoundException() : base((int)HttpStatusCode.NotFound,
        "Animal Not Found")
    {
    }
}

public class SameAnimalNameFoundException : BaseException
{
    public SameAnimalNameFoundException() : base((int)HttpStatusCode.NotFound,
        "Same Animal Name Found")
    {
    }
}

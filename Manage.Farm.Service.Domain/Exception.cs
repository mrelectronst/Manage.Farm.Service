namespace Manage.Farm.Service.Domain;

public class BaseException : Exception
{
    public BaseException(int code, string message)
    {
        Message = message;
        Code = code;
    }

    public int Code { get; }
    public override string Message { get; }
}

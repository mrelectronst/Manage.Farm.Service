namespace Manage.Farm.Service.Domain;

public record FilterPaging(int Page,
    int Size,
    string? SearchText = null) : Paging(Page, Size);

public record Paging(int Page, int Size)
{
    public int Skip => Size * (Page - 1);
    public int Take => Skip + (Size > 50 ? 50 : Size);
}

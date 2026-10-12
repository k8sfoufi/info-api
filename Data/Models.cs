namespace TodoInfo.Api.Data;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Color { get; set; } = "#64748b";
}

public class Priority
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Level { get; set; }
}

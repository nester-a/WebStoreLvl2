namespace WebStore.DTO;
public class EmployeeDTO
{
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? Patronymic { get; set; }
    public string? ShortName { get; set; }
    public int Age { get; set; }

}

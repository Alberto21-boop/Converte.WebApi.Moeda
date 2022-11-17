using Dapper;

namespace Converte.WebApi.Moeda.Model;

[Table("[Email]")]
public class Email
{
    [Key]
    [Column("Id")]
    [IgnoreInsert]
    public int Id { get; set; }
    public string Titulo_Email { get; set; } = default!;
    public string Corpo_Email { get; set; } = default!;

    public Email()
    {

    }
}

using System.ComponentModel.DataAnnotations;
namespace Domain;
public class User
{
    [Key]
    public int Id { get; set; }
    [MaxLength(30)]
    public string FirstName { get; set; }
    [MaxLength(30)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    [MaxLength(30)]
    public string City { get; set; }
    [MaxLength(13)]
    public string Phone { get; set; }
    public Status Status { get; set; }
    [MaxLength(100)]
    public string PhotoName { get; set; }
}

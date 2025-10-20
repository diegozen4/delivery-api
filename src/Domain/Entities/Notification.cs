
namespace Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public string Message { get; set; }
    public bool IsRead { get; set; }
    public DateTime Date { get; set; }
    public string NotificationType { get; set; }
}

using LocaMoto.Domain.Entities;
using MediatR;

namespace LocaMoto.Domain.Events
{
    public class UserRegisteredEvent(User user): INotification
    {
        public User User{ get; set; } = user;
    }
}

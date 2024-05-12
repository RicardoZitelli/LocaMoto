using LocaMoto.Domain.Entities;
using MediatR;

namespace LocaMoto.Domain.Events
{
    public class MotorcycleRegisteredEvent(Motorcycle motorcycle) : INotification
    {
        public Motorcycle Motorcycle { get; set; } = motorcycle;             
    }
}
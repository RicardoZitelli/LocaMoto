using LocaMotoDomain.Entities;
using MediatR;

namespace LocaMotoDomain.Events
{
    public class MotorcycleRegisteredEvent(Motorcycle motorcycle) : INotification
    {
        public Motorcycle Motorcycle { get; set; } = motorcycle;             
    }
}
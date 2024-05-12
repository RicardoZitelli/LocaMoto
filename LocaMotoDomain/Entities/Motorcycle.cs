using System.ComponentModel.DataAnnotations;

namespace LocaMotoDomain.Entities
{
    public sealed class Motorcycle
    {
        [Key]  
        public Guid Id { get; set; }
        public required int Year { get; set; } 
        public required string Model { get; set; }
        public required string LicensePlate { get; set; } 
        public DateTime CreationDate { get; set; } 
        public DateTime? UpdatedDate { get; set; } 
        public Guid User { get; set; }
        
        public Motorcycle()
        { }

        public Motorcycle(Guid id, int year, string model, string licensePlate, DateTime CreationDate, DateTime? UpdatedDate, Guid User)            
        {
            this.Id = id;
            this.Year = year;
            this.Model = model;
            this.LicensePlate = licensePlate;
            this.CreationDate = CreationDate;
            this.UpdatedDate = UpdatedDate;
            this.User = User;
        }
    }
}

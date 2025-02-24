// src/TravelAgency.Domain/Common/BaseEntity.cs
namespace TravelAgency.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
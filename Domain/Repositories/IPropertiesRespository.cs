using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertiesRespository
    {
        Task<Property> FindByIdAsync(Guid propertyId);
    }
}

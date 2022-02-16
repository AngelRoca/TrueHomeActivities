using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.RepositoriesPostgreEF
{
    public class PropertiesRespositoryPostgreEF : IPropertiesRespository
    {
        private readonly TrueHomeDataContext _dbContext;

        public PropertiesRespositoryPostgreEF(TrueHomeDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Property> FindByIdAsync(Guid propertyId)
        {
            return await _dbContext.Properties.FindAsync(propertyId);
        }
    }
}

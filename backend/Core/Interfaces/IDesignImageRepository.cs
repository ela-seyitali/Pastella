using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Core.Interfaces
{
    public interface IDesignImageRepository : IRepository<DesignImage>
    {
        // DesignImage'e özgü ek metotlar buraya eklenebilir.
        // Örneğin: Task<IEnumerable<DesignImage>> GetImagesBySweetDesignId(int sweetDesignId);
    }
} 
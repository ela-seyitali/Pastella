using Pastella.Backend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pastella.Backend.Core.Interfaces
{
    public interface ICakeRepository : IRepository<Cake>
    {
        // Cake'e özgü ek metotlar buraya eklenebilir.
        // Örneğin: Task<IEnumerable<Cake>> GetCakesBySweetDesignId(int sweetDesignId);
    }
} 
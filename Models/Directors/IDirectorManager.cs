using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_net_core_example_api.Models.Directors
{
    public interface IDirectorManager
    {
        Task<List<Director>> GetAll();
        Task<Director> GetById(long id);
        Task<bool> Create(Director director);
        Task<bool> Update(Director director);
        Task<bool> Remove(Director director);
    }
}
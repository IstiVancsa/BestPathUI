using Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IGoogleDataService
    {
        Task<IList<GoogleTextSearchDTO>> TextSearch(string searchText, LocationDTO locationDTO);
    }
}

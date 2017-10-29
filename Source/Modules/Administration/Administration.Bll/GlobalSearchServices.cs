using Administration.Bll.Dtos;
using StructureMap;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class GlobalSearchServices : IGlobalSearchServices
    {
        private readonly IContainer _container;

        public GlobalSearchServices(IContainer container)
        {
            _container = container;
        }

        public async Task<IList<GlobalSearchResultDto>> GetResults(string search)
        {
            var result = new List<GlobalSearchResultDto>();

            foreach (var plugin in _container.GetAllInstances<IGlobalSearchResult>())
            {
                result.AddRange(await plugin.GetResults(search));
            }

            return result;
        }
    }
}

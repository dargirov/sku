using StructureMap;

namespace Request.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<IRequestServices>().Use<RequestServices>());
        }
    }
}

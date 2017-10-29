using StructureMap;

namespace Administration.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<IUserServices>().Use<UserServices>());
            container.Configure(x => x.For<IOrganizationServices>().Use<OrganizationServices>());
            container.Configure(x => x.For<ICityServices>().Use<CityServices>());
            container.Configure(x => x.For<ICountryServices>().Use<CountryServices>());
            container.Configure(x => x.For<IFileServices>().Use<FileServices>());
            container.Configure(x => x.For<IAuthenticationServices>().Use<AuthenticationServices>());
            container.Configure(x => x.For<IAuthorizationServices>().Use<AuthorizationServices>());
            container.Configure(x => x.For<IGlobalSearchServices>().Use<GlobalSearchServices>());
            container.Configure(x => x.For<INotificationServices>().Use<NotificationServices>());
        }
    }
}

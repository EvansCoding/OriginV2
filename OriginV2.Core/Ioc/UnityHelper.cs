namespace OriginV2.Core.Ioc
{
    using System.Web.Mvc;
    using Data.Context;
    using Interfaces.IServices;
    using Services;
    using Interfaces;
    using Unity;
    using Unity.Lifetime;

    /// <summary>
    ///     Bind the given interface in request scope
    /// </summary>
    public static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }
    }

    /// <summary>
    ///     The injection for Unity
    /// </summary>
    public static class UnityHelper
    {
        public static IUnityContainer Container;

        public static void InitialiseUnityContainer()
        {
            Container = new UnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

            Container.BindInRequestScope<IConfigService, ConfigService>();
            Container.BindInRequestScope<ICacheService, CacheService>();
        }

        /// <summary>
        ///     Inject
        /// </summary>
        /// <returns></returns>
        public static void BuildUnityContainer()
        {
            Container.BindInRequestScope<IDataContext, DataContext>();
            Container.BindInRequestScope<IAccountService, AccountService>();
            Container.BindInRequestScope<IRoleService, RoleService>();
            Container.BindInRequestScope<IUserService, UserService>();
        }
    }
}

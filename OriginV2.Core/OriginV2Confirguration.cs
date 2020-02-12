namespace OriginV2.Core
{
    using Interfaces.IServices;
    using Unity;
    using Core.Ioc;

    public class OriginV2Confirguration
    {
        private string _originVersion;

        public string OriginVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_originVersion))
                {
                    _originVersion = GetConfig("OriginV2Version");
                }
                return _originVersion;
            }
        }

        // Database Connection Key
        public string OriginContext => GetConfig("DataContext");

        #region Singleton

        private static OriginV2Confirguration _instance;
        private static readonly object InstanceLock = new object();
        private static IConfigService _configService;

        private OriginV2Confirguration(IConfigService configService)
        {
            _configService = configService;
        }

        public static OriginV2Confirguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (InstanceLock)
                    {
                        if (_instance == null)
                        {
                            var configService = UnityHelper.Container.Resolve<IConfigService>();
                            _instance = new OriginV2Confirguration(configService);
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Generic Get
        public string GetConfig(string key)
        {
            var dict = _configService.GetConfig();
            if (!string.IsNullOrWhiteSpace(key) && dict.ContainsKey(key))
            {
                return dict[key];
            }
            return string.Empty;
        }
        #endregion
    }
}

﻿namespace OriginV2.Core.Interfaces.IServices
{
    using System.Collections.Generic;

    public partial interface IConfigService
    {
        Dictionary<string, string> GetConfig();
    }
}
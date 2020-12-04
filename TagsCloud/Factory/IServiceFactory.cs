using System;
using System.Collections.Generic;

namespace TagsCloud.Factory
{
    public interface IServiceFactory<TService>
    {
        TService Create();
        IEnumerable<string> GetServiceNames();
        IServiceFactory<TService> Register(string serviceName, Func<TService> wordConverter);
    }
}

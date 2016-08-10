using System;

namespace DependencyInjection.IApplicationService
{
    public interface IGuidAppService
    {
        Guid GuidItem();
    }


    public interface IGuidTransientAppService : IGuidAppService
    {
    }

    public interface IGuidScopedAppService : IGuidAppService
    {
    }

    public interface IGuidSingletonAppService : IGuidAppService
    {
    }
    
}
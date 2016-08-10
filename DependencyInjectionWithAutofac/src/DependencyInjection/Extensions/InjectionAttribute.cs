using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Extensions
{
    [AttributeUsage(AttributeTargets.Constructor |
                    AttributeTargets.Property |
                    AttributeTargets.Method)]
    public class InjectionAttribute : Attribute
    {
    }
}

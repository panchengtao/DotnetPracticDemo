using System;

namespace DependencyInjection.Extensions
{
    [AttributeUsage(AttributeTargets.Constructor |
                    AttributeTargets.Property |
                    AttributeTargets.Method)]
    public class InjectionAttribute : Attribute
    {
    }
}
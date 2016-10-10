using System;
using System.Linq;
using Elders.Cronus.DomainModeling;

namespace Elders.Cronus.Projections.Replay.Common.Extensions
{
    public static class ProjectionExtensions
    {
        public static Type[] GetEvents(this IProjection projection)
        {
            var type = projection.GetType();
            var events = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                .SelectMany(i => i.GetGenericArguments())
                .ToArray();

            return events;
        }
    }
}

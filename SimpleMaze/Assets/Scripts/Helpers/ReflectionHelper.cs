using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers {
    public class ReflectionHelper {
        public static IEnumerable<Type> GetAllWithSameType(Type type) {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type.IsAssignableFrom);
        }
    }
}
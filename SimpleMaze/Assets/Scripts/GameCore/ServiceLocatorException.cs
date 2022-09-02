using System;

namespace GameCore {
    public class ServiceLocatorException : Exception {
        public ServiceLocatorException(string message) : base(message) {}
    }
}
using System;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace GameCore {
    public class Core: MonoBehaviour {
        private static readonly Dictionary<Type, MonoBehaviourCoreService> _services = new();
        
        public static TService Get<TService>() where TService : MonoBehaviourCoreService, new() {
            var serviceType = typeof(TService);
            
            if (IsRegistered<TService>()) {
                return (TService) _services[serviceType];
            }
            
            throw new ServiceLocatorException($"{serviceType.Name} hasn't been registered");
        }
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            var monoBehaviourServices = ReflectionHelper.GetAllWithSameType(typeof(MonoBehaviourCoreService));
            foreach (var serviceType in monoBehaviourServices) {
                if (IsRegistered(serviceType)) {
                    continue;
                }
                
                AddMonoBehaviourService(serviceType);
            }
        }

        private static bool IsRegistered(Type t) => _services.ContainsKey(t);

        private static bool IsRegistered<TService>() => IsRegistered(typeof(TService));
        
        private static void AddMonoBehaviourService(Type serviceType) {
            var inGameService = FindObjectOfType(serviceType);
            _services[serviceType] = inGameService as MonoBehaviourCoreService;
        }
        
    }
}
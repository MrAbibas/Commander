using App.Core;
using UnityEngine;
using App.Utils;

namespace App.Services
{
    public interface IConfigurationService
    {
        T GetConfiguration<T>() where T : Configuration; 
    }
    public class ConfigurationService: IConfigurationService
    {
        private const string CONFIGURATION_PATH = "Configurations";

        private Configurations _configurations;

        public T GetConfiguration<T>() where T : Configuration
        {
            _configurations ??= Resources.Load<Configurations>(CONFIGURATION_PATH);
            return (T)_configurations.allConfigurations[typeof(T)];
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace MinTur.ServiceRegistration.Test.Dummies
{
    public class ConfigurationDummy : IConfiguration
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IConfigurationProvider> Providers => throw new NotImplementedException();

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}

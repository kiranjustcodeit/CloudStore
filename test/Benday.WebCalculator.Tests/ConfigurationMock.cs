using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Benday.WebCalculator.Tests
{
    public class ConfigurationMock : IConfiguration
    {
        public ConfigurationMock()
        {
            IndexerReturnValue = null;
        }
        public string IndexerReturnValue { get; set; }
        public string this[string key] { get => IndexerReturnValue; set => throw new NotImplementedException(); }

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
    }
}

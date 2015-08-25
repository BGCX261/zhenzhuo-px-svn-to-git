using System;
using System.Collections.Generic;
using System.Configuration;

namespace TrainEngine.Core.Providers
{
  /// <summary>
  /// A configuration section for web.config.
  /// </summary>
  /// <remarks>
  /// In the config section you can specify the provider you 
  /// want to use for TrainEngine.NET.
  /// </remarks>
  public class TrainProviderSection : ConfigurationSection
  {

    /// <summary>
    /// A collection of registered providers.
    /// </summary>
    [ConfigurationProperty("providers")]
    public ProviderSettingsCollection Providers
    {
      get { return (ProviderSettingsCollection)base["providers"]; }
    }

    /// <summary>
    /// The name of the default provider
    /// </summary>
    [StringValidator(MinLength = 1)]
    [ConfigurationProperty("defaultProvider", DefaultValue = "XmlTrainProvider")]
    public string DefaultProvider
    {
      get { return (string)base["defaultProvider"]; }
      set { base["defaultProvider"] = value; }
    }

  }
}

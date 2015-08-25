﻿using System;
using TrainEngine.Core.Providers;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml.Serialization;

namespace TrainEngine.Core.DataStore
{
  class StringDictionaryBehavior : ISettingsBehavior
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public StringDictionaryBehavior() { }

    private static TrainProviderSection _section = (TrainProviderSection)ConfigurationManager.GetSection("TrainEngine/blogProvider");
    /// <summary>
    /// Saves String Dictionary to Data Store
    /// </summary>
    /// <param name="exType">Extension Type</param>
    /// <param name="exId">Extension Id</param>
    /// <param name="settings">StringDictionary settings</param>
    /// <returns></returns>
    public bool SaveSettings(ExtensionType exType, string exId, object settings)
    {
      try
      {
        StringDictionary sd = (StringDictionary)settings;
        SerializableStringDictionary ssd = new SerializableStringDictionary();

        foreach (DictionaryEntry de in sd)
        {
          ssd.Add(de.Key.ToString(), de.Value.ToString());
        }

        TrainService.SaveToDataStore(exType, exId, ssd);
        return true;
      }
      catch (Exception)
      {
        throw;
      }
    }

    /// <summary>
    /// Retreaves StringDictionary object from database or file system
    /// </summary>
    /// <param name="exType">Extension Type</param>
    /// <param name="exId">Extension Id</param>
    /// <returns>StringDictionary object as Stream</returns>
    public object GetSettings(ExtensionType exType, string exId)
    {
      SerializableStringDictionary ssd = null;
      StringDictionary sd = new StringDictionary();
      XmlSerializer serializer = new XmlSerializer(typeof(SerializableStringDictionary));

      if (_section.DefaultProvider == "XmlTrainProvider")
      {
        Stream stm = (Stream)TrainService.LoadFromDataStore(exType, exId);
        if (stm != null)
        {
          ssd = (SerializableStringDictionary)serializer.Deserialize(stm);
          stm.Close();
          sd = (StringDictionary)ssd;
        }
      }
      else
      {
        object o = TrainService.LoadFromDataStore(exType, exId);
        if (!string.IsNullOrEmpty((string)o))
        {
          using (StringReader reader = new StringReader((string)o))
          {
            ssd = (SerializableStringDictionary)serializer.Deserialize(reader);
          }
          sd = (StringDictionary)ssd;
        }
      }
      return sd;
    }
  }
}

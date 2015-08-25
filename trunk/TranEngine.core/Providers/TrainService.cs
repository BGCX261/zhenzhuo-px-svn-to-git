#region Using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using System.IO;
using TrainEngine.Core;
using TrainEngine.Core.DataStore;
using TrainEngine.Core.Classes;

#endregion

namespace TrainEngine.Core.Providers
{
	/// <summary>
	/// The proxy class for communication between
	/// the business objects and the providers.
	/// </summary>
	public static class TrainService
	{

		#region Provider model

		private static TrainProvider _provider;
		private static TrainProviderCollection _providers;
		private static object _lock = new object();

		/// <summary>
		/// Gets the current provider.
		/// </summary>
		public static TrainProvider Provider
		{
			get { LoadProviders(); return _provider; }
		}

		/// <summary>
		/// Gets a collection of all registered providers.
		/// </summary>
		public static TrainProviderCollection Providers
		{
			get { LoadProviders(); return _providers; }
		}

		/// <summary>
		/// Load the providers from the web.config.
		/// </summary>
		private static void LoadProviders()
		{
			// Avoid claiming lock if providers are already loaded
			if (_provider == null)
			{
				lock (_lock)
				{
					// Do this again to make sure _provider is still null
					if (_provider == null)
					{
						// Get a reference to the <blogProvider> section
						TrainProviderSection section = (TrainProviderSection)WebConfigurationManager.GetSection("TrainEngine/TrainProvider");

						// Load registered providers and point _provider
						// to the default provider
						_providers = new TrainProviderCollection();
						ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(TrainProvider));
						_provider = _providers[section.DefaultProvider];

						if (_provider == null)
							throw new ProviderException("Unable to load default TrainProvider");
					}
				}
			}
		}

		#endregion

		#region Trainings

		/// <summary>
        /// Returns a Training based on the specified id.
		/// </summary>
		public static Training SelectTraining(Guid id)
		{
			LoadProviders();
			return _provider.SelectTraining(id);
		}

		///// <summary>
		///// Returns the content of a post.
		///// </summary>
		//public static string SelectTrainingContent(Guid id)
		//{
		//  LoadProviders();
		//  return _provider.SelectTrainingContent(id);
		//}

		/// <summary>
        /// Persists a new Training in the current provider.
		/// </summary>
        public static void InsertTraining(Training training)
		{
			LoadProviders();
            _provider.InsertTraining(training);
		}

		/// <summary>
        /// Updates an exsiting Training.
		/// </summary>
        public static void UpdateTraining(Training training)
		{
			LoadProviders();
            _provider.UpdateTraining(training);
		}

		/// <summary>
        /// Deletes the specified Training from the current provider.
		/// </summary>
        public static void DeleteTraining(Training training)
		{
			LoadProviders();
            _provider.DeleteTraining(training);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public static List<Training> FillTrainings()
		{
			LoadProviders();
            return _provider.FillTrainings();
		}

		#endregion

        #region Curricula

        /// <summary>
		/// Returns a Page based on the specified id.
		/// </summary>
        public static Curricula SelectCurricula(Guid id)
		{
			LoadProviders();
            return _provider.SelectCurricula(id);
		}

		/// <summary>
		/// Persists a new Page in the current provider.
		/// </summary>
        public static void InsertCurricula(Curricula curricula)
		{
			LoadProviders();
            _provider.InsertCurricula(curricula);
		}

		/// <summary>
		/// Updates an exsiting Page.
		/// </summary>
        public static void UpdateCurricula(Curricula curricula)
		{
			LoadProviders();
            _provider.UpdateCurricula(curricula);
		}

		/// <summary>
		/// Deletes the specified Page from the current provider.
		/// </summary>
        public static void DeleteCurricula(Curricula curricula)
		{
			LoadProviders();
            _provider.DeleteCurricula(curricula);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public static List<Curricula> FillCurriculas()
		{
			LoadProviders();
            return _provider.FillCurriculas();
		}

		#endregion

        #region Fields

        /// <summary>
        /// Returns a Field based on the specified id.
        /// </summary>
        public static Field SelectField(Guid id)
        {
            LoadProviders();
            return _provider.SelectField(id);
        }

        /// <summary>
        /// Persists a new Field in the current provider.
        /// </summary>
        public static void InsertField(Field field)
        {
            LoadProviders();
            _provider.InsertField(field);
        }

        /// <summary>
        /// Updates an exsiting Field.
        /// </summary>
        public static void UpdateField(Field field)
        {
            LoadProviders();
            _provider.UpdateField(field);
        }

        /// <summary>
        /// Deletes the specified Category from the current provider.
        /// </summary>
        public static void DeleteField(Field field)
        {
            LoadProviders();
            _provider.DeleteField(field);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Field> FillFields()
        {
            LoadProviders();
            return _provider.FillFields();
        }
        
        #endregion

        #region Excellents

        /// <summary>
        /// Returns a Field based on the specified id.
        /// </summary>
        public static Excellent SelectExcellent(Guid id)
        {
            LoadProviders();
            return _provider.SelectExcellent(id);
        }

        /// <summary>
        /// Persists a new Excellent in the current provider.
        /// </summary>
        public static void InsertExcellent(Excellent excellent)
        {
            LoadProviders();
            _provider.InsertExcellent(excellent);
        }

        /// <summary>
        /// Updates an exsiting Excellent.
        /// </summary>
        public static void UpdateExcellent(Excellent excellent)
        {
            LoadProviders();
            _provider.UpdateExcellent(excellent);
        }

        /// <summary>
        /// Deletes the specified Excellent from the current provider.
        /// </summary>
        public static void DeleteExcellent(Excellent excellent)
        {
            LoadProviders();
            _provider.DeleteExcellent(excellent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Excellent> FillExcellents()
        {
            LoadProviders();
            return _provider.FillExcellents();
        }

        #endregion

        #region Ress

        /// <summary>
        /// Returns a Res based on the specified id.
        /// </summary>
        public static Res SelectRes(Guid id)
        {
            LoadProviders();
            return _provider.SelectRes(id);
        }

        /// <summary>
        /// Persists a new Res in the current provider.
        /// </summary>
        public static void InsertRes(Res res)
        {
            LoadProviders();
            _provider.InsertRes(res);
        }

        /// <summary>
        /// Updates an exsiting Res.
        /// </summary>
        public static void UpdateRes(Res res)
        {
            LoadProviders();
            _provider.UpdateRes(res);
        }

        /// <summary>
        /// Deletes the specified Res from the current provider.
        /// </summary>
        public static void DeleteRes(Res res)
        {
            LoadProviders();
            _provider.DeleteRes(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Res> FillRess()
        {
            LoadProviders();
            return _provider.FillRess();
        }

        public static int UpdateBlob(Res res)
        {
            LoadProviders();
            return _provider.UpateBlob(res);
        }

        public static byte[] GetBlob(Res res)
        {
            LoadProviders();
            return _provider.GetBlob(res);
        }
        #endregion

        #region CurriculaInfos

        /// <summary>
        /// Returns a CurriculaInfo based on the specified id.
        /// </summary>
        public static CurriculaInfo SelectCurriculaInfo(Guid id)
        {
            LoadProviders();
            return _provider.SelectCurriculaInfo(id);
        }

        /// <summary>
        /// Persists a new CurriculaInfo in the current provider.
        /// </summary>
        public static void InsertCurriculaInfo(CurriculaInfo info)
        {
            LoadProviders();
            _provider.InsertCurriculaInfo(info);
        }

        /// <summary>
        /// Updates an exsiting CurriculaInfo.
        /// </summary>
        public static void UpdateCurriculaInfo(CurriculaInfo info)
        {
            LoadProviders();
            _provider.UpdateCurriculaInfo(info);
        }

        /// <summary>
        /// Deletes the specified CurriculaInfo from the current provider.
        /// </summary>
        public static void DeleteCurriculaInfo(CurriculaInfo info)
        {
            LoadProviders();
            _provider.DeleteCurriculaInfo(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<CurriculaInfo> FillCurriculaInfos()
        {
            LoadProviders();
            return _provider.FillCurriculaInfos();
        }

        #endregion

		#region Categories

		/// <summary>
		/// Returns a Category based on the specified id.
		/// </summary>
		public static Category SelectCategory(Guid id)
		{
			LoadProviders();
			return _provider.SelectCategory(id);
		}

		/// <summary>
		/// Persists a new Category in the current provider.
		/// </summary>
		public static void InsertCategory(Category category)
		{
			LoadProviders();
			_provider.InsertCategory(category);
		}

		/// <summary>
		/// Updates an exsiting Category.
		/// </summary>
		public static void UpdateCategory(Category category)
		{
			LoadProviders();
			_provider.UpdateCategory(category);
		}

		/// <summary>
		/// Deletes the specified Category from the current provider.
		/// </summary>
		public static void DeleteCategory(Category category)
		{
			LoadProviders();
			_provider.DeleteCategory(category);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static List<Category> FillCategories()
		{
			LoadProviders();
			return _provider.FillCategories();
		}

		#endregion

        #region Author profiles

        /// <summary>
        /// Returns a Page based on the specified id.
        /// </summary>
        public static AuthorProfile SelectProfile(string id)
        {
            LoadProviders();
            return _provider.SelectProfile(id);
        }

        /// <summary>
        /// Persists a new Page in the current provider.
        /// </summary>
        public static void InsertProfile(AuthorProfile profile)
        {
            LoadProviders();
            _provider.InsertProfile(profile);
        }

        /// <summary>
        /// Updates an exsiting Page.
        /// </summary>
        public static void UpdateProfile(AuthorProfile profile)
        {
            LoadProviders();
            _provider.UpdateProfile(profile);
        }

        /// <summary>
        /// Deletes the specified Page from the current provider.
        /// </summary>
        public static void DeleteProfile(AuthorProfile profile)
        {
            LoadProviders();
            _provider.DeleteProfile(profile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<AuthorProfile> FillProfiles()
        {
            LoadProviders();
            return _provider.FillProfiles();
        }

        #endregion

        public static void UpdateViewCount(string table, string where, string viewCount)
        {
            LoadProviders();
            _provider.UpdateViewCount(table, where, viewCount );
        }

        public static void UpdateViewCount(AuthorProfile ap)
        {
            LoadProviders();
            _provider.UpdateViewCount(ap);
        }
        #region User Historys

        /// <summary>
        /// Returns a Page based on the specified id.
        /// </summary>
        public static UserHis SelectUserHis(string id)
        {
            LoadProviders();
            return _provider.SelectUserHis(id);
        }

        /// <summary>
        /// Persists a new Page in the current provider.
        /// </summary>
        public static void InsertUserHis(UserHis his)
        {
            LoadProviders();
            _provider.InsertUserHis(his);
        }

        /// <summary>
        /// Updates an exsiting Page.
        /// </summary>
        public static void UpdateUserHis(UserHis his)
        {
            LoadProviders();
            _provider.UpdateUserHis(his);
        }

        /// <summary>
        /// Deletes the specified Page from the current provider.
        /// </summary>
        public static void DeleteUserHis(UserHis his)
        {
            LoadProviders();
            _provider.DeleteUserHis(his);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<UserHis> FillUserHiss()
        {
            LoadProviders();
            return _provider.FillUserHiss();
        }

        #endregion
		#region Settings

		/// <summary>
		/// Loads the settings from the provider and returns
		/// them in a StringDictionary for the TrainSettings class to use.
		/// </summary>
		public static System.Collections.Specialized.StringDictionary LoadSettings()
		{
			LoadProviders();
			return _provider.LoadSettings();
		}

		/// <summary>
		/// Save the settings to the current provider.
		/// </summary>
		public static void SaveSettings(System.Collections.Specialized.StringDictionary settings)
		{
			LoadProviders();
			_provider.SaveSettings(settings);
		}

		#endregion

		#region Stop words

		/// <summary>
		/// Loads the stop words from the data store.
		/// </summary>
		public static StringCollection LoadStopWords()
		{
			LoadProviders();
			return _provider.LoadStopWords();
		}

		#endregion

		#region Data Store
		/// <summary>
		/// Loads settings from data storage
		/// </summary>
		/// <param name="exType">Extension Type</param>
		/// <param name="exId">Extension ID</param>
		/// <returns>Settings as stream</returns>
		public static object LoadFromDataStore(ExtensionType exType, string exId)
		{
			LoadProviders();
			return _provider.LoadFromDataStore(exType, exId);
		}

		/// <summary>
		/// Saves settings to data store
		/// </summary>
		/// <param name="exType">Extension Type</param>
		/// <param name="exId">Extensio ID</param>
		/// <param name="settings">Settings object</param>
		public static void SaveToDataStore(ExtensionType exType, string exId, object settings)
		{
			LoadProviders();
			_provider.SaveToDataStore(exType, exId, settings);
		}

		/// <summary>
		/// Removes object from data store
		/// </summary>
		/// <param name="exType">Extension Type</param>
		/// <param name="exId">Extension Id</param>
		public static void RemoveFromDataStore(ExtensionType exType, string exId)
		{
			LoadProviders();
			_provider.RemoveFromDataStore(exType, exId);
		}

		///<summary>
		///</summary>
		///<returns></returns>
		public static string GetStorageLocation()
		{
			LoadProviders();
			return _provider.StorageLocation();
		}
		#endregion

        

        

    }
}

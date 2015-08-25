#region Using

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

#endregion

namespace TrainEngine.Core.Providers
{
    /// <summary>
    /// A base class for all custom providers to inherit from.
    /// </summary>
    public abstract partial class TrainProvider : ProviderBase
    {
        #region  Training(内训)
        /// <summary>
        /// Retrieves a Training from the provider based on the specified id.
        /// </summary>
        public abstract Training SelectTraining(Guid id);

        /// <summary>
        /// Inserts a new Training into the data store specified by the provider.
        /// </summary>
        public abstract void InsertTraining(Training training);
        /// <summary>
        /// Updates an existing Training in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateTraining(Training training);
        /// <summary>
        /// Deletes a Training from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteTraining(Training training);
        /// <summary>
        /// Retrieves all Posts from the provider and returns them in a List.
        /// </summary>
        public abstract List<Training> FillTrainings();
        #endregion

        #region  Curricula(公开课)
        /// <summary>
        /// Retrieves a Curricula from the provider based on the specified id.
        /// </summary>
        public abstract Curricula SelectCurricula(Guid id);
        /// <summary>
        /// Inserts a new Curricula into the data store specified by the provider.
        /// </summary>
        public abstract void InsertCurricula(Curricula page);
        /// <summary>
        /// Updates an existing Curricula in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateCurricula(Curricula page);
        /// <summary>
        /// Deletes a Curricula from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteCurricula(Curricula page);
        /// <summary>
        /// Retrieves all Pages from the provider and returns them in a List.
        /// </summary>
        public abstract List<Curricula> FillCurriculas();
        #endregion

        #region Profile(用户详情)
        // Profile
        /// <summary>
        /// Retrieves a AuthorProfile from the provider based on the specified id.
        /// </summary>
        public abstract AuthorProfile SelectProfile(string id);
        /// <summary>
        /// Inserts a new AuthorProfile into the data store specified by the provider.
        /// </summary>
        public abstract void InsertProfile(AuthorProfile profile);
        /// <summary>
        /// Updates an existing AuthorProfile in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateProfile(AuthorProfile profile);
        /// <summary>
        /// Deletes a AuthorProfile from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteProfile(AuthorProfile profile);
        /// <summary>
        /// Retrieves all AuthorProfile from the provider and returns them in a List.
        /// </summary>
        public abstract List<AuthorProfile> FillProfiles();
        #endregion

        #region (用户操作历史)
        public abstract UserHis SelectUserHis(string id);
        /// <summary>
        /// Inserts a new UserHis into the data store specified by the provider.
        /// </summary>
        public abstract void InsertUserHis(UserHis his);
        /// <summary>
        /// Updates an existing UserHis in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateUserHis(UserHis his);
        /// <summary>
        /// Deletes a UserHis from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteUserHis(UserHis his);
        /// <summary>
        /// Retrieves all UserHiss from the provider and returns them in a List.
        /// </summary>
        public abstract List<UserHis> FillUserHiss();
        #endregion

        #region Category(分类)
        /// <summary>
        /// Retrieves a Category from the provider based on the specified id.
        /// </summary>
        public abstract Category SelectCategory(Guid id);
        /// <summary>
        /// Inserts a new Category into the data store specified by the provider.
        /// </summary>
        public abstract void InsertCategory(Category category);
        /// <summary>
        /// Updates an existing Category in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateCategory(Category category);
        /// <summary>
        /// Deletes a Category from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteCategory(Category category);
        /// <summary>
        /// Retrieves all Categories from the provider and returns them in a List.
        /// </summary>
        public abstract List<Category> FillCategories();
        #endregion

        #region Field(领域)
        /// <summary>
        /// Retrieves a Field from the provider based on the specified id.
        /// </summary>
        public abstract Field SelectField(Guid id);
        /// <summary>
        /// Inserts a new Field into the data store specified by the provider.
        /// </summary>
        public abstract void InsertField(Field field);
        /// <summary>
        /// Updates an existing Field in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateField(Field field);
        /// <summary>
        /// Deletes a Category from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteField(Field field);
        /// <summary>
        /// Retrieves all Categories from the provider and returns them in a List.
        /// </summary>
        public abstract List<Field> FillFields();
        #endregion

        #region Excellent(精彩)
        /// <summary>
        /// Retrieves a Excellent from the provider based on the specified id.
        /// </summary>
        public abstract Excellent SelectExcellent(Guid id);
        /// <summary>
        /// Inserts a new Excellent into the data store specified by the provider.
        /// </summary>
        public abstract void InsertExcellent(Excellent excellent);
        /// <summary>
        /// Updates an existing Excellent in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateExcellent(Excellent excellent);
        /// <summary>
        /// Deletes a Excellent from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteExcellent(Excellent excellent);
        /// <summary>
        /// Retrieves all Excellent from the provider and returns them in a List.
        /// </summary>
        public abstract List<Excellent> FillExcellents();
        #endregion

        #region Res(多媒体资源)
        /// <summary>
        /// Retrieves a Res from the provider based on the specified id.
        /// </summary>
        public abstract Res SelectRes(Guid id);
        /// <summary>
        /// Inserts a new Res into the data store specified by the provider.
        /// </summary>
        public abstract void InsertRes(Res res);
        /// <summary>
        /// Updates an existing Res in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateRes(Res res);
        /// <summary>
        /// Deletes a Res from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteRes(Res res);
        /// <summary>
        /// Retrieves all Res from the provider and returns them in a List.
        /// </summary>
        public abstract List<Res> FillRess();

        public abstract int UpateBlob(Res res);

        public abstract byte[] GetBlob(Res res);
        #endregion

        #region CurriculaInfo(课程安排)
        /// <summary>
        /// Retrieves a CurriculaInfo from the provider based on the specified id.
        /// </summary>
        public abstract CurriculaInfo SelectCurriculaInfo(Guid id);
        /// <summary>
        /// Inserts a new CurriculaInfo into the data store specified by the provider.
        /// </summary>
        public abstract void InsertCurriculaInfo(CurriculaInfo curriculaInfo);
        /// <summary>
        /// Updates an existing CurriculaInfo in the data store specified by the provider.
        /// </summary>
        public abstract void UpdateCurriculaInfo(CurriculaInfo curriculaInfo);
        /// <summary>
        /// Deletes a Category from the data store specified by the provider.
        /// </summary>
        public abstract void DeleteCurriculaInfo(CurriculaInfo curriculaInfo);
        /// <summary>
        /// Retrieves all Categories from the provider and returns them in a List.
        /// </summary>
        public abstract List<CurriculaInfo> FillCurriculaInfos();
        #endregion

        public abstract void UpdateViewCount(string table, string id, string viewCount);
        public abstract void UpdateViewCount(AuthorProfile ap);
        // Settings
        /// <summary>
        /// Loads the settings from the provider.
        /// </summary>
        public abstract StringDictionary LoadSettings();
        /// <summary>
        /// Saves the settings to the provider.
        /// </summary>
        public abstract void SaveSettings(StringDictionary settings);


        //Stop words
        /// <summary>
        /// Loads the stop words used in the search feature.
        /// </summary>
        public abstract StringCollection LoadStopWords();

        // Data Store
        /// <summary>
        /// Loads settings from data store
        /// </summary>
        /// <param name="exType">Extension Type</param>
        /// <param name="exId">Extensio Id</param>
        /// <returns>Settings as stream</returns>
        public abstract object LoadFromDataStore(DataStore.ExtensionType exType, string exId);
        /// <summary>
        /// Saves settings to data store
        /// </summary>
        /// <param name="exType">Extension Type</param>
        /// <param name="exId">Extension Id</param>
        /// <param name="settings">Settings object</param>
        public abstract void SaveToDataStore(DataStore.ExtensionType exType, string exId, object settings);
        /// <summary>
        /// Removes settings from data store
        /// </summary>
        /// <param name="exType">Extension Type</param>
        /// <param name="exId">Extension Id</param>
        public abstract void RemoveFromDataStore(DataStore.ExtensionType exType, string exId);
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string StorageLocation();


    }

    /// <summary>
    /// A collection of all registered providers.
    /// </summary>
    public class TrainProviderCollection : ProviderCollection
    {
        /// <summary>
        /// Gets a provider by its name.
        /// </summary>
        public new TrainProvider this[string name]
        {
            get { return (TrainProvider)base[name]; }
        }

        /// <summary>
        /// Add a provider to the collection.
        /// </summary>
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is TrainProvider))
                throw new ArgumentException
                    ("Invalid provider type", "provider");

            base.Add(provider);
        }
    }

}

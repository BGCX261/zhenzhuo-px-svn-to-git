using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.Core.Providers;

namespace TrainEngine.Core.Classes
{
    [Serializable]
    public class CurriculaInfo : BusinessBase<CurriculaInfo, Guid>, IComparable<Category>
    {
        #region Constructor

		/// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
		/// </summary>
		public CurriculaInfo()
		{
            base.Id = Guid.NewGuid();
            _StartDate = new DateTime();
            _EndDate = new DateTime();
            _IsPublished = true;
		}

		

		#endregion
        private Guid _CurriculaId;
        public Guid CurriculaId
        {
            get { return _CurriculaId; }
            set
            {
                if (_CurriculaId != value) MarkChanged("CurriculaId");
                _CurriculaId = value;
            } 
        }
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value) MarkChanged("StartDate");
                _StartDate = value;
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value) MarkChanged("EndDate");
                _EndDate = value;
            }
        }

        private int _Cast;
        public int Cast
        {
            get { return _Cast; }
            set
            {
                if (_Cast != value) MarkChanged("Cast");
                _Cast = value;
            }
        }
        private string _CityTown;
        public string CityTown
        {
            get { return _CityTown; }
            set
            {
                if (_CityTown != value) MarkChanged("CityTown");
                _CityTown = value;
            }
        }
        private bool _IsPublished;
        public bool IsPublished
        {
            get { return _IsPublished; }
            set
            {
                if (_IsPublished != value) MarkChanged("IsPublished");
                _IsPublished = value;
            }
        }
        private static object _SyncRoot = new object();
        private static List<CurriculaInfo> _CurriculaInfos;
        /// <summary>
        /// Gets an unsorted list of all Categories.
        /// </summary>
        public static List<CurriculaInfo> CurriculaInfos
        {
            get
            {
                if (_CurriculaInfos == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_CurriculaInfos == null)
                        {
                            _CurriculaInfos = TrainService.FillCurriculaInfos();
                            _CurriculaInfos.Sort(delegate(CurriculaInfo obj1, CurriculaInfo obj2)
                            {
                                return obj1.StartDate.CompareTo(obj2.StartDate);
                            });
                            _CurriculaInfos.TrimExcess();
                        }
                    }
                }

                return _CurriculaInfos;
            }
        }
        /// <summary>
        /// Get all Curricula in this CurriculaInfo.
		/// </summary>
        public Curricula Curricula
		{
            get { return Curricula.GetCurriculaByCurriculaInfo(this.Id); }
		}
        protected override void ValidationRules()
        {
            //AddRule("StartDate", "StartDate must be set", DateTime.IsNullOrEmpty(StartDate));
        }

        protected override CurriculaInfo DataSelect(Guid id)
        {
            return TrainService.SelectCurriculaInfo(id);
        }

        protected override void DataUpdate()
        {
            if (IsChanged)
                TrainService.UpdateCurriculaInfo(this);
        }

        protected override void DataInsert()
        {
            if (IsNew)
                TrainService.InsertCurriculaInfo(this);
        }

        protected override void DataDelete()
        {
            if (IsDeleted)
            {

                TrainService.DeleteCurriculaInfo(this);
            }
            if (CurriculaInfos.Contains(this))
                CurriculaInfos.Remove(this);

            Dispose();
        }

        #region IComparable<Category> 成员

        public int CompareTo(Category other)
        {
            throw new NotImplementedException();
        }

        #endregion


        public static CurriculaInfo GetCurriculaInfo(Guid infoId)
        {
            foreach (CurriculaInfo field in CurriculaInfos)
            {
                if (field.Id == infoId)
                    return field;
            }

            return null;
        }
        
    }
}

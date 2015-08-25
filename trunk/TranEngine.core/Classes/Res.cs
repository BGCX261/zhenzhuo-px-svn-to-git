using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.Core.Providers;
using System.Web;
using System.IO;

namespace TrainEngine.Core.Classes
{
    public class Res : BusinessBase<Res, Guid>, IComparable<Res>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Res"/> class.
		/// </summary>
        public Res()
		{
			Id = Guid.NewGuid();
            _IsPublished = false;
            _Points = 0;
		}

        private string _FileName;
        /// <summary>
        /// Gets or sets the Title or the object.
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName != value) MarkChanged("FiledName");
                _FileName = value;
            }
        }
        private string _ResType;
        public string ResType
        {
             get { return _ResType; }
            set
            {
                if (_ResType != value) MarkChanged("ResType");
                _ResType = value;
            }
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value) MarkChanged("Description");
                _Description = value;
            }
        }
        private string _Author;
        public string Author
        {
            get { return _Author; }
            set
            {
                if (_Author != value) MarkChanged("Author");
                _Author = value;
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
        private int _Points;
        public int Points
        {
            get { return _Points; }
            set
            {
                if (_Points != value) MarkChanged("Points");
                _Points = value;
            }
        }
        private byte[] _CurrentPostFileBuffer;
        public byte[] CurrentPostFileBuffer
        {
            get {
                if (_CurrentPostFileBuffer == null)
                {
                    _CurrentPostFileBuffer = TrainService.GetBlob(this);
                }
                return _CurrentPostFileBuffer; }
            set
            {
                _CurrentPostFileBuffer = value;
            }
        }
        private string _NoMess;
        public string NoMess
        {
            get { return _NoMess; }
            set
            {
                if (value != _NoMess) MarkChanged("NoMess");
                _NoMess = value;
            }
        }
        public int BlobUpdate()
        {
            if (CurrentPostFileBuffer.LongLength>0)
            {
                return TrainService.UpdateBlob(this);
            }
            else
            {
                return -1;
            }
        }

        public string GetResTempFilePath()
        {
            string file = Utils.ApplicationRoot() + "LoadTemp/" + this.FileName;
            if (!File.Exists(file))
            {
                byte[] buff = this.CurrentPostFileBuffer;
                File.WriteAllBytes(Utils.ApplicationRoot() + "LoadTemp/" + this.FileName, buff);
            }

            return Utils.RelativeWebRoot + "LoadTemp/" + this.FileName;
        }
        private static object _SyncRoot = new object();
        private static List<Res> _Ress;

        /// <summary>
        /// A sorted collection of all Trainings in the Web.
        /// Sorted by date.
        /// </summary>
        public static List<Res> Ress
        {
            get
            {
                if (_Ress == null)
                {
                    lock (_SyncRoot)
                    {
                        if (_Ress == null)
                        {
                            _Ress = TrainService.FillRess();
                            _Ress.TrimExcess();
                        }
                    }
                }

                return _Ress;
            }
        }

        /// <summary>
        /// Returns a Res based on the specified id.
        /// </summary>
        public static Res GetRes(Guid id)
        {
            foreach (Res res in Ress)
            {
                if (res.Id == id)
                    return res;
            }

            return null;
        }
        
        protected override void ValidationRules()
        {
            AddRule("FileName", "FileName must be set", string.IsNullOrEmpty(FileName));
        }

        /// <summary>
        /// Retrieves the object from the data store and populates it.
        /// </summary>
        /// <param name="id">The unique identifier of the object.</param>
        /// <returns>
        /// True if the object exists and is being populated successfully
        /// </returns>
        protected override Res DataSelect(Guid id)
        {
            return TrainService.SelectRes(id);
        }

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected override void DataUpdate()
        {
            if (IsChanged)
                TrainService.UpdateRes(this);
        }

        /// <summary>
        /// Inserts a new object to the data store.
        /// </summary>
        protected override void DataInsert()
        {
            if (IsNew)
                TrainService.InsertRes(this);
        }

        /// <summary>
        /// Deletes the object from the data store.
        /// </summary>
        protected override void DataDelete()
        {
            if (IsDeleted)
            {

                TrainService.DeleteRes(this);
            }
            if (Ress.Contains(this))
                Ress.Remove(this);

            Dispose();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return CompleteTitle();
        }

        #region IComparable<Res> 成员

        public int CompareTo(Res other)
        {
            return this.CompleteTitle().CompareTo(other.CompleteTitle());
        }

        /// <summary>
        /// Gets the full title with Parent names included
        /// </summary>
        public string CompleteTitle()
        {

            return _FileName;

        }

        #endregion
    }
}

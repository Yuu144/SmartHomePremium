using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniversalServer.ViewModelBase
{
    [Serializable]
    public abstract class ViewModel : IViewModel
    {
        protected ViewModel()
        {
            var initializationTask = new Task(() => Initialize());
            initializationTask.ContinueWith(result => InitializationCompletedCallback(result));
            initializationTask.Start();
        }

        protected virtual void Initialize()
        {
        }

        private void InitializationCompletedCallback(IAsyncResult result)
        {
            var initializationCompleted = InitializationCompleted;
            if (initializationCompleted != null)
            {
                InitializationCompleted(this, new AsyncCompletedEventArgs(null, !result.IsCompleted, result.AsyncState));
            }
            InitializationCompleted = null;
        }

        public event AsyncCompletedEventHandler InitializationCompleted;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public abstract class ViewModel<TModel> : ViewModel, IViewModel<TModel> where TModel : class
    {
        private TModel _model;

        [Browsable(false)]
        [Bindable(false)]
        public TModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (Model != value)
                {
                    var properties = this.GetType().GetProperties(BindingFlags.Public);
                    var oldValues = properties.Select(p => p.GetValue(this, null));
                    var enumerator = oldValues.GetEnumerator();

                    _model = value;

                    foreach (var property in properties)
                    {
                        enumerator.MoveNext();
                        var oldValue = enumerator.Current;
                        var newValue = property.GetValue(this, null);

                        if ((oldValue == null && newValue != null)
                            || (oldValue != null && newValue == null)
                            || (!oldValue.Equals(newValue)))
                        {
                            OnPropertyChanged(property.Name);
                        }
                    }
                }
            }
        }

        protected ViewModel(TModel model) : base()
        {
            this._model = model;
        }

        public override int GetHashCode()
        {
            return Model.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as IViewModel<TModel>;

            if (other == null)
                return false;

            return Equals(other);
        }

        public bool Equals(IViewModel<TModel> other)
        {
            if (other == null)
                return false;

            if (Model == null)
                return Model == other.Model;

            return Model.Equals(other.Model);
        }
    }
}
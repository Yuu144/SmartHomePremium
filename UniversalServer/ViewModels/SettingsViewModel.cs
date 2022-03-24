using CommandHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalServer.Model;
using UniversalServer.ViewModelBase;

namespace UniversalServer.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        private string _messageFromFileAccess;

        public string MessageFromFileAccess
        {
            get { return _messageFromFileAccess; }
            set { _messageFromFileAccess = value;
                OnPropertyChanged("MessageFromFileAccess");
            }
        }
        public string DBIPAddress { get; set; }

        ICommand _saveSettingsCommand;



        public ICommand SaveSettingsCommand
        {
            get
            {
                if (_saveSettingsCommand == null)
                {
                    _saveSettingsCommand = new RelayCommand(c => ExecuteSaveSettingsCommand());
                }
                return _saveSettingsCommand;

            }
        }

        private void ExecuteSaveSettingsCommand()
        {
            FileAccess fa = new FileAccess();
            fa.DataSaved += Fa_DataSaved;

            fa.SaveSettings(DBIPAddress);

        }

        private void Fa_DataSaved(string msg)
        {
            _messageFromFileAccess = msg;
        }
    }
}

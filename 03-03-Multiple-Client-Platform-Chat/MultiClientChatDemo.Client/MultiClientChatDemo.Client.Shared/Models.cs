using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MultiClientChatDemo.Client
{
    public class ChatClientViewModel : BaseViewModel
    {
        public ChatClientViewModel()
        {
            this.Messages = new ObservableCollection<string>();
        }

        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged("Messages");
            }
        }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MultiClientChatDemo.Client
{
    public class ChatClientViewModel : BaseViewModel
    {
        public ChatClientViewModel()
        {
            this.Messages = new ObservableCollection<ChatMessage>();
        }

        private ObservableCollection<ChatMessage> _messages;
        public ObservableCollection<ChatMessage> Messages
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

    public class ChatMessage : BaseViewModel
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _sender;
        public string Sender
        {
            get { return _sender; }
            set
            {
                _sender = value;
                OnPropertyChanged("Sender");
            }
        }
    }
}

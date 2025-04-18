using System.Collections.ObjectModel;
using System.Windows;
using Chatter.Client.Models;

namespace Chatter.Client;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<ChatMessage> _messages = [];
    
    private readonly User _user;
    
    public MainWindow()
    {
        InitializeComponent();
        ChatMessageList.ItemsSource = _messages;
        _user = new User
        {
            Id = Guid.Parse("fb1f803e-fb73-4cd8-a3ca-d6e63e3cb48a"),
            UserName = "guy"
        };
    }

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        _messages.Add(new ChatMessage
        {
            Sender = _user.UserName,
            MessageContent = MessageBox.Text,
            SentAt = DateTime.UtcNow
        });
        MessageBox.Clear();
    }
}
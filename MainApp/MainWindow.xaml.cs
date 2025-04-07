using DeviceControl.Messages;
using ExchnageClient;
using SDCIPCCore;
using System;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    IMessageService messageService;
    public MainWindow()
    {
        this.messageService = new MessageService(App.UniqueId);
        InitializeComponent();

    }

    private void deviceOn_Click(object sender, RoutedEventArgs e)
    {
        var deviceOnMessage = new DeviceOnMessage
        {
            DeviceName = "1788 Camera"

        };

        messageService.SendMessage("DeviceControl", deviceOnMessage);
    }

    private void deviceOff_Click(object sender, RoutedEventArgs e)
    {
        var deviceOFFMessage = new DeviceOffMessage
        {
            DeviceName = "1788 Camera"

        };

        messageService.SendMessage("DeviceControl", deviceOFFMessage);

    }

    private async void updateProperty_Click(object sender, RoutedEventArgs e)
    {
        var deviceOnMessage = new UpdatePropertyMessage
        {
            DeviceName = "1788 Camera",
            PropertyName = "MyTestProperty",
            PropertyValue = "100"
        };

        Mouse.OverrideCursor = Cursors.Wait;
        
        var result = await messageService.SendMessage("DeviceControl", deviceOnMessage);
        if (result != null)
        {
            MessageBox.Show(result.Reply, "My Response");
        }

        Mouse.OverrideCursor = Cursors.Arrow;

    }
}
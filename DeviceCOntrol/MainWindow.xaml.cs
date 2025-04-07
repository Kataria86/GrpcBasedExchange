using ExchnageClient;
using SDCIPCCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceCOntrol
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMessageService messageService = new MessageService("DeviceControl");
        public MainWindow()
        {
            List<IMessageHandler> handlers = new List<IMessageHandler>();
            var deviceStartHandler = new DeviceStartHander();
            deviceStartHandler.Processed += DeviceStartHandler_Processed;
            handlers.Add(deviceStartHandler);

            var deviceStopHandler = new DeviceStopHander();
            deviceStopHandler.Processed += DeviceStopHandler_Processed;
            handlers.Add(deviceStopHandler);

            var deviceSetProperty = new DeviceSetPropertyHandler();
            deviceSetProperty.Processed += DeviceSetProperty_Processed;
            handlers.Add(deviceSetProperty);

            messageService.RegisterClient("DeviceControl", new MessageProcessorCore(handlers));
            InitializeComponent();
        }

        private void DeviceSetProperty_Processed(object? sender, dynamic e)
        {
            string message = "Message Recieved to set the Property";
            UpdateGui(e, message);
        }

        private void UpdateGui(dynamic e, string message)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                this.txtStatus.Text = message;
                this.jsonText.Text = e.JSON;
            }));
        }

        private void DeviceStopHandler_Processed(object? sender, dynamic e)
        {
            string message  = "Stop Message Recieved";
            UpdateGui(e, message);


        }

        private void DeviceStartHandler_Processed(object? sender, dynamic e)
        {
            string message = "Device start Message Received";
            UpdateGui(e, message);
            
        }
    }
}
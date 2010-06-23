using System.Diagnostics;
using System.Windows;

namespace GearLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void launchClientBtn_Click(object sender, RoutedEventArgs e)
        {
            Process proc = Process.Start(App.GearClientExe);
            proc.Start();
        }

        private void launchServerBtn_Click(object sender, RoutedEventArgs e)
        {
            Process proc = Process.Start(App.GearServerExe);
        }

        private void launchEditorBtn_Click(object sender, RoutedEventArgs e)
        {
            Process proc = Process.Start(App.GearEditExe);
        }
    }
}

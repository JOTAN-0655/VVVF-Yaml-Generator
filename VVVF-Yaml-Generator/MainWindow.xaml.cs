using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using YamlDotNet.Serialization;

namespace VVVF_Data_Generator
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

        private void setting_button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string name = button.Name;
            if (name.Equals("settings_level"))
                setting_window.Navigate(new Uri("Pages/Settings/level_setting.xaml", UriKind.Relative));
            else if(name.Equals("settings_minimum"))
                setting_window.Navigate(new Uri("Pages/Settings/minimum_freq_setting.xaml", UriKind.Relative));
            else if(name.Equals("settings_mascon"))
                setting_window.Navigate(new Uri("Pages/Settings/mascon_off_setting.xaml", UriKind.Relative));
        }

        private void file_button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Object? tag = button.Tag;
            if (tag == null) return;
            if (tag.Equals("Load"))
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "Yaml (*.yaml)|*.yaml|All (*.*)|*.*"
                };
                if (dialog.ShowDialog() == false) return;

                if (Yaml_Generation.load_Yaml(dialog.FileName))
                    MessageBox.Show("Load OK.", "Great", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Invalid yaml or path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (tag.Equals("Save"))
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Yaml (*.yaml)|*.yaml"
                };

                // ダイアログを表示する
                if (dialog.ShowDialog() == false) return;

                if (Yaml_Generation.save_Yaml(dialog.FileName))
                    MessageBox.Show("Save OK.", "Great", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Error occurred on saving.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

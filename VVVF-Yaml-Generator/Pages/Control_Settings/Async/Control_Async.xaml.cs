using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VVVF_Data_Generator;
using VVVF_Yaml_Generator.Pages.Control_Settings.Async;
using static VVVF_Data_Generator.Yaml_Sound_Data;
using static VVVF_Data_Generator.Yaml_Sound_Data.Yaml_Control_Data.Yaml_Async_Parameter.Yaml_Async_Parameter_Carrier_Freq;

namespace VVVF_Yaml_Generator.Pages.Control_Settings
{
    /// <summary>
    /// Control_Async.xaml の相互作用ロジック
    /// </summary>
    public partial class Control_Async : UserControl
    {
        Yaml_Control_Data data;
        MainWindow MainWindow;
        public Control_Async(Yaml_Control_Data ycd, MainWindow mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;
            data = ycd;

            apply_data();
        }

        private void apply_data()
        {
            Yaml_Async_Carrier_Mode[] modes = (Yaml_Async_Carrier_Mode[])Enum.GetValues(typeof(Yaml_Async_Carrier_Mode));
            carrier_freq_mode.ItemsSource = modes;
            carrier_freq_mode.SelectedItem = data.async_data.carrier_wave_data.carrier_mode;
        }

        private void carrier_freq_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Yaml_Async_Carrier_Mode selected = (Yaml_Async_Carrier_Mode) carrier_freq_mode.SelectedItem;

            if (selected == Yaml_Async_Carrier_Mode.Const)
                carrier_setting.Navigate(new Control_Async_Const(data, MainWindow));
            else if(selected == Yaml_Async_Carrier_Mode.Moving)
                carrier_setting.Navigate(new Control_Async_Moving(data, MainWindow));
        }
    }
}

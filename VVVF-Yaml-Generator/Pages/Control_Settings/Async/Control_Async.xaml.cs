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
using VVVF_Yaml_Generator.Pages.Control_Settings.Async.Vibrato;
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

        bool no_update = true;
        public Control_Async(Yaml_Control_Data ycd, MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            data = ycd;

            InitializeComponent();

            apply_data();

            no_update = false;
        }

        private void apply_data()
        {
            Yaml_Async_Carrier_Mode[] modes = (Yaml_Async_Carrier_Mode[])Enum.GetValues(typeof(Yaml_Async_Carrier_Mode));
            carrier_freq_mode.ItemsSource = modes;
            carrier_freq_mode.SelectedItem = data.async_data.carrier_wave_data.carrier_mode;

            random_box.Text = data.async_data.random_range.ToString();

            show_selected(data.async_data.carrier_wave_data.carrier_mode);
        }

        private void carrier_freq_mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (no_update) return;

            Yaml_Async_Carrier_Mode selected = (Yaml_Async_Carrier_Mode) carrier_freq_mode.SelectedItem;

            data.async_data.carrier_wave_data.carrier_mode = selected;

            show_selected(selected);


        }

        private void show_selected(Yaml_Async_Carrier_Mode selected)
        {
            if (selected == Yaml_Async_Carrier_Mode.Const)
                carrier_setting.Navigate(new Control_Async_Const(data, MainWindow));
            else if (selected == Yaml_Async_Carrier_Mode.Moving)
                carrier_setting.Navigate(new Control_Async_Moving(data, MainWindow));
            else if (selected == Yaml_Async_Carrier_Mode.Vibrato)
                carrier_setting.Navigate(new Control_Async_Vibrato(data, MainWindow));
            else
                carrier_setting.Navigate(new Control_Async_Table(data, MainWindow));
        }

        private int parse_i(TextBox tb)
        {
            try
            {
                tb.Background = new BrushConverter().ConvertFrom("#FFFFFFFF") as Brush;
                return Int32.Parse(tb.Text);
            }
            catch
            {
                tb.Background = new BrushConverter().ConvertFrom("#FFfed0d0") as Brush;
                return -1;
            }
        }

        private void random_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (no_update) return;
            TextBox tb = (TextBox)sender;
            int d = parse_i(tb);

            data.async_data.random_range = d;
        }
    }
}

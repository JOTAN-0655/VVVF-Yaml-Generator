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
using static VVVF_Data_Generator.Yaml_Sound_Data;
using static VVVF_Data_Generator.Yaml_Sound_Data.Yaml_Control_Data.Yaml_Control_Data_Amplitude_Control;
using static VVVF_Data_Generator.Yaml_Sound_Data.Yaml_Control_Data.Yaml_Control_Data_Amplitude_Control.Yaml_Control_Data_Amplitude;

namespace VVVF_Yaml_Generator.Pages.Control_Settings
{
    /// <summary>
    /// Control_Amplitude.xaml の相互作用ロジック
    /// </summary>
    public partial class Control_Amplitude : UserControl
    {
        private Yaml_Control_Data_Amplitude target;
        private MainWindow mainWindow;
        private Control_Amplitude_Content content;

        
        public Control_Amplitude(Yaml_Control_Data_Amplitude ycd, Control_Amplitude_Content cac, MainWindow mainWindow)
        {
            InitializeComponent();

            target = ycd;
            this.mainWindow = mainWindow;
            content = cac;

            if (cac == Control_Amplitude_Content.Default)
                title.Content = "Default Amplitude Setting";
                
            else if (cac == Control_Amplitude_Content.Free_Run_On)
                title.Content = "Mascon On Free Run Amplitude Setting";

            else
                title.Content = "Mascon Off Free Run Amplitude Setting";

            grid_hider(ycd.mode,cac);

            apply_view();
        }

        private void apply_view()
        {
            Amplitude_Mode[] modes = (Amplitude_Mode[])Enum.GetValues(typeof(Amplitude_Mode));
            amplitude_mode_selector.ItemsSource = modes;
            amplitude_mode_selector.SelectedItem = target.mode;

            start_freq_box.Text = target.parameter.start_freq.ToString();
            start_amp_box.Text = target.parameter.start_amp.ToString();
            end_freq_box.Text = target.parameter.end_freq.ToString();
            end_amp_box.Text = target.parameter.end_amp.ToString();
            cutoff_amp_box.Text = target.parameter.cut_off_amp.ToString();
            max_amp_box.Text = target.parameter.max_amp.ToString();
            polynomial_box.Text = target.parameter.polynomial.ToString();
            curve_rate_box.Text = target.parameter.curve_change_rate.ToString();
            disable_range_limit_check.IsChecked = target.parameter.disable_range_limit;
        }

        private double parse_d(TextBox tb)
        {
            try
            {
                tb.Background = new BrushConverter().ConvertFrom("#FFFFFFFF") as Brush;
                return Double.Parse(tb.Text);
            }
            catch
            {
                tb.Background = new BrushConverter().ConvertFrom("#FFfed0d0") as Brush;
                return -1;
            }
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

        private void textbox_change(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Object? tag = tb.Tag;
            if (tag == null) return;

            if (tag.Equals("start_freq"))
                target.parameter.start_freq = parse_d(tb);
            else if (tag.Equals("start_amp"))
                target.parameter.start_amp = parse_d(tb);
            else if (tag.Equals("end_freq"))
                target.parameter.end_freq = parse_d(tb);
            else if (tag.Equals("end_amp"))
                target.parameter.end_amp = parse_d(tb);
            else if (tag.Equals("cutoff_amp"))
                target.parameter.cut_off_amp = parse_d(tb);
            else if (tag.Equals("max_amp"))
                target.parameter.max_amp = parse_d(tb);
            else if(tag.Equals("curve_rate"))
                target.parameter.curve_change_rate = parse_d(tb);
            else if (tag.Equals("polynomial"))
                target.parameter.polynomial = parse_i(tb);

            mainWindow.update_Control_List_View();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            target.parameter.disable_range_limit = (cb.IsChecked == false) ? false : true;
            mainWindow.update_Control_List_View();
        }

        private void amplitude_mode_selector_Selected(object sender, RoutedEventArgs e)
        {
            Amplitude_Mode selected = (Amplitude_Mode)amplitude_mode_selector.SelectedItem;

            target.mode = selected;

            mainWindow.update_Control_List_View();

            grid_hider(target.mode, content);
        }

        private Grid get_Grid(int i)
        {
            switch (i) {
                case 0: return start_freq_grid;
                case 1: return start_amp_grid;
                case 2: return end_freq_grid;
                case 3: return end_amp_grid;
                case 4: return cut_off_amp_grid;
                case 5: return max_amp_grid;
                case 6: return polynomial_grid;
                case 7: return curve_change_grid;
                default: return disable_range_grid;
            
            }
        }
        private void grid_hider(Amplitude_Mode mode , Control_Amplitude_Content cac)
        {
            Boolean[] condition_1, condition_2;

            if (mode == Amplitude_Mode.Linear)
                condition_1 = new Boolean[9] { true, true, true, true, true, true, false, false, true };
            else if(mode == Amplitude_Mode.Wide_3_Pulse)
                condition_1 = new Boolean[9] { true, true, true, true, true, true, false, false, true };
            else if(mode == Amplitude_Mode.Inv_Proportional)
                condition_1 = new Boolean[9] { true, true, true, true, true, true, false, true, true };
            else if(mode == Amplitude_Mode.Exponential)
                condition_1 = new Boolean[9] { false, false, true, true, true, true, false, false, true };
            else
                condition_1 = new Boolean[9] { false, false, true, true, true, true, true, false, true };

            if (cac == Control_Amplitude_Content.Default)
                condition_2 = new Boolean[9] { true, true, true, true, true, true, true, true, true };
            else if(cac == Control_Amplitude_Content.Free_Run_On)
                condition_2 = new Boolean[9] { true, true, false, false, true, true, true, true, true };
            else
                condition_2 = new Boolean[9] { true, true, false, false, true, true, true, true, true };

            for(int i =0; i < 9; i++)
            {
                Grid grid = get_Grid(i);
                if (condition_1[i] && condition_2[i]) grid.IsEnabled = true;
                else grid.IsEnabled = false;

            }
        }
    }



    public enum Control_Amplitude_Content
    {
        Default,Free_Run_On,Free_Run_Off
    }
}

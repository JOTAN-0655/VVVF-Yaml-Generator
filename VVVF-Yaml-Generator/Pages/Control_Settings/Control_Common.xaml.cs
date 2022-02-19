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
using static VVVF_Data_Generator.Yaml_Sound_Data.Yaml_Control_Data;

namespace VVVF_Yaml_Generator.Pages.Control_Settings
{
    /// <summary>
    /// Control_Common.xaml の相互作用ロジック
    /// </summary>
    public partial class Control_Common : Page
    {
        private Yaml_Control_Data target;
        private MainWindow MainWindow;
        public Control_Common(Yaml_Control_Data ycd, MainWindow mainWindow)
        {
            InitializeComponent();

            target = ycd;
            MainWindow = mainWindow;

            Control_Basic.Navigate(new Control_Basic(ycd, mainWindow));
            Control_When_FreeRun.Navigate(new Control_When_FreeRun(ycd, mainWindow));
        }

        
        
    }
}

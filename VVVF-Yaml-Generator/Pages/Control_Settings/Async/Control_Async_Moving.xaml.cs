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

namespace VVVF_Yaml_Generator.Pages.Control_Settings.Async
{
    /// <summary>
    /// Control_Async_Moving.xaml の相互作用ロジック
    /// </summary>
    public partial class Control_Async_Moving : UserControl
    {
        public Control_Async_Moving(Yaml_Control_Data data, MainWindow mainWindow)
        {
            InitializeComponent();
        }
    }
}

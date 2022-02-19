﻿using System;
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

            Control_Basic.Navigate(new Control_Basic(ycd, mainWindow, this));
            Control_When_FreeRun.Navigate(new Control_When_FreeRun(ycd, mainWindow));

            Control_Amplitude_Default.Navigate(new Control_Amplitude(ycd.amplitude_control.default_data,Control_Amplitude_Content.Default, mainWindow));
            
            if(ycd.amplitude_control.free_run_data != null)
            {
                Control_Amplitude_FreeRun_On.Navigate(new Control_Amplitude(ycd.amplitude_control.free_run_data.mascon_on, Control_Amplitude_Content.Free_Run_On, mainWindow));
                Control_Amplitude_FreeRun_Off.Navigate(new Control_Amplitude(ycd.amplitude_control.free_run_data.mascon_off, Control_Amplitude_Content.Free_Run_Off, mainWindow));
            }

            if(target.pulse_Mode == Pulse_Mode.Async || target.pulse_Mode == Pulse_Mode.Async_THI)
            {
                Control_Async.Navigate(new Control_Async(ycd, mainWindow));
            }

        }

        
        
    }
}

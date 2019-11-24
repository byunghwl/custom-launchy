﻿using CustomLaunchy.ViewModel;
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

namespace CustomLaunchy.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            Globals.MainView = this;

            DataContext = new MainViewModel();
        }

        public void FocusToInputBox()
        {
            Inputbox.Focus();
        }

        private void TextBox_KeyEnterUpdate( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Enter )
            {
                TextBox tBox = ( TextBox )sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression( tBox, prop );
                if ( binding != null ) { binding.UpdateSource(); }
            }
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {
            SettingWindow.Open();
        }
    }
}

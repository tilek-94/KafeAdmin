﻿#pragma checksum "..\..\..\..\Windows\GlavWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3B6A80F8412F788688422779E297574422887E47"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AdminKafe.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AdminKafe.Windows {
    
    
    /// <summary>
    /// GlavWindow
    /// </summary>
    public partial class GlavWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\Windows\GlavWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton MenuIcon;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Windows\GlavWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MenuGrid;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Windows\GlavWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Fitnestext;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\Windows\GlavWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame GlavGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AdminKafe;V1.0.0.0;component/windows/glavwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\GlavWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 24 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MenuIcon = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 32 "..\..\..\..\Windows\GlavWindow.xaml"
            this.MenuIcon.Click += new System.Windows.RoutedEventHandler(this.ToggleButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 39 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 42 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 45 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MenuGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.Fitnestext = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            
            #line 70 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 76 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonUp_1);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 82 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonUp_2);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 88 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonUp_3);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 94 "..\..\..\..\Windows\GlavWindow.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_PreviewMouseLeftButtonUp_4);
            
            #line default
            #line hidden
            return;
            case 13:
            this.GlavGrid = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\Views\MeasurementGraphView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5BB7959206B00B713B8A7430694EB9DE042C195EEA01BFE887C385D7F00BE5C9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NetworkService.Helpers;
using NetworkService.ViewModel;
using NetworkService.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace NetworkService.Views {
    
    
    /// <summary>
    /// MeasurementGraphView
    /// </summary>
    public partial class MeasurementGraphView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EntitesComboBox;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBlockNameListView;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBlockIdListView;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel graphStackPanel;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line xAxis;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\Views\MeasurementGraphView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line yAxis;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NetworkService;component/views/measurementgraphview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\MeasurementGraphView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.EntitesComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.txtBlockNameListView = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtBlockIdListView = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.graphStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.xAxis = ((System.Windows.Shapes.Line)(target));
            return;
            case 6:
            this.yAxis = ((System.Windows.Shapes.Line)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


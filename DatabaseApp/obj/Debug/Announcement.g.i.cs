﻿#pragma checksum "..\..\Announcement.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8A493C17910E6A8D9ECBE9A872D03F1E4F126B9E25E1C410C44F549E1ECB6AC2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DatabaseApp;
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


namespace DatabaseApp {
    
    
    /// <summary>
    /// Announcement
    /// </summary>
    public partial class Announcement : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDisplay;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDetails;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTarget;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCheck;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtCheck;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCategory;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem C1;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbRequestType;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPost;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\Announcement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
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
            System.Uri resourceLocater = new System.Uri("/DatabaseApp;component/announcement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Announcement.xaml"
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
            
            #line 8 "..\..\Announcement.xaml"
            ((DatabaseApp.Announcement)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtDisplay = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtDetails = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cbTarget = ((System.Windows.Controls.ComboBox)(target));
            
            #line 51 "..\..\Announcement.xaml"
            this.cbTarget.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbTarget_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnCheck = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\Announcement.xaml"
            this.btnCheck.Click += new System.Windows.RoutedEventHandler(this.btnCheck_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtCheck = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.cbCategory = ((System.Windows.Controls.ComboBox)(target));
            
            #line 74 "..\..\Announcement.xaml"
            this.cbCategory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCategory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.C1 = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 10:
            this.cbRequestType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\Announcement.xaml"
            this.cbRequestType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbRequestType_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\Announcement.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.btnClear_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnPost = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\Announcement.xaml"
            this.btnPost.Click += new System.Windows.RoutedEventHandler(this.btnPost_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\Announcement.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnBack_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


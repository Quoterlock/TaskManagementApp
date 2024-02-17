﻿#pragma checksum "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EAD50D9819FE99436B81E1DE80B11CDB0C4AEE42"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TasksApp.UI.Windows;


namespace TasksApp.UI.Windows {
    
    
    /// <summary>
    /// TaskDetailsWindow
    /// </summary>
    public partial class TaskDetailsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox projectsComboBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button markDoneBtn;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox taskTextBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox dueToTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox timeBlockedCheckBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox startTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox endTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBtn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button deleteBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TasksApp.UI;V1.0.0.0;component/windows/task/taskdetailswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.projectsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.markDoneBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.markDoneBtn.Click += new System.Windows.RoutedEventHandler(this.markDoneBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.taskTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.taskTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dueToTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.dueToTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.timeBlockedCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 38 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.timeBlockedCheckBox.Checked += new System.Windows.RoutedEventHandler(this.timeBlockedCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.timeBlockedCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.timeBlockedCheckBox_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.startTimeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.startTimeTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.endTimeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.endTimeTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.saveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.saveBtn.Click += new System.Windows.RoutedEventHandler(this.saveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.deleteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\..\..\Windows\Task\TaskDetailsWindow.xaml"
            this.deleteBtn.Click += new System.Windows.RoutedEventHandler(this.deleteBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


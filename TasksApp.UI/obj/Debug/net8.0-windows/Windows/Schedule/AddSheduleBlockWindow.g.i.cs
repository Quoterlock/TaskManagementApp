// Updated by XamlIntelliSenseFileGenerator 15/02/2024 20:58:01
#pragma checksum "..\..\..\..\..\Windows\Schedule\AddSheduleBlockWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "44595441FC2C1A61B38DD23EC6C152719B9ADCE0"
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
using TasksApp.UI.Windows.Schedule;


namespace TasksApp.UI.Windows.Schedule
{


    /// <summary>
    /// AddSheduleBlockWindow
    /// </summary>
    public partial class AddSheduleBlockWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TasksApp.UI;V1.0.0.0;component/windows/schedule/addsheduleblockwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\..\Windows\Schedule\AddSheduleBlockWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox titleTextBox;
        internal System.Windows.Controls.CheckBox moCheckBox;
        internal System.Windows.Controls.CheckBox tuCheckBox;
        internal System.Windows.Controls.CheckBox weCheckBox;
        internal System.Windows.Controls.CheckBox thCheckBox;
        internal System.Windows.Controls.CheckBox frCheckBox;
        internal System.Windows.Controls.CheckBox saCheckBox;
        internal System.Windows.Controls.CheckBox suCheckBox;
        internal System.Windows.Controls.TextBox startTimeTextBox;
        internal System.Windows.Controls.TextBox endTimeTextBox;
        internal System.Windows.Controls.Button addBtn;
        internal System.Windows.Controls.Button cancelBtn;
    }
}


﻿#pragma checksum "C:\Users\Brayden\source\repos\COP4870_Project\UWP.LearningManagement\Pages\DetailedStudent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "41DD8FC6E6B93E86EBE0896567A8480CE7D35A6345459A91AF99CDC27A06CC73"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWP.LearningManagement
{
    partial class DetailedStudent : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\DetailedStudent.xaml line 52
                {
                    this.AssignmentListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.AssignmentListBox).DoubleTapped += this.Assign_DoubleTapped;
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.AssignmentListBox).SelectionChanged += this.Assign_SelectionChanged;
                }
                break;
            case 4: // Pages\DetailedStudent.xaml line 29
                {
                    this.CoursesListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


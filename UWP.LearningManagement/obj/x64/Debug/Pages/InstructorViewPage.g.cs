﻿#pragma checksum "C:\Users\Brayden\source\repos\COP4870_Project\UWP.LearningManagement\Pages\InstructorViewPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C7041A10898413C7150662B97E0C2497ED91CE8C890A21F94B72539A94650C67"
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
    partial class InstructorViewPage : 
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
            case 2: // Pages\InstructorViewPage.xaml line 17
                {
                    this.frame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 3: // Pages\InstructorViewPage.xaml line 49
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Back_Click;
                }
                break;
            case 4: // Pages\InstructorViewPage.xaml line 50
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.AddStudent_Click;
                }
                break;
            case 5: // Pages\InstructorViewPage.xaml line 51
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.AddAdmin_Click;
                }
                break;
            case 6: // Pages\InstructorViewPage.xaml line 52
                {
                    global::Windows.UI.Xaml.Controls.Button element6 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element6).Click += this.EditPerson_Click;
                }
                break;
            case 7: // Pages\InstructorViewPage.xaml line 53
                {
                    global::Windows.UI.Xaml.Controls.Button element7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element7).Click += this.Delete_Click;
                }
                break;
            case 8: // Pages\InstructorViewPage.xaml line 28
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.Search_Click;
                }
                break;
            case 9: // Pages\InstructorViewPage.xaml line 30
                {
                    global::Windows.UI.Xaml.Controls.ListBox element9 = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)element9).DoubleTapped += this.ListBox_DoubleTapped;
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


﻿#pragma checksum "C:\Users\Brayden\source\repos\COP4870_Project\UWP.LearningManagement\Pages\ItemsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EBF68FE7000901D9C1FFCFF428F0BE0D7E560E3538C4B56C924AAF431527DE91"
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
    partial class ModulesPage : 
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
            case 2: // Pages\ItemsPage.xaml line 26
                {
                    global::Windows.UI.Xaml.Controls.ListBox element2 = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)element2).DoubleTapped += this.ListBox_DoubleTapped;
                }
                break;
            case 3: // Pages\ItemsPage.xaml line 43
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.Add_Click;
                }
                break;
            case 4: // Pages\ItemsPage.xaml line 44
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.Edit_Click;
                }
                break;
            case 5: // Pages\ItemsPage.xaml line 45
                {
                    global::Windows.UI.Xaml.Controls.Button element5 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element5).Click += this.Delete_Click;
                }
                break;
            case 7: // Pages\ItemsPage.xaml line 23
                {
                    global::Windows.UI.Xaml.Controls.Button element7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element7).Click += this.Search_Click;
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


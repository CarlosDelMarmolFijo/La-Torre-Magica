﻿#pragma checksum "..\..\BuscarCliente.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "679276328A864105AE6E1002DB3B7CBF5474056B695BB8D6D220CC43ABFBE39F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace La_Torre_Mágica {
    
    
    /// <summary>
    /// BuscarCliente
    /// </summary>
    public partial class BuscarCliente : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\BuscarCliente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxNombreCliente;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\BuscarCliente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAceptar;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\BuscarCliente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCancelar;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\BuscarCliente.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxInformacionCliente;
        
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
            System.Uri resourceLocater = new System.Uri("/La Torre Mágica;component/buscarcliente.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BuscarCliente.xaml"
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
            this.TextBoxNombreCliente = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.ButtonAceptar = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\BuscarCliente.xaml"
            this.ButtonAceptar.Click += new System.Windows.RoutedEventHandler(this.ButtonAceptar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\BuscarCliente.xaml"
            this.ButtonCancelar.Click += new System.Windows.RoutedEventHandler(this.ButtonCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBoxInformacionCliente = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "C:\Users\Sigurd\Desktop\wp7chess\wp7chess\RootControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2EDA60DCF27968071A7AFF1EB8851D01"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SilverlightChess;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace wp7chess {
    
    
    public partial class RootControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.UserControl UserControl;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal SilverlightChess.PlayerSelector blackSelector;
        
        internal SilverlightChess.ChessBoard chessBoard;
        
        internal SilverlightChess.PlayerSelector whiteSelector;
        
        internal System.Windows.Controls.TextBlock whiteNodesTitle;
        
        internal System.Windows.Controls.TextBlock blackNodesTitle;
        
        internal System.Windows.Controls.TextBlock blackNodesSec;
        
        internal System.Windows.Controls.TextBlock whiteNodesSec;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/wp7chess;component/RootControl.xaml", System.UriKind.Relative));
            this.UserControl = ((System.Windows.Controls.UserControl)(this.FindName("UserControl")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.blackSelector = ((SilverlightChess.PlayerSelector)(this.FindName("blackSelector")));
            this.chessBoard = ((SilverlightChess.ChessBoard)(this.FindName("chessBoard")));
            this.whiteSelector = ((SilverlightChess.PlayerSelector)(this.FindName("whiteSelector")));
            this.whiteNodesTitle = ((System.Windows.Controls.TextBlock)(this.FindName("whiteNodesTitle")));
            this.blackNodesTitle = ((System.Windows.Controls.TextBlock)(this.FindName("blackNodesTitle")));
            this.blackNodesSec = ((System.Windows.Controls.TextBlock)(this.FindName("blackNodesSec")));
            this.whiteNodesSec = ((System.Windows.Controls.TextBlock)(this.FindName("whiteNodesSec")));
        }
    }
}


using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BrewingApp.Models;

namespace BrewingApp.Other
{
    public class Messages
    {

    }
 
    public class NavigateMessage
    {
        public NavigateMessage(string pageName, string sender)
        {
            PageName = pageName;
            Sender = sender;
        }

        public string PageName { get; set; }
        public string Sender { get; set; }
    }
    
}

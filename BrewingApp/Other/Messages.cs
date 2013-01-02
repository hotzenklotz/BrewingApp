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
using BrewBuddy.Models;
using System.Text.RegularExpressions;

namespace BrewBuddy.Other
{
 
    public class NavigateMessage
    {
        public NavigateMessage(string pageName)
        {
            PageName = pageName;
        }

        public string PageName { get; set; }
    }

    public class UpdateViewMessage
    {
        public UpdateViewMessage(string viewName)
        {
            ViewName = viewName;
        }

        public string ViewName { get; set; }
    }

        
    
}

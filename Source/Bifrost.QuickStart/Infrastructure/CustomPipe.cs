using Bifrost.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bifrost.QuickStart.Infrastructure
{
    public class CustomPipe : IPipe
    {
        public void Before(IWebContext webContext)
        {
            foreach(string parameter in webContext.Request.From.Keys)
            {               
                var parameterValue = webContext.Request.From[parameter];
                webContext.Request.From[parameter] = parameterValue.Replace("Foo", "Bar");
            }
        }

        public void After(IWebContext webContext)
        {
        }
    }
}
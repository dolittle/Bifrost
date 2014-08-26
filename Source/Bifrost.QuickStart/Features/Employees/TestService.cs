using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bifrost.Concepts;

namespace Bifrost.QuickStart.Features.Employees
{
    public class GuidConcept : ConceptAs<Guid>
    {
    }

    public class TestService
    {
        public void DoStuff(GuidConcept value)
        {
            var i = 0;
            i++;
        }
    }
}
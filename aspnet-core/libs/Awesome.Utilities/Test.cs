using System;
using System.Collections.Generic;
using System.Text;

namespace Awesome
{
    public class Test
    {
        public string FirstName;
        public string LastName;

        public static void Run()
        {
            var x = new Dictionary<string, Test>().WrapForIndexing();

            var firstNameIndex1 = x.Index<string>("FirstName").SetAsHashOf(item => item.Value.FirstName);
            var firstNameIndex2 = x.HashIndexOf("FirstName", item => item.Value.FirstName);
        }
    }
}

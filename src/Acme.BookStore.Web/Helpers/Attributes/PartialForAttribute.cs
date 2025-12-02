using System;

namespace Acme.BookStore.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PartialForAttribute : Attribute
    {
        public string[] PageNames { get; }
        public string ViewComponentName { get; }
        public PartialForAttribute(string[] pageNames, string viewComponentName)
        {
            PageNames = pageNames;
            ViewComponentName = viewComponentName;
        }
    }


}

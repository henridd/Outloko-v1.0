using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outloko.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewAttribute : Attribute
    {
        public string Region { get; set; }
        public Type Type { get; set; }

        public DependentViewAttribute(string region, Type type)
        {
            if (region is null)
                throw new ArgumentNullException(nameof(region));

            if (type is null)
                throw new ArgumentNullException(nameof(type));

            Region = region;
            Type = type;
        }
    }
}

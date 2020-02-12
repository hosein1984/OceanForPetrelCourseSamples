using System.ComponentModel;
using System.Text;

namespace OceanCoursePlugin.Extensions
{
    public static class ObjectExtensions
    {
        public static string DumpToString<T>(this T source)
        {
            if (source == null)
            {
                return "null";
            }
            //
            StringBuilder builder = new StringBuilder();
            //
            builder.Append(typeof(T).Name + ": ");
            //
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(source))
            {
                string propertyName = descriptor.Name;
                object propertyValue = descriptor.GetValue(source);
                //
                builder.Append($"'{propertyName}' = '{propertyValue}'; ");
            }
            //
            return builder.ToString();
        }        
    }
}

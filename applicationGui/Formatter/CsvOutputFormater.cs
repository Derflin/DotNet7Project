using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace applicationGui.Formatter
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(MyContentTypes.CSV));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            StringBuilder csv = new StringBuilder();
            Type type = GetTypeOf(context.Object);

            csv.AppendLine(
                string.Join<string>(
                    ",", type.GetProperties().Select(x => x.Name)
                )
            );

            IEnumerable<object> objects = (IEnumerable<object>) context.Object;

            if (objects == null) throw new ArgumentNullException(nameof(context));

            foreach (object _object in objects)
            {
                var valueList = _object.GetType().GetProperties().Select(
                    propertyInfo => new
                    {
                        Value = propertyInfo.GetValue(_object, null)
                    }
                );

                List<string> values = new List<string>();

                foreach (dynamic value in valueList)
                {
                    if (value.Value != null)
                    {
                        var tmpValue = value.Value.ToString();

                        if (tmpValue.Contains(","))
                            tmpValue = string.Concat("\"", tmpValue, "\"");

                        tmpValue = tmpValue.Replace("\r", " ", StringComparison.InvariantCultureIgnoreCase);
                        tmpValue = tmpValue.Replace("\n", " ", StringComparison.InvariantCultureIgnoreCase);

                        values.Add(tmpValue);
                    }
                    else
                    {
                        values.Add(string.Empty);
                    }
                }

                csv.AppendLine(string.Join(",", values));
            }

            return context.HttpContext.Response.WriteAsync(csv.ToString(), selectedEncoding);
        }

        private static Type GetTypeOf(object obj)
        {
            Type type = obj.GetType();

            Type itemType = type.GetGenericArguments().Length > 0
                ? type.GetGenericArguments()[0]
                : type.GetElementType();

            return itemType;
        }
    }
}
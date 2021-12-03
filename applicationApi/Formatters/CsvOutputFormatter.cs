using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using applicationApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace applicationApi.Formatters
{
    public class CsvOutputFormatter : OutputFormatter
    {
        private const string CsvDelimiter = ",";
        
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(Sensor).IsAssignableFrom(type) || typeof(IEnumerable<Sensor>).IsAssignableFrom(type);
        }
        
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            Type type = context.Object.GetType();
            
            Type itemType;
            if (type.GetGenericArguments().Length > 0)
                itemType = type.GetGenericArguments()[0];
            else
                itemType = type.GetElementType();

            var headers = itemType.GetProperties()
                .Select(x => x.GetCustomAttribute<DisplayAttribute>(false)?.Name ?? x.Name);

            var buffer = CreateCsv((IEnumerable<object>) context.Object, headers);
            var response = context.HttpContext.Response;

            await response.WriteAsync(buffer.ToString());
        }

        public override void WriteResponseHeaders(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            var saveAsFileName = "sensors.csv";
            response.ContentType = "text/csv";
            response.Headers.Add("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
        }

        //function which returns StringBuilder buffer with csv file content
        private static StringBuilder CreateCsv(IEnumerable<object> data, IEnumerable<object> headers)
        {
            var buffer = new StringBuilder();
            
            //create headers
            buffer.AppendLine(string.Join(CsvDelimiter, headers.Select(x => x)));
            
            //for each data item create new row
            foreach (var obj in data)
            {
                //get values from item
                var vals = obj.GetType().GetProperties().Select(
                    pi => new
                    {
                        Value = pi.GetValue(obj, null)
                    });

                //add each value from item to row
                var row = string.Empty;
                foreach (var val in vals)
                {
                    if (val.Value != null)
                    {
                        var _val = val.Value.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (_val.Contains(","))
                            _val = string.Concat("\"", _val, "\"");

                        //Replace any \r or \n special characters from a new line with a space
                        if (_val.Contains("\r"))
                            _val = _val.Replace("\r", " ");
                        if (_val.Contains("\n"))
                            _val = _val.Replace("\n", " ");

                        row = string.Concat(row, _val, CsvDelimiter);
                    }
                    else
                    {
                        row = string.Concat(row, string.Empty, CsvDelimiter);
                    }
                }
                //create item row
                buffer.AppendLine(row.TrimEnd(CsvDelimiter.ToCharArray()));
            }

            return buffer;
        }
    }
}
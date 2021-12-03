using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace applicationApi.Models
{
    [DataContract]
    public class PaginatedListSensor<T>
    {
        [DataMember]
        public int PageIndex { get; set; }
        
        [DataMember]
        public int TotalPages { get; set; }
        
        [DataMember]
        public int PageSize { get; set; }
        
        [DataMember]
        public int TotalItems { get; set; }
        
        [DataMember]
        public List<T> Items { get; set; }

        public PaginatedListSensor(List<T> items, int count, int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            if (pageSize > 0)
            {
                this.PageSize = pageSize;
                this.TotalPages = (int) Math.Ceiling(count / (double) pageSize);
                this.Items = items.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            else
            {
                this.PageSize = 0;
                this.TotalPages = 1;
                this.Items = pageIndex == 0 ? items : new List<T>();
            }
            this.TotalItems = count;
        }
    }
}
using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models {
    public class DeleteEditButtonViewModel {
        public string Name { get; set; }
        public string DeleteMethod { get; set; }
        public string EditUrl { get; set; }
        public bool HasEdit { get; set; }
        public bool HasDelete { get; set; }
        public string GridId { get; set; }
    }

    public interface IGirdNavViewModel<out T> where T : IEntity {
        string Title { get; set; }
        string CreateUrl { get; set; }
        bool IsPartial { get; set; }
        string GridName { get; set; }
        int PageSize { get; set; }
        IQueryable<T> List { get; }
    }

    public class GirdNavViewModel<T> : IGirdNavViewModel<T> where T : IEntity {
        public string Title { get; set; }
        public string CreateUrl { get; set; }
        public bool IsPartial { get; set; }
        public string GridName { get; set; }
        public int PageSize { get; set; }
        public IQueryable<T> List { get; set; }

        public GirdNavViewModel() {
            var pageSize = HttpContext.Current.Request.QueryString.Get("pageSize");
            if (!string.IsNullOrEmpty(pageSize)) {
                int num = 10;
                int.TryParse(pageSize, out num);
                PageSize = num;
            }
            else {
                PageSize = 10;
            }
        }

    }

}
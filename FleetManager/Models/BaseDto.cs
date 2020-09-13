using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetManager.Models
{
    public abstract class BaseDto
    {
        public virtual int? Id { get; set; }

        public virtual ApiLink Link { get; set; }

        public class ApiLink
        {
            public string Href { get; set; }
            public string Rel { get; set; }

            public ApiLink(string formatLink, int id, string rel = "self")
            {
                Href = string.Format(formatLink, id);
                Rel = rel;
            }
        }
    }
}
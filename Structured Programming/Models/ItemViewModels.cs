using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Structured_Programming.Models
{
    public class ItemIndexModel
    {
        public string TypeName { get; set; }
        public IEnumerable<Structured_Programming.Models.Item> Items { get; set; }
    }
    public class ItemFormModel
    {
        public SelectList TypeList { get; set; }
        public Item Item { get; set; }
            
        [FileTypes("jpg,png,jpeg")]
        [FileSize(10240000)]
        public HttpPostedFileBase Image { get; set; }
    }
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Invalid file type. Only the following types {0} are supported.", String.Join(", ", _types));
        }
    }
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
 
        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }
 
        public override bool IsValid(object value)
        {
            if (value == null) return true;
 
            return _maxSize > (value as HttpPostedFileBase).ContentLength;
        }
 
        public override string FormatErrorMessage(string name)
        {
            return string.Format("The file size should not exceed {0}", _maxSize);
        }
    }
}
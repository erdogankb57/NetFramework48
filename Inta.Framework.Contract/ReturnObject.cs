using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Contract
{
    public class ReturnObject<T>
    {
        public ReturnObject()
        {

        }
        public T Data { get; set; }
        public string ResultMessage { get; set; }
        public MessageType ResultType { get; set; }
        public string ErrorMessage { get; set; }
        public object Validation { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class ReturnErrorObject<T> : ReturnObject<T>
    {
        public ReturnErrorObject()
        {

        }
        public Exception Exception { get; set; }
    }
    public enum MessageType
    {
        Success = 0,
        Error = 1,
        Warning = 2
    }

    public class ReturnErrorObject : ReturnObject<string>
    {
        public Exception Exception { get; set; }
    }

    public class ReturnBaseDataObject : ReturnObject<string>
    {
        public string Extension { get; set; }
        public string FileName { get; set; }
        public string FileBase64Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentType { get; set; }
    }

    public class ReturnPaginationObject<T> : ReturnObject<T>
    {
        public int DataCount { get; set; }
        public int PageCount { get; set; }
    }
}

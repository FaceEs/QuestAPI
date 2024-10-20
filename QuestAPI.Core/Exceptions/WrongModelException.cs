using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Exceptions
{
    public class WrongModelException : Exception
    {
        public WrongModelException(string message) : base(message) { }
        public WrongModelException(string message, Exception innerException) : base(message, innerException) { }
    }
}

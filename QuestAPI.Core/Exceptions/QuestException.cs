using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Exceptions
{
    public class QuestException : Exception
    {
        public QuestException(string message) : base(message) { }
        public QuestException(string message, Exception innerException) : base(message,innerException) { }
    }
}

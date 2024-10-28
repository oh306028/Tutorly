using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Domain.ModelsExceptions
{
    public class OutOfSpaceException : Exception
    {
        public OutOfSpaceException(string message) : base(message)
        {
            
        }
    }
}

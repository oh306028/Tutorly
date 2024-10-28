using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;

namespace Tutorly.Application.Handlers
{
    public interface IHandler<T> where T :  Command
    {
        void Handle(T command);

    }
}

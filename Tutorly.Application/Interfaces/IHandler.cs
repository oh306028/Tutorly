using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;



namespace Tutorly.Application.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
       Task Handle(T command);

    }
}

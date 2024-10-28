using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tutorly.Application.Commands
{
    public record ApplyForPostCommand(int PostId, int StudentId) : Command
    {
    }
}
    
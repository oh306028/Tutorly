using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.Params
{
    public class AcceptStudentParams
    {
        public bool isAccepted { get; set; }
        public int StudentId { get; set; }  
    }
}

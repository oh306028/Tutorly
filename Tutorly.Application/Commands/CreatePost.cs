using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using ICommand = Tutorly.Application.Interfaces.ICommand;

namespace Tutorly.Application.Commands
{
    public class CreatePost : ICommand
    {
        public CreatePost(
            int categoryId,
            int tutorId,
            int maxStudentAmount,
            DayOfWeek happensOn,
            TimeSpan happensAt,
            bool isRemotely,
            bool isAtStudentPlace,
            Grade studentsGrade,
            string? description = null,
            int? addressId = null
        )
        {
            if (maxStudentAmount <= 0)
                throw new ArgumentException("Max student amount must be greater than zero");

            if (happensAt < TimeSpan.Zero || happensAt >= TimeSpan.FromDays(1))
                throw new ArgumentException("HappensAt time must be within a 24-hour period");

            Id = Guid.NewGuid();
            CategoryId = categoryId;
            TutorId = tutorId;
            MaxStudentAmount = maxStudentAmount;
            HappensOn = happensOn;
            HappensAt = happensAt;
            IsRemotely = isRemotely;
            IsAtStudentPlace = isAtStudentPlace;
            Description = description;
            AddressId = addressId;
            StudentsGrade = studentsGrade;
        }

        public Guid Id { get; }
        public int CategoryId { get; }
        public int TutorId { get; }
        public int MaxStudentAmount { get; }
        public DayOfWeek HappensOn { get; }
        public TimeSpan HappensAt { get; }
        public bool IsRemotely { get; }
        public bool IsAtStudentPlace { get; }
        public string? Description { get; }
        public int? AddressId { get; }
        public Grade StudentsGrade { get; }
    }
}


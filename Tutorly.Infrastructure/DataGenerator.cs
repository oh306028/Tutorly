using Bogus;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;

public class DataGenerator
{
    private readonly TutorlyDbContext _dbContext;

    public DataGenerator(TutorlyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        // Tutor Generator
        var tutorGenerator = new Faker<Tutor>()
            .RuleFor(f => f.FirstName, a => a.Name.FirstName())
            .RuleFor(f => f.LastName, a => a.Name.LastName())
            .RuleFor(f => f.Email, a => a.Person.Email)
            .RuleFor(f => f.PasswordHash, a => a.Lorem.Word())
            .RuleFor(f => f.Role, Role.Tutor)
            .RuleFor(f => f.Experience, a => a.Random.Byte(1, 10)); 

        // Category Generator
        var categoryGenerator = new Faker<Category>()
            .RuleFor(c => c.Name, a => a.PickRandom(new[] { "Maths", "Science", "English", "History" }));

        // Address Generator
        var addressGenerator = new Faker<Address>()
             .RuleFor(a => a.Street, "Street")
             .RuleFor(a => a.City, "City")
             .RuleFor(a => a.Number, "43");

        // Post Generator
        var postGenerator = new Faker<Post>()
            .RuleFor(p => p.Tutor, tutorGenerator)
            .RuleFor(p => p.MaxStudentAmount, a => a.Random.Number(5, 20))
            .RuleFor(p => p.Description, a => a.Lorem.Sentence())
            .RuleFor(p => p.Category, categoryGenerator)
            .RuleFor(p => p.HappensOn, a => a.PickRandom<DayOfWeek>())
            .RuleFor(p => p.HappensAt, TimeSpan.FromMinutes(120))
            .RuleFor(p => p.IsRemotely, a => a.Random.Bool())
            .RuleFor(p => p.IsAtStudentPlace, a => a.Random.Bool())
            .RuleFor(p => p.IsHappeningAtStudentPlace, a => a.Random.Bool())
            .RuleFor(p => p.AddressId, a => a.Random.Int())
            .RuleFor(p => p.StudentsGrade, a => a.PickRandom(new[] { Grade.College, Grade.Primary, Grade.Secondary, Grade.HighSchool }))
            .RuleFor(p => p.Tutor, tutorGenerator.Generate())
            .RuleFor(p => p.Category, categoryGenerator.Generate())
            .RuleFor(p => p.Address, addressGenerator.Generate());

        
        var posts = postGenerator.Generate(3);

     
        _dbContext.AddRange(posts);
        _dbContext.SaveChanges();
    }
}

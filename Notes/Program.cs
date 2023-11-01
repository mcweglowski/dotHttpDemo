using Notes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Note> notes = new List<Note>
{
    new Note { NoteId = 1, Title = "Meeting notes", Description = "Discuss project roadmap", CreationDateTime = DateTime.Now.AddDays(-9), DueDateTime = DateTime.Now.AddDays(-1) },
    new Note { NoteId = 2, Title = "Grocery list", Description = "Milk, Bread, Eggs", CreationDateTime = DateTime.Now.AddDays(-8), DueDateTime = DateTime.Now.AddDays(-2) },
    new Note { NoteId = 3, Title = "Book to read", Description = "The Great Gatsby", CreationDateTime = DateTime.Now.AddDays(-7), DueDateTime = DateTime.Now.AddDays(-3) },
    new Note { NoteId = 4, Title = "Movies to watch", Description = "Inception, Interstellar", CreationDateTime = DateTime.Now.AddDays(-6), DueDateTime = DateTime.Now.AddDays(-4) },
    new Note { NoteId = 5, Title = "Gift ideas", Description = "Watch, Perfume", CreationDateTime = DateTime.Now.AddDays(-5), DueDateTime = DateTime.Now.AddDays(-5) },
    new Note { NoteId = 6, Title = "Holiday plan", Description = "Visit Paris", CreationDateTime = DateTime.Now.AddDays(-4), DueDateTime = DateTime.Now.AddDays(-6) },
    new Note { NoteId = 7, Title = "Learning goals", Description = "Learn C#", CreationDateTime = DateTime.Now.AddDays(-3), DueDateTime = DateTime.Now.AddDays(-7) },
    new Note { NoteId = 8, Title = "Workout routine", Description = "Gym at 7 AM", CreationDateTime = DateTime.Now.AddDays(-2), DueDateTime = DateTime.Now.AddDays(-8) },
    new Note { NoteId = 9, Title = "Dinner recipe", Description = "Try new Italian pasta recipe", CreationDateTime = DateTime.Now, DueDateTime = DateTime.Now.AddDays(-9) },
    new Note { NoteId = 10, Title = "Car service", Description = "Annual maintenance", CreationDateTime = DateTime.Now.AddDays(-1), DueDateTime = DateTime.Now.AddDays(-10) }
};

app.MapGet("/notes", () => notes)
.WithName("GetNotes")
.WithOpenApi();

app.MapGet("/notes/{noteId}", (int noteId) =>
{
    var note = notes.SingleOrDefault(x => x.NoteId == noteId);

    return note;
})
.WithName("GetNote")
.WithOpenApi();

app.MapDelete("/notes/{noteId}", (int noteId) =>
{
    var note = notes.SingleOrDefault(y => y.NoteId == noteId);
    if (note is not null)
        notes.Remove(note);
})
.WithName("DeleteNote")
.WithOpenApi();

app.MapPost("/notes", (Note note) =>
{
    int noteId = notes.Max(x => x.NoteId);
    note.NoteId = noteId + 1;
    notes.Add(note);
})
.WithName("PostNote")
.WithOpenApi();

app.MapPut("/notes/{noteId}", (int noteId, Note note) =>
{
    var n = notes.SingleOrDefault(y => y.NoteId == noteId);
    n.Title = note.Title;
    n.Description = note.Description;
    n.CreationDateTime = note.CreationDateTime;
    n.DueDateTime = note.DueDateTime;
})
.WithName("PutNote")
.WithOpenApi();

app.Run();
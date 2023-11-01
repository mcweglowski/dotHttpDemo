namespace Notes;

public class Note
{
    public int NoteId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreationDateTime { get; set; }
    public DateTime DueDateTime { get; set; }
}
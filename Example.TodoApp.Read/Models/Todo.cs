using System;
using Concept.Cqrs.Read;

namespace Example.TodoApp.Read.Models
{
  public class Todo : ReadModelBase
  {
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    public DateTime DueDate { get; set; }
    public string Topic { get; set; }
    public string State { get; set; }
    public string Assigne { get; set; }
  }
}

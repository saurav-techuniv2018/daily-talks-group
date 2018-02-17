using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

enum Day
{
  MON,
  TUE,
  WED,
  THU,
  FRI
}

namespace talks
{
  class Intern
  {
    public string Name { get; set; }
    public Day Day { get; set; }
    public int Week { get; set; }

    public Intern(string name, string speakingDay)
    {
      this.Name = name;

      var parts = speakingDay.Trim().Split(" - ").ToList();

      if (Enum.TryParse<Day>(parts.First().Trim().ToUpper(), out Day dayEnum))
      {
        this.Day = dayEnum;
      }
      else
      {
        this.Day = Day.MON;
      }

      this.Week = int.Parse(parts.Last());
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("NOTE: Provide input json file path.");
        return;
      }

      var inputFile = args[0];
      var jsonString = File.ReadAllText(inputFile);

      var interns = JsonConvert.DeserializeObject<List<Intern>>(jsonString);

      var groupedTalks = interns
      .OrderBy(p => p.Week)
      .ThenBy(p => p.Day)
      .Select((p, i) => new { Intern = p, Group = (int)(i / 3) })
      .GroupBy(p => p.Group);

      for (var i = 0; i < groupedTalks.Count(); ++i)
      {
        Console.WriteLine($"\nGROUP {i + 1}");
        groupedTalks.ElementAt(i)
        .ToList()
        .ForEach(p => { Console.WriteLine($"{p.Intern.Name} ({p.Intern.Day} - {p.Intern.Week})"); });
      }
    }
  }
}

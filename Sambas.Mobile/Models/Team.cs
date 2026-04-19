using System.Text.Json.Serialization;

namespace Sambas.Mobile.Models;

public sealed record Team
{
    public static Team Sambas = new(
        Guid.Parse("4ce41864-7a9b-4c40-949b-c8041b1f1f0f"),
        "Sambas",
        [
            new Player("Jakub", "Howarth"),
            new Player("Theo", "Metcalfe"),
            new Player("Henry", "Pantall"),
            new Player("Arlo", "Parkes"),
            new Player("Harrison", "Fisher"),
            new Player("Kristian", "Gorham"),
            new Player("Logan", "Ayre"),
            new Player("Stanley", "Warren")
        ]
    );

    public static Team Empty
        => new(Guid.Empty, "", []);

    public Guid Id { get; init; }
    public string Name { get; init; }
    public IReadOnlyCollection<Player> Squad { get; init; }

    [JsonConstructor]
    public Team(Guid id, string name, IReadOnlyCollection<Player> squad)
    {
        Id = id;
        Name = name;
        Squad = squad;
    }
}

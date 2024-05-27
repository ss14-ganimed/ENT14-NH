using Robust.Shared.Prototypes;

namespace Content.Shared.StationGoal
{
    [Serializable, Prototype("stationGoal")]
    public sealed class StationGoalPrototype : IPrototype
    {
        [IdDataFieldAttribute] public string ID { get; } = default!;

        [DataField("text")] public string Text { get; set; } = string.Empty;
    }
}
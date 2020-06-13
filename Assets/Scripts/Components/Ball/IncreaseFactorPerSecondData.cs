using Unity.Entities;

namespace Components.Ball
{
    [GenerateAuthoringComponent]
    public struct IncreaseFactorPerSecondData : IComponentData
    {
        public float Value;
    }
}
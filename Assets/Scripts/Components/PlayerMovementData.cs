using Unity.Entities;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PlayerMovementData : IComponentData
    {
        public int Direction;
        public float Speed;
    }
}
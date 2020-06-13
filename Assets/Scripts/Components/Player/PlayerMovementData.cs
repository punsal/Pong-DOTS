using Unity.Entities;

namespace Components.Player
{
    [GenerateAuthoringComponent]
    public struct PlayerMovementData : IComponentData
    {
        public int Direction;
        public float Speed;
    }
}
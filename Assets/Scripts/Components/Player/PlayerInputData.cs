using Unity.Entities;
using UnityEngine;

namespace Components.Player
{
    [GenerateAuthoringComponent]
    public struct PlayerInputData : IComponentData
    {
        public KeyCode UpKeyCode;
        public KeyCode DownKeyCode;
    }
}

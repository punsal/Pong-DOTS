using Unity.Entities;
using UnityEngine;

namespace Components
{
    [GenerateAuthoringComponent]
    public struct PlayerInputData : IComponentData
    {
        public KeyCode UpKeyCode;
        public KeyCode DownKeyCode;
    }
}

using Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Rendering;
using UnityEngine;

namespace Systems
{
    [AlwaysSynchronizeSystem]
    public class PlayerInputSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            Entities.ForEach((ref PlayerMovementData playerMovementData, in PlayerInputData playerInputData) =>
            {
                playerMovementData.Direction = 0;

                playerMovementData.Direction += Input.GetKey(playerInputData.UpKeyCode) ? 1 : 0;
                playerMovementData.Direction -= Input.GetKey(playerInputData.DownKeyCode) ? 1 : 0;
            }).Run();

            return default;
        }
    }
}
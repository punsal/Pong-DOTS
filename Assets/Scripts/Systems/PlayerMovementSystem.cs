using Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Systems
{
    [AlwaysSynchronizeSystem]
    public class PlayerMovementSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var deltaTime = Time.DeltaTime; 
            var yBound = GameManager.Instance.yBound;
            
            Entities.ForEach((ref Translation position, in PlayerMovementData playerMovementData) =>
            {
                var positionY = position.Value.y + playerMovementData.Speed * playerMovementData.Direction * deltaTime;
                position.Value.y = math.clamp(positionY, -yBound, yBound);
            }).Run();

            return default;
        }
    }
}
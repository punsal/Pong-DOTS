using Components.Ball;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;

namespace Systems.Ball
{
    [AlwaysSynchronizeSystem]
    public class SpeedIncreasePerSecondSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<BallTag>()
                .ForEach((
                ref PhysicsVelocity physicsVelocity,
                in IncreaseFactorPerSecondData increaseFactorPerSecondData) =>
            {
                var modifier = new float2(increaseFactorPerSecondData.Value * deltaTime);

                var newVelocity = physicsVelocity.Linear.xy;
                newVelocity += math.lerp(-modifier, modifier, math.sign(newVelocity));
                physicsVelocity.Linear.xy = newVelocity;

            }).Run();
            
            return default;
        }
    }
}
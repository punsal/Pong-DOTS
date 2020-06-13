using Components.Ball;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Systems.Ball
{
    [AlwaysSynchronizeSystem]
    public class CheckScoreSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            var gameManager = GameManager.Instance;
            var xBound = gameManager.xBound;
            
            Entities
                .WithAll<BallTag>()
                .WithoutBurst()
                .ForEach((Entity entity, in Translation position) =>
                {
                    var positionX = position.Value.x;

                    if (positionX >= xBound)
                    {
                        gameManager.PlayerScored(0);
                        // ReSharper disable once AccessToDisposedClosure
                        entityCommandBuffer.DestroyEntity(entity);
                        return;
                    }

                    if (!(positionX <= -xBound)) return;
                    gameManager.PlayerScored(1);
                    // ReSharper disable once AccessToDisposedClosure
                    entityCommandBuffer.DestroyEntity(entity);
                })
                .Run();
            
            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();
            
            return default;
        }
    }
}
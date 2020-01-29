using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class BallGoalCheckSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        
        Entities
            .WithAll<BallTag>()
            .ForEach((Entity entity, in Translation trans) =>
            {
                var pos = trans.Value;
                var bound = GameManager.main.xBound;

                if (pos.x >= bound)
                {
                    GameManager.main.PlayerScored(0);
                    ecb.DestroyEntity(entity);
                }
                else if (pos.x <= -bound)
                {
                    GameManager.main.PlayerScored(1);
                    ecb.DestroyEntity(entity);
                }
            }).WithoutBurst().Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();
        
        return default;
    }
}

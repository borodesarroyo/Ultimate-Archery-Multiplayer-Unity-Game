using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PaddleMovementSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var deltaTime = Time.DeltaTime;
        var yBound = GameManager.main.yBound;

        Entities.ForEach((ref Translation trans, in PaddleMovementData moveData) =>
            {
                trans.Value.y = math.clamp(trans.Value.y + (moveData.speed * moveData.direction * deltaTime), -yBound,
                    yBound);
            }).Run();
        
        return default;
    }
}

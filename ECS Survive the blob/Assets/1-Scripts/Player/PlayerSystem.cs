using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PlayerSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Translation trans, in PlayerData data) =>
        {
            int direction = 0;
            direction += Input.GetKey(data.left) ? -1 : 0;
            direction += Input.GetKey(data.right) ? 1 : 0;
            trans.Value = new float3(trans.Value.x + direction * deltaTime * data.speed, trans.Value.y, trans.Value.z);
        }).Run();
        return default;
    }
}

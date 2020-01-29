using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;

[AlwaysSynchronizeSystem]
public class IncreaseVelocityOverTimeSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps) //if multiple balls, run on multiple threads for each balls job losing the attribute, changing Run() to Schedule(inputDeps) and returning that job handle 
    {
        //caching the delta time from the component class, replacing the monobehaviour version
        var deltaTime = Time.DeltaTime;

        //logic of the system
        Entities.ForEach((ref PhysicsVelocity vel, in SpeedIncreaseOverTimeData data) =>
        {
            var modifier = new float2(data.increasePerSecond * deltaTime); //get move time
            var newVel = vel.Linear.xy; //get velocity

            newVel += math.lerp(-modifier, modifier, math.sin(newVel)); //lerp the velocity
            
            vel.Linear.xy = newVel; //set the current velocity to the modified velocity
            
        }).WithoutBurst().Run(); //runs on main thread without burst otherwise error
        
        return default; //return nothing cuz were not trying to schedule jobs on multiple threads for a single object using the system
    }
}

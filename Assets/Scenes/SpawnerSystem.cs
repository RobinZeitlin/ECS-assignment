using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)     {    }

    public void OnDestroy(ref SystemState state)    {   }

    public void OnUpdate(ref SystemState state)
    {
        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if(spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
                float3 pos = new float3(spawner.ValueRO.spawnPosition.x, spawner.ValueRO.spawnPosition.y, spawner.ValueRO.spawnPosition.z);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.spawnPosition));
                spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

                Debug.WriteLine("Spawned new entity");
            }
        }
    }
}

using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Burst;
using UnityEngine;
using Unity.Collections;
using Unity.VisualScripting;

[BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Enemy> enemy in SystemAPI.Query<RefRW<Enemy>>())
        {
            ProcessSpawner(ref state, enemy);
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Enemy> enemy)
    {
        if (enemy.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity newEntity = state.EntityManager.Instantiate(enemy.ValueRO.prefab);
            Vector3 pos = GetRandomPointInCircle(10);

            enemy.ValueRW.spawnPosition = pos;
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(enemy.ValueRO.spawnPosition));

            enemy.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + enemy.ValueRO.spawnRate;

        }

    }

    Vector3 GetRandomPointInCircle(float radius)
    {
        float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        return new Vector3(x, y, 0);
    }
    
}


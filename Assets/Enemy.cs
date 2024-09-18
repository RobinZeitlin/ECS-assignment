using Unity.Entities;
using Unity.Mathematics;

public struct Enemy : IComponentData
{
    public Entity prefab;
    public float3 spawnPosition;
    public float nextSpawnTime;
    public float spawnRate;
}

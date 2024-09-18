using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float SpawnRate;
    
    class SpawnerBaker : Baker<SpawnAuthoring>
    {
        public override void Bake(SpawnAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Enemy
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                spawnPosition = float3.zero,
                nextSpawnTime = 0,
                spawnRate = authoring.SpawnRate
                
            });
        }
    }

}

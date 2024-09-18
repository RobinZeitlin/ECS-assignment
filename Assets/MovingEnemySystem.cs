using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MovingEnemySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;

        NativeArray<Entity> entities = entityManager.GetAllEntities(Allocator.Temp);
        Vector3 targetPos = PlayerPos(entityManager, entities);
        foreach ((RefRW<LocalTransform> transform, RefRO<Speed> speed)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>>())
        {
            transform.ValueRW.Position = Vector3.MoveTowards(transform.ValueRO.Position, targetPos, speed.ValueRO.Value * SystemAPI.Time.DeltaTime);
        }
    }

    Vector3 PlayerPos(EntityManager entityManager, NativeArray<Entity> allEntities)
    {
        foreach (var e in allEntities)
        {
            if (entityManager.HasComponent<Player>(e))
            {
                var component = entityManager.GetComponentData<LocalTransform>(e);
                return component.Position;
            }
        }

        return Vector3.zero;
    }
}

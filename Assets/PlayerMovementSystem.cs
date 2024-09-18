using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        foreach (var (transform, player) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Player>>())
        {
            Vector3 input = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(input);

            Vector2 playerPos = new Vector2(transform.ValueRO.Position.x, transform.ValueRO.Position.y);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            Vector2 direction = worldPos2D - playerPos;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
            transform.ValueRW = transform.ValueRO.WithRotation(rotation);

            if (Input.GetMouseButtonDown(0))
            {
                Entity bulletEntity = ecb.Instantiate(player.ValueRO.bulletPrefab);

                float3 forwardDirection = new float3(direction.x, direction.y, 0f);
                float3 spawnPosition = transform.ValueRO.Position + forwardDirection * 1.1f;

                ecb.SetComponent(bulletEntity, LocalTransform.FromPositionRotation(spawnPosition, transform.ValueRO.Rotation));

                ecb.AddComponent(bulletEntity, new Bullet
                {
                    direction = forwardDirection,
                    speed = 10f
                });
            }
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

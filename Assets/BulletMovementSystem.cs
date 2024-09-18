using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct BulletMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // Query all bullets with a Bullet component and a LocalTransform component
        foreach (var (bullet, transform) in SystemAPI.Query<RefRW<Bullet>, RefRW<LocalTransform>>())
        {
            // Calculate the new position based on the direction and speed
            float3 newPosition = transform.ValueRO.Position + bullet.ValueRO.direction * bullet.ValueRO.speed * SystemAPI.Time.DeltaTime;

            // Update the bullet's position
            transform.ValueRW = transform.ValueRO.WithPosition(newPosition);
        }
    }
}

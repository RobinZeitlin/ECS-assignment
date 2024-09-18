using UnityEngine;
using Unity.Entities;

public class PlayerAuthoring : MonoBehaviour
{
    public Vector2 mousePos;
    public float Speed;
    public GameObject bulletPrefab;

    private class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Entity bulletEntityPrefab = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player 
            {
                bulletPrefab = bulletEntityPrefab
            });
        }
    }
}

public struct Player : IComponentData
{
    public Vector2 mousePos;
    public float Speed;
    public Entity bulletPrefab;

}
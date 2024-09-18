using UnityEngine;
using Unity.Entities;

public class SpeedAuthoring : MonoBehaviour
{
    public float Value;

    private class Baker : Baker<SpeedAuthoring>
    {
        public override void Bake(SpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Speed { Value = authoring.Value });
        }
    }
}

public struct Speed : IComponentData
{
    public float Value;
}
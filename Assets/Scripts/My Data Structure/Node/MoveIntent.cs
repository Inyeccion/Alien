using UnityEngine;

public class MoveIntent
{
    public MovementType movementType;
    public Transform transform;

    public MoveIntent(MovementType type, Transform targetTransform)
    {
        movementType = type;
        transform = targetTransform;
    }
}

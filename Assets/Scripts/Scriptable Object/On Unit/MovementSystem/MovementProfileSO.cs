using UnityEngine;
//CharacterMotor”√
[CreateAssetMenu(fileName = "MovementProfile",menuName = "Scriptable Object/Movement Profile")]
public class MovementProfileSO : ScriptableObject
{
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float unitFriction = 1f;
}

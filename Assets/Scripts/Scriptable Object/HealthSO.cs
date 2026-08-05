using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Scriptable Object/Health")]
public class HealthSO : ScriptableObject
{
    public float currentHealth;

    public float maxHealth;
}

using UnityEngine;

[CreateAssetMenu(fileName = "BounceOffSkillEffect", menuName = "Scriptable Object/Active Skill Effect/Bounce Off Skill Effect")]
public class BounceOffSkillEffectSO : ActiveSkillEffectSO
{
    public float bounceVelocity = 10f;
    public float bounceRadius = 5f;
    public float damage = 1f;

    [SerializeField]private LayerMask layerMask;
    public override void Activate(SkillContext skillContext)
    {
        Vector3 currentPos = skillContext.position;
        Collider[] colliders = RadiusCheck(currentPos);
        foreach (Collider collider in colliders)
        {
            //排除对自己的检测
            if (collider.name != skillContext.gameObject.name)
            {
                Debug.Log("检测到collider: " + collider.name);
                Health health = collider.GetComponent<Health>();
                CharacterMotor characterMotor = collider.GetComponent<CharacterMotor>();
                if (health != null)
                {
                    health.TakeDamage(damage);

                    Vector3 direction = (collider.transform.position - currentPos).normalized;
                    Vector3 initialvelocity = direction * bounceVelocity;
                    characterMotor.AddExternalVelocity(initialvelocity);
                }
                else Debug.LogWarning("Collider does not have a Health component: " + collider.name);
            }

        }
        Debug.Log("BounceoffSKillEffect Activated");

    }

    private Collider[] RadiusCheck(Vector3 position)
    {   
        return Physics.OverlapSphere(position, bounceRadius, layerMask);
    }

}

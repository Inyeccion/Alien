using UnityEngine;

[CreateAssetMenu(fileName = "BounceOffSkillEffect", menuName = "Scriptable Object/Active Skill Effect/Bounce Off Skill Effect")]
public class BounceOffSkillEffectSO : ActiveSkillEffectSO
{
    public float bounceForce = 10f;
    public float bounceRadius = 5f;
    public float damage = 1f;
    public override void Activate(SkillContext skillContext)
    {
        Debug.Log("BounceoffSKillEffect Activated");
    }

}

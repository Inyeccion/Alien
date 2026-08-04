using UnityEngine;

public abstract class ActiveSkillEffectSO : ScriptableObject
{
    public abstract void Activate(SkillContext skillContext);
}

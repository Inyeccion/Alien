using UnityEngine;

public abstract class ActiveSkillEffect : MonoBehaviour
{
    public abstract void Activate(SkillContext skillContext);
}

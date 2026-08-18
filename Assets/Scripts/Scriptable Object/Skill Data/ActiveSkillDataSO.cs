using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillData", menuName = "Scriptable Object/Active Skill Data")]
public class ActiveSkillDataSO : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;

    public float skillCooldown;

    public SkillType skillType;

    public ActiveSkillEffectSO activeSkillEffect;

    public bool IsMainSkill()
    {
        if (skillType == SkillType.manualMain) return true;
        else return false;
    }

    public bool IsMinorSkill()
    {
        if (skillType == SkillType.manualMinor) return true;
        else return false;
    }
}

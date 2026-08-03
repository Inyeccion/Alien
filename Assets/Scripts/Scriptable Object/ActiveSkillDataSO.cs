using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillData", menuName = "Scriptable Object/Active Skill Data")]
public class ActiveSkillDataSO : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;

    public float skillCooldown;

    public SkillType skillType;

    public ActiveSkillEffect activeSkillEffect;
}

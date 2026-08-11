using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public UnitDataSO unitData;

    private List<SkillInstance> activeSkillInstances = new List<SkillInstance>();
    private void Start()
    {
        foreach (var skillData in unitData.activeSkills)
        {
            SkillInstance skillInstance = new SkillInstance
            {
                skillData = skillData,
                currentCooldown = 0f
            };
            activeSkillInstances.Add(skillInstance);
        }
    }

    public void ActivateMainSkill()
    {
        Debug.Log("AbilitySystem: ActivateMainSkill");
        foreach (var skillInstance in activeSkillInstances)
        {
            //只遍历当前技能实例的冷却时间为0的技能
            if (skillInstance.currentCooldown == 0)
                //判断当前技能实例是否为主要技能
                if (skillInstance.skillData.IsMainSkill())
                {
                    //这个构造函数是后续要动态更改的
                    SkillContext skillContext = new SkillContext(gameObject);

                    skillInstance.skillData.activeSkillEffect.Activate(skillContext);
                    skillInstance.currentCooldown = skillInstance.skillData.skillCooldown;
                }
        }
    }


    private void Update()
    {
        //技能冷却
        foreach (var skillInstance in activeSkillInstances)
        {
            if (skillInstance.currentCooldown > 0)
            {
                skillInstance.currentCooldown -= Time.deltaTime;
                if (skillInstance.currentCooldown < 0)
                    skillInstance.currentCooldown = 0;
            }
        }
    }
}

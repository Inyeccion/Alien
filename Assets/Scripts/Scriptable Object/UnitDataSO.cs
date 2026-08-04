using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/Unit Data")]
public class UnitDataSO : ScriptableObject
{
    public string unitName;

    public float unitHealth;

    public ActiveSkillDataSO[] activeSkills;


    //后续加入模型等
}

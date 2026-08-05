using UnityEngine;

public class SkillContext
{
    public Health health;       //单位的血量数据SO

    public Vector3 position;        //单位的位置

    public Vector3 skillOffset;     //技能释放位置偏移量

    public SkillContext(GameObject gameObject)
    {
        this.position = gameObject.transform.position;

        health = gameObject.GetComponent<Health>();
    }
}

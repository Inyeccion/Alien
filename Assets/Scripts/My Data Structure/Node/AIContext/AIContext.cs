using UnityEngine;

[System.Serializable]
public class AIContext
{
    public AbilitySystem abilitySystem;             //技能系统
    //public CharacterMotor characterMotor;           //物理组件
    public MovementController movementController;   //Steering Behavior系统中枢

    public BlackBoard blackBoard;
}

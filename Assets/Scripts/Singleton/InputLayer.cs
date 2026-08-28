using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputLayer : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction slowTimeAction;
    [SerializeField] private InputAction exitHostAction;
    [SerializeField] private InputAction mainSkillAction;
    [SerializeField] private InputAction minorSkillAction;
    public InputAction hostAction;

    private Vector2 action;

    //Debug
    [SerializeField] private float currentTimeScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        slowTimeAction.Enable();
        exitHostAction.Enable();
        mainSkillAction.Enable();
        minorSkillAction.Enable();
        HostManagerSO.SetInputLayer(this);
    }

    private void Update()
    {
        //移动控制
        action = moveAction.ReadValue<Vector2>();
        if (action != Vector2.zero)
        {
            Debug.Log("InputLayer: Movement input detected.");
            HostManagerSO.InformMotorToProcessMoveAction(action);
        }
        else
        {
            Debug.Log("InputLayer: No movement input detected.");
            HostManagerSO.InformMotorToProcessEmptyMoveAction();
        }


        //时间控制
        if (slowTimeAction.IsPressed())
        {
            HostManagerSO.SlowTimeScale();
            //寄生
            if (hostAction.WasPressedThisFrame())
                HostManagerSO.RayCastToSetHost();
        }
        else HostManagerSO.ResetTimeScale();

        //退出寄生
        if (exitHostAction.WasPressedThisFrame())
        {
            HostManagerSO.ExitHost();
        }
            
        //主要技能按键检测
        if (mainSkillAction.WasPressedThisFrame())
        {
            Debug.Log("InputLayer: mainSkillAction WasPressedThisFrame");
            AbilitySystem currentAbilitySystem = HostManagerSO.currentHost.gameObject.GetComponent<AbilitySystem>();
            currentAbilitySystem.ActivateMainSkill();
        }

        //Debug
        currentTimeScale = Time.timeScale;

    }
}

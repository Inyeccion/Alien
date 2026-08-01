using UnityEngine;
using UnityEngine.InputSystem;

public class InputLayer : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction slowTimeAction;
    [SerializeField] private InputAction exitHostAction;
    public InputAction hostAction;

    [SerializeField] private HostManagerSO hostManager;

    private Vector2 action;

    //Debug
    [SerializeField] private float currentTimeScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
        slowTimeAction.Enable();
        exitHostAction.Enable();
        hostManager.SetInputLayer(this);
    }

    private void Update()
    {
        action = moveAction.ReadValue<Vector2>();
        //时间控制
        if (slowTimeAction.IsPressed())
        {
            hostManager.SlowTimeScale();
            //寄生
            if (hostAction.WasPressedThisFrame())
                hostManager.RayCastToSetHost();
        }
        else hostManager.ResetTimeScale();

        //退出寄生
        if (exitHostAction.WasPressedThisFrame())
        {
            hostManager.ExitHost();
        }
            


        //Debug
        currentTimeScale = Time.timeScale;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hostManager.ProcessMoveAction(action);
    }


}

using UnityEngine;
using UnityEngine.InputSystem;

public class InputBridge : MonoBehaviour
{
    [SerializeField] private InputAction moveAction;

    [SerializeField] private HostManagerSO hostManager;

    private Vector2 action;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.Enable();
    }

    private void Update()
    {
        action = moveAction.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hostManager.ProcessMoveAction(action);
    }
}

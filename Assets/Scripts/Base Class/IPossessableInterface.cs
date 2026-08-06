using UnityEngine;

public interface IPossessable
{
    GameObject gameObject { get; }
    Transform transform { get; }

    void OnEnterHost();

    void OnExitHost();

    void OnFinalVelocityInput(Vector3 finalVelocity);
}

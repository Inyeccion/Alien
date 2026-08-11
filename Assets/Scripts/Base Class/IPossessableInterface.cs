using UnityEngine;

public interface IPossessable
{
    GameObject gameObject { get; }
    Transform transform { get; }

    void OnEnterHost();

    void OnExitHost();

    Vector3 OnFinalVelocityInput(Vector3 finalVelocity);
}

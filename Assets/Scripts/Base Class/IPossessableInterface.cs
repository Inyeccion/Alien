using UnityEngine;

public interface IPossessable
{
    GameObject gameObject { get; }
    Transform transform { get; }

    void OnEnterHost();

    void OnExitHost();

    void OnMoveInput(Vector2 action);
}

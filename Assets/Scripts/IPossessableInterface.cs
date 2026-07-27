using UnityEngine;

public interface IPossessable
{
    GameObject gameObject { get; }
    Transform transform { get; }

    void OnMoveInput(Vector2 action);

}

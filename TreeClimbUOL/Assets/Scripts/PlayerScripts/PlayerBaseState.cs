using UnityEngine;

public enum MovementAxis { Horizontal, Vertical }
public abstract class PlayerBaseState
{
    public abstract Color Colour { get;}

    public abstract void PrintState();
    public abstract void EnterState(PlayerStateModel state);

    public abstract void UpdateState(PlayerStateModel state);

    public abstract void OnCollision(PlayerStateModel state,Collision collision);
}

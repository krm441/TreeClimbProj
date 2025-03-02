using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private Renderer playerRenderer;

    public override Color Colour { get { return new Color(0.5f, 0, 0.5f, 1f); } }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {
    }

    public override void PrintState()
    {
    }

    public override void UpdateState(PlayerStateModel state)
    {
        //throw new System.NotImplementedException();
    }
    public override void EnterState(PlayerStateModel state)
    {
        playerRenderer = state.gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Colour);
        state.PlayerAirState = true;
        PrintState();
    }

}

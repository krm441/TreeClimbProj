using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    private Renderer playerRenderer;
    public override Color Colour { get { return new Color(0.25f, 0.7f, 0.5f, 1f); } }

    public override void EnterState(PlayerStateModel state)
    {
       
        state.PlayerHurtState = true;
        //
        state.PlayerClimbingState = false;
        state.PlayerHoldingOnLadder = false;

        playerRenderer = state.gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Colour);

       // Wait(state);


    }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {
      
    }

    public override void PrintState()
    {
        Debug.Log("Player Hurt");
    }

    public override void UpdateState(PlayerStateModel state)
    {
        
    }
}

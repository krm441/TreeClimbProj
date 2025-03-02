using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    [SerializeField]
    private float jumpHeight = 9.81f*25;
    private Renderer playerRenderer;
    public override Color Colour { get { return new Color(0.5f, 1f, 0, 1f); } }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {
   
    }

    public override void PrintState()
    {
       Debug.Log("Jumping");
    }

    public override void UpdateState(PlayerStateModel state)
    {
     
    }

    public override void EnterState(PlayerStateModel state)
    {
        playerRenderer = state.gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Colour);
        /*Apply a force upwards if the player is on the ground*/
        if (!state.PlayerAirState)
        {
            state.rigidBody.AddForce(jumpHeight * state.transform.up);
            state.PlayerAirState = true;
        }

        PrintState();
    }

}

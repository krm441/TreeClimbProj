using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerBaseState
{
    [SerializeField]
    private Renderer playerRenderer;


    public override Color Colour { get { return new Color(0.5f, 0.31f, 0.235f, 1f); } }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {

    }

    public override void PrintState()
    {
       Debug.Log("Idle");
    }

    public override void UpdateState(PlayerStateModel state)
    {
        /*Allow the player to move up and down the ladder*/
        float[] array = state.GetDirectionalArray();
        int verticalDirection = array[1] > 0 ? 1 : 0 > array[1] ? -1 : 0;
        float ladderHeight = state.LadderBounds.size.y / 2;
 
        bool canMove = verticalDirection >0 ? state.LadderBounds.center.y + ladderHeight/2 > state.gameObject.transform.position.y - state.GetPlayerHeight :
             state.LadderBounds.center.y - ladderHeight/2 < state.gameObject.transform.position.y;

        if (canMove)
        {
            state.gameObject.transform.Translate(5f * Time.deltaTime * new Vector3(0, verticalDirection, 0));
        }
  
        /*If the player has let go of the ladder make them fall*/
        if (!state.PlayerHoldingOnLadder)
        {
            state.PlayerClimbingState = false;
            state.rigidBody.useGravity = true;
        }
    }

    public override void EnterState(PlayerStateModel state)
    {
        playerRenderer = state.gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Colour);
        state.PlayerClimbingState = true;
        state.PlayerHoldingOnLadder = true;
        Vector3 ladderCentering = state.LadderBounds.center;
        ladderCentering.y = state.gameObject.transform.position.y;
        state.gameObject.transform.position = Vector3.MoveTowards(state.gameObject.transform.position,ladderCentering,1);
        /*Prevent gravity from acting on the player*/
        state.rigidBody.useGravity = false;
        /*Remove all forces that acting on the player*/
        state.rigidBody.velocity = Vector3.zero;
        PrintState();
    }

}
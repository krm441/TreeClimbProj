using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    [SerializeField]
    private Renderer playerRenderer;


    public override Color Colour { get { return new Color(0.5f, 1f, 0, 1f); } }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {
    }

    public override void PrintState()
    {
       Debug.Log("Idle");
    }

    public override void UpdateState(PlayerStateModel state)
    {
    }

    public override void EnterState(PlayerStateModel state)
    {
        playerRenderer = state.gameObject.GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Colour);
        PrintState();
    }

}
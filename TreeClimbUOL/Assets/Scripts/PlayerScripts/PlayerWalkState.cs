using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    readonly float moveSpeed = 5f;

    // readonly float rotateSpeed = 5f;

    /*For determining if this is the frame in which the user enter the state */
    bool justEnteredState = false;
    public override Color Colour { get { return new Color(1, 0, 0, 1f); } }

    /*Moves the character in the y and x axis*/
    private void MovePlayerInXY(PlayerStateModel state)
    {
        float[] array = state.GetDirectionalArray();

        int xAngle = array[0] > 0 ? 90 : 0 > array[0] ? 270 : 0;
        int yAngle = array[1] > 0 ? 0 : 0 > array[1] ? 180 : 0;
        /*Get the angle the player is moving*/
        float angle = (yAngle + xAngle)/(Mathf.Abs(array[0])+ Mathf.Abs(array[1]));
        /*Edge case for moving up and left*/
        if (array[1] == 1 && array[0] == -1)
        {
            angle = 315;
        }
        /*Rotate the player in the direction they are moving*/
        state.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0);
        /*Move the player forward or backwards*/
        state.gameObject.transform.Translate(moveSpeed * Time.deltaTime * new Vector3(0, 0, 1));
    }

    public override void EnterState(PlayerStateModel state)
    {
        Renderer playerRenderer = state.gameObject.GetComponent<Renderer>();
        state.GetDirectionalArray();
        playerRenderer.material.SetColor("_Color", Colour);
        MovePlayerInXY(state);
        justEnteredState = true;
        PrintState();
    }

    public override void OnCollision(PlayerStateModel state, Collision collision)
    {
       // throw new System.NotImplementedException();
    }

    public override void PrintState()
    {
       Debug.Log("Walking");
    }

    public override void UpdateState(PlayerStateModel state)
    {
        /*Ingnore on the first update frame to prevent the user moving twice in that frame*/
        if (!justEnteredState)
        {
            MovePlayerInXY(state);
        }
        else {
            justEnteredState = false;
        }
    }
}


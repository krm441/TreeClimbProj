using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    // readonly float rotateSpeed = 5f;

    /*For determining if this is the frame in which the user enter the state */
    bool justEnteredState = false;
    public override Color Colour { get { return new Color(1, 0, 0, 1f); } }

    /*Moves the character in the y and x axis*/
    private void MovePlayer(PlayerStateModel state)
    {
        //If the player is using three dimensional movement (moving across nodes)
        if (state.Mode == "3D")
        {
            float horizontalDirection = state.GetDirectionalArray()[0];
            //Move right
            if (horizontalDirection == 1 && state.NextNodeIndex < state.PathNodes.Count)
            {
                if (MoveTowardsNode(state.NextNodeIndex, state))
                {
                    state.PreviousNodeIndex = state.NextNodeIndex; // exchange indicies
                    state.NextNodeIndex++;
                    /*If we reach the end on a node list*/
                    if (state.NextNodeIndex == state.PathNodes.Count)
                    {
                        if (state.CurrentLevel < 2)
                        {
                            state.CurrentLevel += 1;
                            state.GetLevelNodes(state.CurrentLevel);
                        }
                        else
                        {
                            state.NextNodeIndex = state.PathNodes.Count - 1;
                        }
                    }
                }
            }
            //Move left
            else if (horizontalDirection == -1 && state.PreviousNodeIndex >= 0)
            {
                if (MoveTowardsNode(state.PreviousNodeIndex, state))
                {
                    state.NextNodeIndex = state.PreviousNodeIndex; // exchange indicies
                    state.PreviousNodeIndex--;
                    if (state.PreviousNodeIndex < 0)
                    {
                        if (state.CurrentLevel > 1)
                        {
                            if (state.CurrentLevel == 3 && state.PreviousNodeIndex == 0)
                            {
                                state.CurrentLevel -= 1;
                                state.GetLevelNodes(state.CurrentLevel, true);
                            }
                        }
                        else
                        {
                            state.PreviousNodeIndex = 0;
                            state.NextNodeIndex = 1;
                        }

                    }

                }
            }
        }
        //Otherwise use two dimensional movement
        else
        {
            float horizontalDirection = state.GetDirectionalArray()[0];
            //If a horizontasl key is pressed 
            if (horizontalDirection != 0)
            {
                state.transform.rotation = Quaternion.Euler(0f, -90 * (-1 + horizontalDirection), 0f);
                state.gameObject.transform.position += state.moveSpeed * Time.deltaTime * state.transform.forward;
            }
        }
    }

    public override void EnterState(PlayerStateModel state)
    {
        Renderer playerRenderer = state.gameObject.GetComponent<Renderer>();
        state.GetDirectionalArray();
        playerRenderer.material.SetColor("_Color", Colour);
        MovePlayer(state);
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
            MovePlayer(state);
        }
        else
        {
            justEnteredState = false;
        }
    }

    bool MoveTowardsNode(int targetIndex, PlayerStateModel state)
    {
        Transform targetNode = state.PathNodes[targetIndex];
        Vector3 target = targetNode.position;


        //transform.position +=  new Vector3(target.x - transform.position.x / moveSpeed * Time.deltaTime, 0, 0);
        //if (!state.IsClimbingLadder)
        //{
        //    target.y = state.transform.position.y;
        //}
        Vector3 nodePosition = Vector3.MoveTowards(state.transform.position, target, state.moveSpeed * Time.deltaTime) - state.transform.position;
        state.transform.LookAt(target, new Vector3(0, 1, 0));
        //Prevent vertical movement if not on a ladder

        //if (!state.IsClimbingLadder)
        //{
        //    nodePosition.y = 0;

        //}

        state.transform.position += nodePosition;
        // Check if reached the target node
        if (Vector3.Distance(state.transform.position, target) < 0.1f)
        {
            //currentNodeIndex = targetIndex;
            return true;
        }
        return false;
    }


}


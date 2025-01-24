using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
//For controlling the state of player
public class PlayerStateModel : MonoBehaviour
{
    /*The surface the player can land on to reset thier jump*/
    private readonly IDictionary<string, bool> groundingPlatforms = new Dictionary<string, bool>()
    {
        { "Ladder(Clone)",true },
        {"Ground",true }
    };
    /*For the key movement*/
    public InputActionReference movement;
    /*For jumping*/
    public InputActionReference jumping;
    /*For Climbing*/
    public InputActionReference climbing;
    /*For throwing snowballs*/
    public InputActionReference throwing;
    /*Reference to the current player state*/
    PlayerBaseState currentState;
    /*The other player states*/
    readonly PlayerIdleState idleState = new PlayerIdleState();
    readonly PlayerWalkState walkState = new PlayerWalkState();
    readonly PlayerJumpState jumpState = new PlayerJumpState();
    readonly PlayerFallState fallState = new PlayerFallState();
    readonly PlayerClimbState climbState = new PlayerClimbState();
    /*For spawning snowballs*/
    public GameObject snowball;
    Renderer playerRender;

    /*The minimun downward force that needs to be acting on the player for them to be considered falling.
     Used as a fix for an issue in which the player is consider to be falling when near edges.*/
    readonly float minimunFallingForce = -0.0001f;

    float playerHeight = 0;
    public float GetPlayerHeight { get => playerHeight; }


    private bool playerInAir = false;
    /*Whetever the player is on the ground or not.*/
    public bool PlayerAirState { get => playerInAir; set => playerInAir = value; }

    private bool nearLadder = false;
    /*Whether the player is near a ladder. */
    public bool NearLadderState { get => nearLadder; set => nearLadder = value; }

    private Bounds ladderBounds = new Bounds();
    /*The bounding of the closest ladder*/
    public Bounds LadderBounds { get => ladderBounds; set => ladderBounds = value; }

    /*Whetever the player is climbing a ladder*/
    private bool onLadder = false;
    public bool PlayerClimbingState { get => onLadder; set => onLadder = value; }

    /*Whetever the player has let go of a ladder*/
    private bool holdingOnLadder = false;
    public bool PlayerHoldingOnLadder { get => holdingOnLadder; set => holdingOnLadder = value; }

    private int snowballs = 10;
    /*The number of snow the player has*/
    public int Snowsballs { get => snowballs; }
    /*Gives the player snowballs*/
    public void GiveSnowBalls()
    {
        snowballs += 4;
        Debug.Log(" + 4 snowballs.");
    }
    /**/
    public Rigidbody rigidBody;

    /*Checks whetever the player is on the ground*/
    public bool IsOnGround(GameObject obj)
    {
        /*Get the name of the object*/
        string objectName = obj.name;
        /*If the player is above the object its touching */
        bool aboveObject = playerRender.bounds.max.y > obj.GetComponent<Renderer>().transform.position.y;
        /*If the player is holdering on to the ladder*/
        bool climbingLadder = PlayerClimbingState && objectName == "Ladder(Clone)";
        /*If the player is top of an object that allows them to stand on*/
        bool onPlatform = PlayerAirState && rigidBody.velocity.y >= minimunFallingForce && aboveObject && groundingPlatforms.ContainsKey(objectName);

        /*If we are either on a platform or climbing ladder*/
        return onPlatform || climbingLadder;
    }
    /*Transitions into the jump state*/
    public void ThrowSnowBall(InputAction.CallbackContext obj)
    {
        /*Check that we aren't climbing as we can't throw snowballs on ladders*/
        if (!PlayerClimbingState)
        {
            /*If we have any snowsballs throw one and decrease number of the snow the player has*/
            if (snowballs > 0)
            {
                snowballs -= 1;
                Debug.Log("  -1 snowballs.");
                Vector3 start = transform.position;
                Instantiate(snowball, start, transform.rotation);
            }

        }
    }

    /*Transitions into the jump state*/
    public void TransistionToJumpState(InputAction.CallbackContext obj)
    {


        /*If we aren't on a ladder allow the player to jump*/
        if (!PlayerAirState)
        {
            PlayerClimbingState = false;
            rigidBody.useGravity = true;
            ChangePlayerState(jumpState);

        }
    }

    /*Transitions into the climb state*/
    public void TransistionToClimbState(InputAction.CallbackContext obj)
    {
        /*If player is not on a ladder attempt to climb one*/
        if (!PlayerClimbingState)
        {
            /*If we are near a ladder climb it*/
            if (NearLadderState)
            {
                ChangePlayerState(climbState);
                PlayerAirState = false;
            }
        }
        /*Otherwise drop off the ladder*/
        else
        {
            PlayerClimbingState = false;
            rigidBody.useGravity = true;
        }
    }

    /*Holds the states of the directional key presses */
    private readonly float[] directionalKeyStates = { 0, 0 };
    //
    /*Get array used to hold the horizontal and vertical keys*/
    public float[] GetDirectionalArray()
    {
        return directionalKeyStates;
    }

    private bool hasDirectionInput;

    /*Return true if a directional key was pressed*/
    private bool HasDirectionalPress()
    {
        for (int i = 0; i < 2; i++)
        {
            /**/
            if (directionalKeyStates[i] != 0)
            {
                return true;
            }
        }
        return false;
    }

    /*Handle keypresses*/
    private void DirectionalKeyChange()
    {
        Vector3 vect = Vector3.zero;
        if (movement)
        {
            vect = movement.action.ReadValue<Vector3>();
        }
        directionalKeyStates[0] = vect.x;
        directionalKeyStates[1] = vect.y;
    }
    /*Changes the current state of the player's FSM*/
    private void ChangePlayerState(PlayerBaseState newState)
    {
        /*If the new state is different to the current state enter that s*/
        if (currentState != newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        /*Start in the idle state*/
        ChangePlayerState(idleState);
        /*Get the player renderer*/
        playerRender = gameObject.GetComponent<Renderer>();
        /*Get the height of the player*/
        playerHeight = playerRender.bounds.size.y;
        //
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        /*Get any directional key presses*/
        DirectionalKeyChange();
        hasDirectionInput = HasDirectionalPress();

        /*If we aren't on a ladder*/
        if (!PlayerClimbingState)
        {
            /*If a direction was pressed switch to walking state*/
            if (hasDirectionInput)
            {
                ChangePlayerState(walkState);
            }
            /*Otherwise return to the idle state*/
            else
            {
                if (!PlayerAirState)
                {
                    ChangePlayerState(idleState);
                }
            }
            /*Only switch to the falling state when the downard force acting on the player is greater than the minimun falling force */
            if (rigidBody.velocity.y < minimunFallingForce)
            {
                ChangePlayerState(fallState);
            }
        }

        /*Perform the state spefic update*/
        currentState.UpdateState(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        /*If the player is on the ground holding on to a ladder allow them to jump*/
        if (IsOnGround(collision.gameObject))
        {
            Debug.Log("Grounded");
            PlayerAirState = false;
        }

        /*Have the other states handle collisions*/
        currentState.OnCollision(this, collision);
    }
    /*Add the player actions for the buttons*/
    private void OnEnable()
    {
        jumping.action.started += TransistionToJumpState;
        climbing.action.started += TransistionToClimbState;
        throwing.action.started += ThrowSnowBall;
    }
    /*Remove any added actions*/
    private void OnDisable()
    {
        jumping.action.started -= TransistionToJumpState;
        climbing.action.started -= TransistionToClimbState;
        throwing.action.started -= ThrowSnowBall;
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        /*If the player is on the ground holding on to a ladder allow them to jump*/
        if (IsOnGround(collisionInfo.gameObject))
        {
            Debug.Log("Grounded");
            PlayerAirState = false;
        }
    }

}
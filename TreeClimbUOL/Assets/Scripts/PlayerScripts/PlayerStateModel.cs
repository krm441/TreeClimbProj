using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
//For controlling the state of player
public class PlayerStateModel : MonoBehaviour
{
    /*For the key movement*/
    public InputActionReference movement;
    /*For jumping*/
    public InputActionReference jumping;
    /*For throwing snowballs*/
    public InputActionReference throwing;
    /*Reference to the current player state*/
    PlayerBaseState currentState;
    /*The other player states*/
    readonly PlayerIdleState idleState = new PlayerIdleState();
    readonly PlayerWalkState walkState = new PlayerWalkState();
    readonly PlayerJumpState jumpState = new PlayerJumpState();
    readonly PlayerFallState fallState = new PlayerFallState();
    readonly PlayerHurtState hurtState = new PlayerHurtState();
    //This contains the all the nodes in the game
    public GameObject nodeContainer;

    //Determines the type of movement the player uses
    private string mode = "3D";
    public string Mode { get => mode; set => mode = value; }

    //The players hitpoints
    int health = 3;
    public int Health { get => health; }

    //Contains the nodes on each level
    readonly List<GameObject> levels = new List<GameObject>();
    private readonly List<Transform> pathNodes = new List<Transform>(); // Drag and drop nodes in the Inspector
    public List<Transform> PathNodes { get => pathNodes; }
    private int nextNodeIndex = 1;
    public int NextNodeIndex { get => nextNodeIndex; set => nextNodeIndex = value; }
    private int previousNodeIndex = 0;
    public int PreviousNodeIndex { get => previousNodeIndex; set => previousNodeIndex = value; }
    public float moveSpeed = 5f;

    int laddersInContactWith = 0;
    public int LaddersInContactWith { get => laddersInContactWith; set => laddersInContactWith = value; }
    /*Player can only move up and down when on a ladder*/
    private bool isClimbingLadder = false;
    public bool IsClimbingLadder { get => isClimbingLadder; }
    /*For spawning snowballs*/
    public GameObject snowball;
    Renderer playerRender;
    MeshRenderer playerMesh;
    public Renderer PlayerRender { get => playerRender; }

    /*The player the player is on*/
    private int currentLevel = 1;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    /*The minimun downward force that needs to be acting on the player for them to be considered falling.
     Used as a fix for an issue in which the player is considered to be falling when near edges.*/
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
    /*Whetever the player has been hit*/
    private bool isHurt = false;
    //If the hit animation is playing
    bool hitAnimationActive = false;
    public bool PlayerHurtState { get => isHurt; set => isHurt = value; }
    private int snowballs = 0;
    /*The number of snow the player has*/

    /*Add the player actions for the buttons*/
    private void OnEnable()
    {
        EventManger.Enter2DMode += SwitchTo2D;
        EventManger.Enter3DMode += SwitchTo3D;
        jumping.action.started += TransistionToJumpState;
        throwing.action.started += ThrowSnowBall;
    }
    /*Remove any added actions*/
    private void OnDisable()
    {
        EventManger.Enter2DMode -= SwitchTo2D;
        EventManger.Enter3DMode -= SwitchTo3D;
        jumping.action.started -= TransistionToJumpState;
        throwing.action.started -= ThrowSnowBall;
    }



    //Moves the player to a node 
    public void TeleportToNode(int level, int nodeIndex)
    {
        //If a vaild level was selected
        if (level >= 1 && level - 1 < levels.Count)
            //Get the node for the level
            GetLevelNodes(level);
        //If there is an node at the selected index
        if (nodeIndex >= 0 && pathNodes.Count > nodeIndex)
        {
            currentLevel = level;
            transform.position = pathNodes[nodeIndex].position;
            previousNodeIndex = nodeIndex - 1 >= 0 ? nodeIndex - 1 : nodeIndex;
            nextNodeIndex = nodeIndex + 1 < pathNodes.Count ? nodeIndex + 1 : pathNodes.Count - 1;
            //Using the teleports reset the player status
            ResetPlayer();

        }
        //Otherwise use the node for the current level
        else
        {
            GetLevelNodes(currentLevel);
        }
    }
    public int Snowsballs { get => snowballs; }
    /*Gives the player snowballs*/
    public void GiveSnowBalls()
    {
        snowballs += 4;
    }
    //The player rigid body
    public Rigidbody rigidBody;
    //Resets the player status
    void ResetPlayer()
    {
        health = 3;
        hitAnimationActive = false;
    }


    /*Checks whetever the player is on the ground*/
    public bool IsOnGround(GameObject obj)
    {
        /*Get the name of the object*/
        // string objectName = obj.name;
        /*If the player is above the object its touching */
        bool aboveObject = playerRender.bounds.max.y > obj.GetComponent<Renderer>().transform.position.y;
        /*If the player is holdering on to the ladder*/
        // bool climbingLadder = PlayerClimbingState && objectName == "Ladder(Clone)";
        /*If the player is top of an object that allows them to stand on*/
        bool onPlatform = PlayerAirState && rigidBody.velocity.y >= minimunFallingForce && rigidBody.velocity.y <= 0 && aboveObject;

        /*If we are either on a platform or climbing ladder*/
        return onPlatform;// || climbingLadder;
    }


    /*Transitions into the jump state*/
    public void ThrowSnowBall(InputAction.CallbackContext obj)
    {
        /*If we are in 2D mode*/
        if (mode == "2D")
        {
            /*If we have any snowsballs throw one and decrease number of the snowballs the player has*/
            if (snowballs > 0)
            {
                snowballs -= 1;
                Vector3 start = transform.position;
                Instantiate(snowball, start, transform.rotation);
            }

        }
    }

    /*Transitions into the jump state*/
    public void TransistionToJumpState(InputAction.CallbackContext obj)
    {


        /*If we aren't in the air or using the 3D mode allow the player to jump*/
        if (!PlayerAirState && mode == "2D")
        {
            PlayerClimbingState = false;
            rigidBody.useGravity = true;
            ChangePlayerState(jumpState);

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
            /*If any of the directional keys are pressed return true*/
            if (directionalKeyStates[i] != 0)
            {
                return true;
            }
        }
        //Otherwise return false
        return false;
    }

    /*Handle key presses*/
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
        /*Get the body of the player*/
        rigidBody = GetComponent<Rigidbody>();
        //Get each node level in the container
        foreach (Transform child in nodeContainer.transform)
        {
            levels.Add(child.gameObject);
        }

        //Get the nodes for the first level
        currentLevel = 1;
        GetLevelNodes(currentLevel);
        playerMesh = GetComponent<MeshRenderer>();
    }

    //Gets nodes from a level and places the player at the start or end
    public void GetLevelNodes(int i, bool playerAtEnd = false)
    {
        //First we clear out all the old nodes
        pathNodes.Clear();
        //Next we get nodes from the given level
        foreach (Transform child in levels[i - 1].transform)
        {
            pathNodes.Add(child.gameObject.transform);
        }

        if (playerAtEnd)
        {
            previousNodeIndex = pathNodes.Count - 2;
            nextNodeIndex = pathNodes.Count - 1;
        }
        else
        {
            nextNodeIndex = 1;
            previousNodeIndex = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        /*Get any directional key presses*/
        DirectionalKeyChange();
        hasDirectionInput = HasDirectionalPress();
        if (!PlayerHurtState)
        {
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

        }
        else
        {
            StartCoroutine(Hit());
        }
        /*Perform the state spefic update*/
        currentState.UpdateState(this);
    }
    //Used to damage the player;
    IEnumerator Hit()
    {
        //If a hit animation is not currently playing start one 
        if (!hitAnimationActive)
        {
            PlayerHurtState = false;
            hitAnimationActive = true;
            //Perform the hit stun
            yield return new WaitForSeconds(0.2f);
            //Subtract a health point


            health -= 1;
            //If the player still has hitpoint activate perform the invincibility flase
            if (health > 0)
            {
                //Cause the player to flash
                for (int i = 0; i < 8; i++)
                {
                    playerMesh.enabled = !playerMesh.enabled;
                    yield return new WaitForSeconds(0.1f);
                }
                playerMesh.enabled = true;
            }
            //Otherwise send them back a level
            else
            {
                GoBackLevel();
            }
            //End the animation
            hitAnimationActive = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (TouchingDangerousObject(collision.gameObject))
        //{
        //    ChangePlayerState(hurtState);
        //}

        /*Have the other states handle collisions*/
        currentState.OnCollision(this, collision);
    }

    //For interacting with ladders 
    public void LadderInteraction(bool isOnLadder)
    {
        //If the player has just gotten on the ladder allow them to climb the ladder
        //Otherwise return back to horizontal movement
        isClimbingLadder = isOnLadder;
        OnTriggerPlatform(isOnLadder);
    }

    //P
    public void OnTriggerPlatform(bool isOnPlatfrom)
    {
        rigidBody.useGravity = !isOnPlatfrom;
        //If the player is on the platfrom
        if (isOnPlatfrom)
        {
            PlayerAirState = false;
            Vector3 vect = rigidBody.velocity;
            vect.y = 0;
            rigidBody.velocity = vect;
        }
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        /*If the player is on the ground or holding on to a ladder allow them to jump*/
        if (IsOnGround(collisionInfo.gameObject))
        {
            Debug.Log("Grounded");
            PlayerAirState = false;
        }
    }
    //For allowing the player to be hurt by external gameobject
    public void HitPlayer()
    {
        //If the player is not going throught an existing hurt animation hurt them
        if (!hitAnimationActive)
        {
            PlayerHurtState = true;
            ChangePlayerState(hurtState);
        }
    }

    //Switches to 2D movement
    public void SwitchTo2D()
    {
        mode = "2D";
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    //Switches to 3D movement
    public void SwitchTo3D()
    {
        mode = "3D";
    }
    //Cause the play to fall back a level
    void GoBackLevel()
    {
        //Reduce the current level the player is on if it is not on the first level 
        if (currentLevel > 1)
        {
            //currentLevel -= 1;
        };
        //Teleport to the start of the level
        TeleportToNode(currentLevel, 0);
        mode = "3D";
        snowballs = 0;
        //Cause a reset
        EventManger.ResetWorld();
    }
}
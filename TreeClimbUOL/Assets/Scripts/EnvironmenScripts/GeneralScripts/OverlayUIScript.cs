using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUIScript : MonoBehaviour
{
    public PlayerStateModel playerModel;
    /*The text for the help messages*/
    public static string helpText;
    /*The text element for the help message*/
    public Text helpUIElement;


    /*The text for the objective messages*/
    public static string objectiveText;
    /*The text element for the player status and objectives*/
    public Text statusElement;

    /*The text for the timer messages*/
    public float timer = 180;
    /*The text element for the help message*/
    public Text timerElement;
    //
    bool isTimerActivated = false;
    /*The number of collectiables in the first level*/
    readonly private int numberOfCollectiblesInLevelOne = 5;
    private static int collectiblesLevelOne = 0;

    private void OnEnable()
    {
        EventManger.startTimer += StartTimer;
        EventManger.stopTimer += EndTimer;
    }

    void OnDisable()
    {
        EventManger.startTimer -= StartTimer;
        EventManger.stopTimer -= EndTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        helpUIElement.text = helpText;
    }
    /*Update the help message*/
    public void UpdateHelpMessage(string str)
    {
        helpText = str;
    }

    /*Update the collectibles list*/
    public void UpdateCollectibles()
    {
        collectiblesLevelOne += 1;
    }


    public void Update()
    {
        ClearUI();
        /*Render level UI*/
        if (isTimerActivated)
        {
            if (playerModel.CurrentLevel == 1)
            {
                LevelOneUI();
            }
            CountDownTimer();
            //If the timer has reached zero
            if (timer <= 0)
            {
                //Stop the timer
                isTimerActivated = false;
                EventManger.stopTimer();
                if (playerModel.CurrentLevel == 1)
                {
                }
                //If we are on level three and the player still has health spawn the teleporter ;
                else if (playerModel.CurrentLevel == 3 && playerModel.Health > 0)
                {
                    EventManger.SpawnExitForLevelThree();
                }
            }
        }

        //Show the players health if they are in a level
        if (playerModel.CurrentLevel > 1 && playerModel.Mode == "2D")
        {
            string statusText = "Health: " + playerModel.Health.ToString();

            if (playerModel.CurrentLevel == 4)
            {
                statusText += "\nSnowBalls: " + playerModel.Snowsballs.ToString();
            }
            statusElement.text = statusText;
        }

        /*Show any help message*/
        helpUIElement.text = helpText;

    }

    /*Clear the UI */
    void ClearUI()
    {
        timerElement.text = "";
        helpUIElement.text = "";
        statusElement.text = "";

    }

    /*Show the UI for the first level.*/
    void LevelOneUI()
    {
        /*Decrement the timer*/
        timer -= Time.deltaTime;
        /*Show the timer,*/
        timerElement.text = System.Math.Round(timer, 2).ToString();
        /*Show the objectives*/
        statusElement.text = collectiblesLevelOne.ToString() + "/" + numberOfCollectiblesInLevelOne.ToString();
    }
    //For counting down the timer
    void CountDownTimer()
    {
        timer -= Time.deltaTime;
        /*Show the timer,*/
        timerElement.text = System.Math.Round(timer, 2).ToString();
    }

    //Starts the timer
    void StartTimer()
    {
        isTimerActivated = true;
        if (playerModel.CurrentLevel == 1)
        {
            timer = 180;
        }
        else
        {
            timer = 45;
        }


    }

    //Ends the timer
    void EndTimer()
    {
        isTimerActivated = false;
        timer = 0;
    }
}

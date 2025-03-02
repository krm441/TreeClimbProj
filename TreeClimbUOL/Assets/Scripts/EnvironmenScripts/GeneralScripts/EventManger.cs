using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManger : MonoBehaviour
{
    public delegate void ChangeTo2D();
    public static event ChangeTo2D Enter2DMode;
    public delegate void ChangeTo3D();
    public static event ChangeTo3D Enter3DMode;
    public delegate void StartTimer();
    public static StartTimer startTimer;
    public delegate void StopTimer();
    public static StopTimer stopTimer;
    //For the timed teleporter
    public delegate void SpawnExit();
    public static SpawnExit endLevelThree;
    public delegate void NextBossAttack();
    public static NextBossAttack nextBossAttack;

    public delegate void SpawnNextSnowpile();
    public static SpawnNextSnowpile spawnNextSnowpile;

    public delegate void GameWonEvent();
    public static GameWonEvent gameWon;

    public delegate void PlayerDeath();
    public static PlayerDeath playerDeath;


    //Calls all the function pertaining to the 2D mode
    public static void SwitchTo2D()
    {
        Enter2DMode?.Invoke();
    }

    //Calls all the function pertaining to the 3D mode
    public static void SwitchTo3D()
    {
        Enter3DMode?.Invoke();
    }

    //Calls all functions to starting the timer
    public static void BeginTimer()
    {
        startTimer?.Invoke();
    }

    //Calls function relating to the spawning of the teleporter in the third level;
    public static void SpawnExitForLevelThree()
    {
        endLevelThree?.Invoke();
    }
    //Actions that are performed when the player dies 
    public static void ResetWorld()
    { 
        EventManger.stopTimer.Invoke();
        if (playerDeath != null)
        {
            EventManger.playerDeath.Invoke();
        }
       
    }

    //Actions that lead to the next snow pile being created
    public static void CreateNextSnowPile()
    {
        EventManger.spawnNextSnowpile.Invoke();
    }

    //Has the boss select another attack to use
    public static void SelectNextAttack()
    {
        EventManger.nextBossAttack.Invoke();
    }

    //Perform the process relate to a player's victory
    public static void  PlayerWins()
    {
        EventManger.gameWon.Invoke();
    }

}


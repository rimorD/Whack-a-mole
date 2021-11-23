using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    // Unity events
    void Start()
    {
        timer = maxTimer;
    }

    //---------------------------------------------------------------------------------------------

    void Update()
    {
        if (!gameManager.gameEnded)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                MoveMole();
        }
    }

    //---------------------------------------------------------------------------------------------

    private void OnMouseDown()
    {
        if (maxTimer > minTimer)
            maxTimer -= timerStep;

        gameManager.IncrementScore();
        MoveMole();
    }

    // Public methods

    public void MoveMole()
    {
        this.transform.position = gameManager.GetRandomHole();
        timer = maxTimer;
    }

    //---------------------------------------------------------------------------------------------


    // Private helpers
    private float maxTimer = 2.0f;
    private float minTimer = 0.5f;
    private float timerStep = 0.05f;

    // Attributes
    public float timer;
    public Game gameManager;
}

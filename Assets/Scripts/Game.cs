using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // Unity events
    void Start()
    {
        // Get a list of hole vector3 positions out of our empty gameobject placeholder list
        holes = GameObject.FindGameObjectsWithTag("Hole").Select<GameObject, Vector3>((holeGO) => { return holeGO.transform.position; }).ToList();
        StartNewGame();
    }

    //---------------------------------------------------------------------------------------------
    void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                GameOver();
            }
            else
            {
                scoreboard.text = score.ToString();
                timer.text = Math.Floor(gameTime).ToString();
            }
        }
    }

    // Public methods
    public void StartNewGame()
    {
        moles = new List<Mole>();
        CreateNewMole();
        ResetScore();
        ResetTimer();
    }

    //---------------------------------------------------------------------------------------------

    public void ResetTimer()
    {
        gameTime = timerMax;
    }

    //---------------------------------------------------------------------------------------------

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //---------------------------------------------------------------------------------------------

    public void ResetScore()
    {
        score = 0;
    }

    //---------------------------------------------------------------------------------------------

    public void IncrementScore()
    {
        score++;

        // Maybe spawn new mole :)
        if (moles.Count < this.maxNumberOfMoles)
        {
            if (UnityEngine.Random.Range(0, 100) <= newMoleChance)
            {
                CreateNewMole();
                this.newMoleChance = this.newMoleMinChance;
            }
            else
            {
                this.newMoleChance += this.newMoleChanceStep;
            }
        }
    }

    //---------------------------------------------------------------------------------------------

    public Vector3 GetRandomHole()
    {
        Vector3 hole;
        do
        {
            hole = holes[UnityEngine.Random.Range(0, holes.Count - 1)];
        } while (moles.Any((mole) => mole.gameObject.transform.position == hole));
        return hole;
    }

    // Private helpers
    private void CreateNewMole()
    {
        GameObject newMoleGO = GameObject.Instantiate(molePrefab, GetRandomHole(), Quaternion.identity);
        Mole newMole = newMoleGO.GetComponent<Mole>();
        newMole.gameManager = this;
        moles.Add(newMole);
    }

    // Private helpers
    private float gameTime = 0;
    private int score = 0;
    private List<Vector3> holes;
    private float newMoleChance = 5.0f;
    private float newMoleMinChance = 5.0f;
    private float newMoleChanceStep = 0.5f;
    private int maxNumberOfMoles = 4;

    // Attributes
    public float timerMax = 60.0f;
    public List<Mole> moles;
    public GameObject molePrefab;
    public bool gameEnded = false;

    // UI 
    public Text scoreboard;
    public Text timer;
    public Text endGamePoints;
    public GameObject GameOverPanel;



    // Public methods

    // Private helpers
    private void GameOver()
    {
        // Stop updating
        gameEnded = true;

        // Show player score and new game button
        endGamePoints.text = String.Format("You've earned {0} points!", score);
        GameOverPanel.SetActive(true);
    }


}

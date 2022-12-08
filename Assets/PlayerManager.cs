using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int playerId;
    [SerializeField]
    private string playerName;
    [SerializeField]
    private int playerScore = 0;
    public GameObject progressPopupGameObject;

    private ArrayList tasks = new ArrayList();
    private ProgressPopUp progressPopup;

    [SerializeField]
    private int previousDay;
    private int today;


    private void Start()
    {
        today = (int)System.DateTime.Now.Day;
        Debug.Log(previousDay);
        Debug.Log(today);

        playerId = 23;
        tasks = new ArrayList(5);

        progressPopup = progressPopupGameObject.GetComponent<ProgressPopUp>();

        progressPopup.setWaterCondition("GOOD");
        progressPopup.setLitterCondition("GOOD");
        progressPopup.setAirCondition("GOOD");
        if (today != previousDay)
        {
            previousDay = today;
            Generate5DailyTask();
        }

        //Get WorldDesign class
    }
    private void Update()
    {
        progressPopup.setTextScore(this.playerScore);
        progressPopup.setTextName(this.playerName);
    }


    #region Class Methods and Constructor
    public PlayerManager(int playerId)
    {
        this.playerId = playerId;
    }

    public ArrayList Generate5DailyTask()
    {
        string[] availableTask = new string[] { "RubbishMinigame", "BrushingTeethMinigame", "SwitchLightsMinigame", "ShoppingMinigame", "TransportMinigame" };
        int NUMBER_OF_TASK = 5;
        for (int i = 0; i < NUMBER_OF_TASK; i++)
        {
            float randomNumber = Random.Range(0, 4);
            Task currentTask = Task.CreateInstance(playerId, Random.Range(0, 1000), availableTask[(int)randomNumber], "daily", "not done");
            this.tasks.Add(currentTask);
            progressPopup.setTask(i, currentTask);
        }
        return this.tasks;
    }

    public int ComputeScore()
    {
        foreach (Task task in this.tasks)
        {
            if (task.getStatus() == "done")
            {
                this.playerScore += 20;
            } else if (task.getStatus() == "failed")
            {
                this.playerScore -= 10;
            }
        }
        return this.playerScore;
    }

    public void UpdateTaskStatus(int taskId, string status)
    {
        foreach (Task task in this.tasks)
        {
            if (task.getId() == taskId)
            {
                task.setStatus(status);
            }
        }
        ComputeScore();
    }

    public void DisplayPopUp(Task[] dailyTasks)
    {
        //Call popup function and display the tasks of dailyTasks
    }

    #endregion
}

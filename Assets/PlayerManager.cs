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


    private void Start()
    {
        progressPopup = progressPopupGameObject.GetComponent<ProgressPopUp>();

        progressPopup.setWaterCondition("GOOD");
        progressPopup.setLitterCondition("GOOD");
        progressPopup.setAirCondition("GOOD");

        //Get WorldDesign class
    }
    private void Update()
    {
        progressPopup.setTextScore(this.playerScore);
        progressPopup.setTextName(this.playerName);

        
    }


    #region Class Methods and Constructor

    private string[] availableTask = new string[] { "RubbishMinigame", "BrushingTeethMinigame", "SwitchLightsMinigame", "ShoppingMinigame" , "TransportMinigame" };
    public PlayerManager(int playerId)
    {
        this.playerId = playerId;
    }

    public ArrayList Generate5DailyTask()
    {
        float randomNumber = Random.Range(0, 4);
        int NUMBER_OF_TASK = 5;
        for (int i = 0; i < NUMBER_OF_TASK; i++)
        {
            this.tasks.Add(new Task(playerId, 0, this.availableTask[(int)randomNumber], "daily", "not done"));
        }
        return this.tasks;
    }

    public int ComputeScore()
    {
        foreach (Task task in this.tasks)
        {
            if (task.getStatus() == "done")
            {
                this.playerScore += 1;
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
    }

    public void DisplayPopUp(Task[] dailyTasks)
    {
        //Call popup function and display the tasks of dailyTasks
    }

    #endregion
}

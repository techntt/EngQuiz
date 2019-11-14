using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Inspector Variables
    public GameObject countdownPnl;
    public Text tvCountDown,tvCurrent,tvMax,tvScore,tvTime;
    public Text tvQuestion;
    public Slider timeProgress;
    public Image fillImg;
    #endregion

    #region Member Variables
    int countdown = 5;
    int questionTime = 0;
    int maxQuestionTime = 15;
    int currentQuest = 0;
    int maxQuest;
    int totalTime = 0;
    int score;
    Queue<Question> listQuest;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        countdownPnl.SetActive(true);
        // Prepare question
        listQuest = new Queue<Question>();
        for(int i = 0; i < 10; i++)
        {
            listQuest.Enqueue(DataManager.Instance.data.GetQuestionById(0));
        }
        // Countdown Game
        CountdownTask();
    }
    private void OnDisable()
    {
        
    }
    #endregion

    #region Public Methods
    public void BackToMenu()
    {
        GameManager.Instance.ChangePhase(GAME_PHASE.MENU);
    }
    #endregion

    #region Private Methods
    private void StartGame()
    {
        score = 0;
        tvMax.text = listQuest.Count.ToString();
        tvScore.text = score.ToString();
        currentQuest = 1;
        DisplayQuestion();
    }
    private void EndGame()
    {
        CancelInvoke("AddTime");
    }

    private void DisplayQuestion()
    {
        if (listQuest.Count <= 0)
        {
            EndGame();
            return;
        }
            
        Question quest = listQuest.Dequeue();
        tvCurrent.text = currentQuest.ToString();
        tvQuestion.text = quest.question;

        questionTime = maxQuestionTime;
        ChangeTime();
        InvokeRepeating("AddTime", 1, 1);
    }

    private void CountdownTask()
    {
        tvCountDown.gameObject.SetActive(false);
        tvCountDown.text = countdown.ToString();
        tvCountDown.gameObject.SetActive(true);
        countdown--;
        if (countdown >= 0)
            Invoke("CountdownTask", 1);
        else
        {
            countdownPnl.SetActive(false);
            StartGame();
        }
            
    }

    private void AddTime()
    {
        totalTime++;
        if (questionTime > 0)
        {
            questionTime--;
            ChangeTime();
        }
        else
        {
            CancelInvoke("AddTime");
            currentQuest++;
            DisplayQuestion();
        }
    }
    private void ChangeTime()
    {
        tvTime.text = questionTime.ToString();
        timeProgress.value = (float)questionTime / maxQuestionTime;
        if (timeProgress.value > 0.6f)
            fillImg.color = Color.green;
        else if (timeProgress.value > 0.3)
            fillImg.color = Color.yellow;
        else
            fillImg.color = Color.red;
    }
    #endregion
}

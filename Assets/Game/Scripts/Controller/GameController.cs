using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : SingletonMonoBehaviour<GameController>
{
    #region Inspector Variables
    public GameObject countdownPnl;
    public Text tvCountDown,tvCurrent,tvMax,tvScore,tvTime;
    public Slider timeProgress;
    public Image fillImg;
    #endregion

    #region Events
    public delegate void SimpleEvent();
    public event SimpleEvent EvtTimeOut;
    #endregion

    #region Member Variables
    int countdown = 5;
    int questionTime = 0;
    int maxQuestionTime = 10;
    int currentQuest = 0;
    int maxQuest;
    int totalTime = 0;
    int score;
    Queue<Question> listQuest;
    bool isPlaying;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        countdownPnl.SetActive(true);
        // Prepare question
        listQuest = new Queue<Question>();
        for(int i = 0; i < 10; i++)
        {
            listQuest.Enqueue(DataManager.Instance.data.GetQuestionById(i));
        }
        isPlaying = false;
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
        if (!isPlaying)
            GameManager.Instance.ChangePhase(GAME_PHASE.MENU);
        else
            EndGame();

    }
    #endregion

    #region Private Methods
    private void StartGame()
    {
        score = 0;
        tvMax.text = listQuest.Count.ToString();
        tvScore.text = score.ToString();
        currentQuest = 1;
        isPlaying = true;
        DisplayQuestion();
    }
    private void EndGame()
    {
        CancelInvoke("AddTime");
        if (EvtTimeOut != null)
            EvtTimeOut();
        // Show end game popup

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
        OneInFourPopup.Instance.DisplayQuestion(quest,null,null);
        questionTime = maxQuestionTime;
        ChangeTime();
        InvokeRepeating("AddTime", 1, 1);
    }

    private void CountdownTask()
    {
        tvCountDown.gameObject.SetActive(false);
        if(countdown>0)
            tvCountDown.text = countdown.ToString();
        else
            tvCountDown.text = "START";
        tvCountDown.gameObject.SetActive(true);
        countdown--;
        if (countdown >= -1)
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
            if (EvtTimeOut != null)
                EvtTimeOut();
            CancelInvoke("AddTime");
            currentQuest++;
            Invoke("DisplayQuestion",1);
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

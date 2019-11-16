using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABIPlugins;
using UnityEngine.UI;
using System;

public class OneInFourPopup : SingletonPopup<OneInFourPopup>
{
    #region Inspector Variables
    public Text tvQuest;
    public Image imgQuest;
    public Button btnAudioQuest;

    public Button[] buttons;
    public Text[] answers;
    public Image[] btnImgs;
    #endregion


    #region Member Variables
    int indexChoose;
    Action selectAnswerAction;
    Action<bool> checkAnswerAction;
    Action closeAction;
    #endregion

    #region Popup Methods
    public void DisplayQuestion(Question question, Action selectAnswerAction, Action<bool> checkAnswerAction,Action closeAction =null)
    {
       
        this.selectAnswerAction = selectAnswerAction;
        this.checkAnswerAction = checkAnswerAction;
        this.closeAction = closeAction;
        indexChoose = -1;

        // Hiển thị thông tin question
        tvQuest.text = question.question;
        imgQuest.enabled = false;
        btnAudioQuest.gameObject.SetActive(false);
        for(int i = 0; i<buttons.Length; i++)
        {
            if (i < question.answers.Length)
            {
                buttons[i].gameObject.SetActive(true);
                answers[i].text = question.answers[i].content;
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
            
        }
        ChangeButtonState(true);
        GameController.Instance.EvtTimeOut += ClosePopup;
        base.Show(null,false,null);
        PopupManagerAbi.Instance.transparent.color = new Color(0, 0, 0, 0);
    }

    public void ClosePopup()
    {
        GameController.Instance.EvtTimeOut -= ClosePopup;
        base.Hide(closeAction);
    }
    #endregion

    #region Public Methods
    public void SelectAnswer(int index)
    {
        indexChoose = index;
        if (selectAnswerAction != null)
            selectAnswerAction();
        //Disable all button
        ChangeButtonState(false);
        btnImgs[index].color = Color.yellow;
        
    }
    #endregion

    #region Private Methods
    private void CheckAnswer()
    {
        bool isRight = false;
        if (checkAnswerAction != null)
            checkAnswerAction(isRight);
    }

    private void ChangeButtonState(bool enable)
    {
        foreach (Button btn in buttons)
            btn.interactable = enable;
        if (enable)
        {
            foreach (Image img in btnImgs)
                img.color = Color.white;
        }
    }
    #endregion
}

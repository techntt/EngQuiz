using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    #region Inspector Variables
    public GameObject loginPopup;
    public Text tvLoading;

    // Login popup properties
    public InputField inputName;
    public Button btnLogin;
    #endregion

    #region Member Variables
    #endregion

    #region Unity Methods
    private void Awake()
    {
        tvLoading.text = ".";
        bool isLogin = PlayerPrefs.GetInt(Const.IS_LOGIN, 0) ==1;
        if (isLogin)
        {
            // get user infomation
            string player_data = PlayerPrefs.GetString(Const.USER_DATA, "");
            Player player = JsonUtility.FromJson<Player>(player_data);
            if(player != null)
            {
                PlayerData.Instance.player = player;
                // prepare data
                PrepareData();
                return;
            }
            
        }
        
        // show login popup
        loginPopup.SetActive(true);
       
    }
    #endregion

    #region Public Methods

    // Login-popup's Methods
    public void InputNameChange(string input)
    {
        // only enable login button when input is not empty
        if (input != null && !input.Equals(""))
            btnLogin.interactable = true;
        else
            btnLogin.interactable = false;
    }
    public void Login()
    {
        // Create player and store data
        PlayerData.Instance.player = new Player(inputName.text,1,0);
        PlayerPrefs.GetInt(Const.IS_LOGIN, 1);
        PlayerData.Instance.SavePlayerData();
        loginPopup.SetActive(false);
        tvLoading.text = "LOADING ...";
        // prepare data
        PrepareData();
    }
    #endregion

    #region Private Methods
    private void PrepareData()
    {
        // Refer to player's level
        DataManager.Instance.ReadQuestionFromFileData(PlayerData.Instance.player.level);
    }
    #endregion
}

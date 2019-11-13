using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Inspector Variables
    public Text tvName, tvLevel, tvEnergy, tvGold, tvScore;
    public Slider scorePrg;
    public Button btnPlay,btnSetting;
    public RectTransform popupSetting;
    #endregion

    #region Member Variables
    #endregion

    #region Unity Methods
    
    private void OnEnable()
    {
        InitUI();
    }
    #endregion

    #region Public Methods
    public void ShowSetting()
    {

    }

    public void PlayGame()
    {
        GameManager.Instance.ChangePhase(GAME_PHASE.GAME);
    }
    #endregion

    #region Private Methods
    private void InitUI()
    {
        tvName.text = PlayerData.Instance.player.name;
        tvLevel.text = "Level "+PlayerData.Instance.player.level.ToString();
        tvScore.text = PlayerData.Instance.player.score.ToString();

    }
    #endregion

}

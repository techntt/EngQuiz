using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Inspector Variables
    public GameObject loadPnl, menuPnl, gamePnl;
    #endregion

    #region Member Variables
    private GAME_PHASE phase;
    #endregion

    #region Unity Methods    
    private void Start()
    {
        ChangePhase(GAME_PHASE.LOADING);
    }
    #endregion

    #region Public Methods

    public void ChangePhase(GAME_PHASE nex_phase)
    {
        this.phase = nex_phase;
        switch (nex_phase)
        {
            case GAME_PHASE.LOADING:
                loadPnl.SetActive(true);
                menuPnl.SetActive(false);
                gamePnl.SetActive(false);
                break;
            case GAME_PHASE.MENU:
                loadPnl.SetActive(false);
                menuPnl.SetActive(true);
                gamePnl.SetActive(false);
                break;
            case GAME_PHASE.GAME:
                loadPnl.SetActive(false);
                menuPnl.SetActive(false);
                gamePnl.SetActive(true);
                break;
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
public enum GAME_PHASE
{
    LOADING,
    MENU,
    GAME
}
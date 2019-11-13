using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Inspector Variables
    #endregion

    #region Member Variables
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        
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
    #endregion
}

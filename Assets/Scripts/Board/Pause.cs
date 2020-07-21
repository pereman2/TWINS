using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    public bool GamePaused
    {
        get;
        private set;
    }
    
    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        SFXManager.Instance.ApplyLowpassEffectToMusic();
        PauseTime();
    }

    public void ClosePauseMenu()
    {
        pauseMenuUI.SetActive(false);
        SFXManager.Instance.BypassLowpassEffectToMusic();
        ResumeTime();
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
        GamePaused = true;
        SFXManager.Instance.ToggleTicTac();
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        SFXManager.Instance.ToggleTicTac();
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TWINSSceneManager : MonoBehaviour
{
    private static TWINSSceneManager instance = null;
    public static TWINSSceneManager Instance
    {
        get { return instance; }
    }

    public Animator transition;
    [SerializeField]
    private string standardGameScene;
    [SerializeField]
    private string cardGameScene;
    [SerializeField]
    private string categoryGameScene;
    [SerializeField]
    private string standardMultiplayerLevelGameScene;
    [SerializeField]
    private string LevelGameScene;
    [SerializeField]
    private string deckMenu;
    [SerializeField]
    private string mainMenu;

    void Awake()
    {
        EnsureSingleton();
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void LoadDeckMenu()
    {
        LoadScene(deckMenu);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SFXManager.Instance.PlayMainMenuMusic();
        SFXManager.Instance.BypassLowpassEffectToMusic();
        LoadScene(mainMenu);
    }

    public void StartStandardGame()
    {
        LoadScene(standardGameScene);
        SFXManager.Instance.PlayIngameMusic();
    }

    public void StartCardGame()
    {
        LoadScene(cardGameScene);
        SFXManager.Instance.PlayIngameMusic();
    }

    public void StartCategoryGame()
    {
        LoadScene(categoryGameScene);
        SFXManager.Instance.PlayIngameMusic();
    }

    public void StartLevelGame(int levelNumber)
    {
        LoadScene(LevelGameScene + levelNumber);
        SFXManager.Instance.PlayIngameMusic();
    }

    public void StartMultiplayerStandardGame()
    {
        LoadScene(standardMultiplayerLevelGameScene);
        SFXManager.Instance.PlayIngameMusic();
    }

    public void RestartActiveScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
        SFXManager.Instance.ToggleTicTac();
        SFXManager.Instance.RestartMusic();
        Time.timeScale = 1f;
    }

    public bool IsGameOnDeckCustomizationMenu()
    {
        return SceneManager.GetActiveScene().name == deckMenu;
    }

    private void LoadScene(string scene)
    {
        StartCoroutine(TransitionLoad(scene));
    }

    IEnumerator TransitionLoad(string scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private Level _currentLevel=Level.Menu;
    private GameType _currentGameType;
    

    /*
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    */
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _currentLevel = 0;
    }

    
    private void LoadScene(string sceneName)
    {
        /*
        _target = 0;
        _progressBar.fillAmount = 0;
        */
        var scene = SceneManager.LoadSceneAsync(sceneName);
        
        //TODO
        /*
        scene.allowSceneActivation = false;
        
        _loaderCanvas.SetActive(true);
        do
        {
            _target = scene.progress;
        } while (scene.progress < 0.9f);
        
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        */
    }

    /*
    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
    */
    
    
    
    public void PlayLevel(Level level)
    {
        _currentLevel = level;

        switch (level)
        {
            case Level.Menu:
                break;
            case Level.StorySuzy:
                _currentGameType = GameType.Story;
                LoadScene("SuzyS");
                break;
            case Level.StoryTullio:
                _currentGameType = GameType.Story;
                break;
            case Level.StoryTobia:
                _currentGameType = GameType.Story;
                break;
            case Level.StoryLaura:
                _currentGameType = GameType.Story;
                break;
            case Level.StoryBendy:
                _currentGameType = GameType.Story;
                break;
            case Level.ResearchSuzy:
                _currentGameType = GameType.Research;
                LoadScene("SuzyR");
                break;
            case Level.ResearchTullio:
                _currentGameType = GameType.Research;
                LoadScene("TullioR");
                break;
            case Level.ResearchTobia:
                _currentGameType = GameType.Research;
                LoadScene("TobiaR");
                break;
            case Level.ResearchLaura:
                _currentGameType = GameType.Research;
                LoadScene("LauraR");
                break;
            case Level.ResearchBendy:
                _currentGameType = GameType.Research;
                LoadScene("Bendy");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
        
    }
    
    public void PlayMainMenu()
    {
        LoadScene("MainMenu");
        _currentLevel = 0;
    }

    public GameType GetCurrentGame()
    {
        return _currentGameType;
    }
}

public enum Level
{
    Menu,
    StorySuzy,
    StoryTullio,
    StoryTobia,
    StoryLaura,
    StoryBendy,
    ResearchSuzy,
    ResearchTullio,
    ResearchTobia,
    ResearchLaura,
    ResearchBendy,
}

public enum GameType
{
    Story,
    Research,
    Exploration
}

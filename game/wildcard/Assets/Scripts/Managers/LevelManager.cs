using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    

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

        switch (level)
        {
            case Level.Menu:
                LoadScene("MainMenu");
                break;
            case Level.StorySuzy:
                LoadScene("SuzyS");
                break;
            case Level.StoryTullio:
                LoadScene("TullioS");
                break;
            case Level.StoryTobia:
                LoadScene("TobiaS");
                break;
            case Level.StoryLaura:
                LoadScene("LauraS");
                break;
            case Level.StoryBendy:
                LoadScene("BendyS");
                break;
            case Level.ResearchSuzy:
                LoadScene("SuzyR");
                break;
            case Level.ResearchTullio:
                LoadScene("TullioR");
                break;
            case Level.ResearchTobia:
                LoadScene("TobiaR");
                break;
            case Level.ResearchLaura:
                LoadScene("LauraR");
                break;
            case Level.ResearchBendy:
                LoadScene("BendyR");
                break;
            case Level.ExplorationSuzy:
                LoadScene("SuzyE");
                break;
            case Level.ExplorationTullio:
                LoadScene("TullioE");
                break;
            case Level.ExplorationTobia:
                LoadScene("TobiaE");
                break;
            case Level.ExplorationLaura:
                LoadScene("LauraE");
                break;
            case Level.ExplorationBendy:
                LoadScene("BendyE");
                break;
            case Level.StorySuzy1:
                LoadScene("SuzyS.1");
                break;
            case Level.StoryPiggy1:
                LoadScene("PiggyS.1");
                break;
            case Level.StoryTobia1:
                LoadScene("TobiaS.1");
                break;
            case Level.StoryPimpa1:
                LoadScene("PimpaS.1");
                break;
            case Level.StoryGiulio1:
                LoadScene("GiulioS.1");
                break;
            case Level.ResearchPiggy1:
                LoadScene("PiggyR1.0");
                break;
            case Level.ResearchSuzy1:
                LoadScene("SuzyR1.0");
                break;
            case Level.ResearchTobia1:
                LoadScene("TobiaR1.0");
                break;
            case Level.ResearchPimpa1:
                LoadScene("PimpaR1.0");
                break;
            case Level.ResearchGiulio1:
                LoadScene("GiulioR1.0");
                break;
            case Level.ExplorationSuzy1:
                LoadScene("SuzyE1.0");
                break;
            case Level.ExplorationTobia1:
                LoadScene("TobiaE1.0");
                break;
            case Level.ExplorationPimpa1:
                LoadScene("PimpaE1.0");
                break;
            case Level.ExplorationGiulio1:
                LoadScene("GiulioE1.0");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(level), level, null);
        }
        
    }
    
    public void PlayMainMenu()
    {
        PlayLevel(Level.Menu);
    }
}

public enum Level
{
    Menu,
    StoryPiggy1,
    StorySuzy1,
    StoryTobia1,
    StoryPimpa1,
    StoryGiulio1,
    ResearchPiggy1,
    ResearchSuzy1,
    ResearchTobia1,
    ResearchPimpa1,
    ResearchGiulio1,
    ExplorationSuzy1,
    ExplorationTobia1,
    ExplorationPimpa1,
    ExplorationGiulio1,
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
    ExplorationSuzy,
    ExplorationTullio,
    ExplorationTobia,
    ExplorationLaura,
    ExplorationBendy,
}

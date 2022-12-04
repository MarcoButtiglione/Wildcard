using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _buttonsMenu;
    [SerializeField] private GameObject _buttonsStory;
    [SerializeField] private GameObject _buttonsResearch;
    [SerializeField] private GameObject _buttonsExploration;
    [SerializeField] private GameObject _buttonsMenu2;
    [SerializeField] private GameObject _buttonsStory2;
    [SerializeField] private GameObject _buttonsResearch2;
    [SerializeField] private GameObject _buttonsExploration2;

    private void Awake()
    {
        _buttonsMenu.SetActive(true);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }

    public void ClickStory()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(true);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    public void ClickResearch()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(true);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    public void ClickExploration()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(true);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    public void ClickStory2()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(true);
    }
    public void ClickResearch2()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(true);
        _buttonsStory2.SetActive(false);
 
    }
    public void ClickExploration2()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(true);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    public void ClickMenu()
    {
        _buttonsMenu.SetActive(true);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(false);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    
    public void ClickMenu2()
    {
        _buttonsMenu.SetActive(false);
        _buttonsStory.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsExploration.SetActive(false);
        _buttonsMenu2.SetActive(true);
        _buttonsExploration2.SetActive(false);
        _buttonsResearch2.SetActive(false);
        _buttonsStory2.SetActive(false);
    }
    
    //STORY1
    public void PlayStoryPiggy1(){LevelManager.Instance.PlayLevel(Level.StoryPiggy1);}
    public void PlayStorySuzy1(){LevelManager.Instance.PlayLevel(Level.StorySuzy1);}
    public void PlayStoryTobia1(){LevelManager.Instance.PlayLevel(Level.StoryTobia1);}
    public void PlayStoryGiulio1(){LevelManager.Instance.PlayLevel(Level.StoryGiulio1);}
    public void PlayStoryPimpa1(){LevelManager.Instance.PlayLevel(Level.StoryPimpa1);}
    
    public void PlayResearchPiggy1(){LevelManager.Instance.PlayLevel(Level.ResearchPiggy1);}
    public void PlayResearchSuzy1(){LevelManager.Instance.PlayLevel(Level.ResearchSuzy1);}
    public void PlayResearchTobia1(){LevelManager.Instance.PlayLevel(Level.ResearchTobia1);}
    public void PlayResearchGiulio1(){LevelManager.Instance.PlayLevel(Level.ResearchGiulio1);}
    public void PlayResearchPimpa1(){LevelManager.Instance.PlayLevel(Level.ResearchPimpa1);}
    
    public void PlayExplorationSuzy1(){LevelManager.Instance.PlayLevel(Level.ExplorationSuzy1);}
    public void PlayExplorationTobia1(){LevelManager.Instance.PlayLevel(Level.ExplorationTobia1);}
    public void PlayExplorationGiulio1(){LevelManager.Instance.PlayLevel(Level.ExplorationGiulio1);}
    public void PlayExplorationPimpa1(){LevelManager.Instance.PlayLevel(Level.ExplorationPimpa1);}
    
    //RESEARCH2
    public void PlayResearchSuzy()
    {
        LevelManager.Instance.PlayLevel(Level.ResearchSuzy);
    }
    public void PlayResearchTullio()
    {
        LevelManager.Instance.PlayLevel(Level.ResearchTullio);
    }
    public void PlayResearchTobia()
    {
        LevelManager.Instance.PlayLevel(Level.ResearchTobia);
    }
    public void PlayResearchLaura()
    { 
        LevelManager.Instance.PlayLevel(Level.ResearchLaura);
    }
    public void PlayResearchBendy()
    { 
        LevelManager.Instance.PlayLevel(Level.ResearchBendy);
    }
    //STORY2
    public void PlayStorySuzy()
    {
        LevelManager.Instance.PlayLevel(Level.StorySuzy);
    }
    public void PlayStoryTullio()
    {
        LevelManager.Instance.PlayLevel(Level.StoryTullio);
    }
    public void PlayStoryTobia()
    {
        LevelManager.Instance.PlayLevel(Level.StoryTobia);
    }
    public void PlayStoryLaura()
    { 
        LevelManager.Instance.PlayLevel(Level.StoryLaura);
    }
    public void PlayStoryBendy()
    { 
        LevelManager.Instance.PlayLevel(Level.StoryBendy);
    }
    //EXPLORATION2
    public void PlayExplorationSuzy()
    {
        LevelManager.Instance.PlayLevel(Level.ExplorationSuzy);
    }
    public void PlayExplorationTullio()
    {
        LevelManager.Instance.PlayLevel(Level.ExplorationTullio);
    }
    public void PlayExplorationTobia()
    {
        LevelManager.Instance.PlayLevel(Level.ExplorationTobia);
    }
    public void PlayExplorationLaura()
    { 
        LevelManager.Instance.PlayLevel(Level.ExplorationLaura);
    }
    public void PlayExplorationBendy()
    { 
        LevelManager.Instance.PlayLevel(Level.ExplorationBendy);
    }
}

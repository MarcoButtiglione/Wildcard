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

    private void Awake()
    {
        _buttonsExploration.SetActive(false);
        _buttonsMenu.SetActive(true);
        _buttonsResearch.SetActive(false);
        _buttonsStory.SetActive(false);
    }

    public void ClickStory()
    {
        _buttonsExploration.SetActive(false);
        _buttonsMenu.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsStory.SetActive(true);
    }
    public void ClickResearch()
    {
        _buttonsExploration.SetActive(false);
        _buttonsMenu.SetActive(false);
        _buttonsResearch.SetActive(true);
        _buttonsStory.SetActive(false);
    }
    public void ClickExploration()
    {
        _buttonsExploration.SetActive(true);
        _buttonsMenu.SetActive(false);
        _buttonsResearch.SetActive(false);
        _buttonsStory.SetActive(false);
    }
    public void ClickReturn()
    {
        _buttonsExploration.SetActive(false);
        _buttonsMenu.SetActive(true);
        _buttonsResearch.SetActive(false);
        _buttonsStory.SetActive(false);
    }
    
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
}

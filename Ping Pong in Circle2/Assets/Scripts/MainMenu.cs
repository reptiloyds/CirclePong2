using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject selectLevelPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot inMenu;
    [SerializeField] private Button[] selectLevelButtons;
    [SerializeField] private Sprite activeButton;
    [SerializeField] private Text globalStarCountText;
    [SerializeField] private SaveLoadManager slManager;
    public event EventHandler<ChangeSkinEventArgs> ChangeSkin;
    public static MainMenu instance { get; private set;}
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        globalStarCountText.text = PlayerPrefs.GetInt("GlobalStarCount").ToString();


        if (!PlayerPrefs.HasKey("availableLevels"))
        {
            PlayerPrefs.SetInt("availableLevels", 1);
        }
        for(int i = 0; i < PlayerPrefs.GetInt("availableLevels"); i++)
        {
            try
            {
                selectLevelButtons[i].interactable = true;
                selectLevelButtons[i].image.sprite = activeButton;
            }
            catch (Exception)
            {

            }
        }

    }

    public void OpenLevelPanel()
    {
        inMenu.TransitionTo(0.4f);
        selectLevelPanel.SetActive(true);
    }

    public void CloseLevelPanel()
    {
        normal.TransitionTo(0.4f);
        selectLevelPanel.SetActive(false);
    }
    public void OpenOption()
    {
        inMenu.TransitionTo(0.4f);
        optionPanel.SetActive(true);
    }
    public void CloseOption()
    {
        normal.TransitionTo(0.4f);
        optionPanel.SetActive(false);
    }
    public void SelectLevel(int numberOfLevel)
    {
        StartCoroutine(LoadLevel(numberOfLevel));
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OpenShop()
    {
        inMenu.TransitionTo(0.4f);
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        normal.TransitionTo(0.4f);
        shopPanel.SetActive(false);
    }

    public void SelectBall(int nameOfBall)
    {
        var changeSkin = new ChangeSkinEventArgs { CurrentSkin = nameOfBall };
        ChangeSkin?.Invoke(this, changeSkin);
    }

    private IEnumerator LoadLevel(int numberOfLevel)
    {
        normal.TransitionTo(0.4f);
        cameraAnimator.SetTrigger("darker");
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(numberOfLevel);
    }

    
}
public class ChangeSkinEventArgs : EventArgs
{
    public int CurrentSkin { get; set; }
}
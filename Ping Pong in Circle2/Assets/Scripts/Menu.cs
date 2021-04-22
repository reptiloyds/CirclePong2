using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator pausePanelAnimator;
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    [SerializeField] private Animator[] startsAnimator;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private AudioMixerGroup master;
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot inMenu;
    [SerializeField] public int playerHealthPoint;
    [SerializeField] private Animator starTextAnimator;
    [SerializeField] private Text winMenuStarText;
    [SerializeField] private Text menuStarText;
    [SerializeField] private Text loseMenuStarText;

    private void Start()
    {
        EventAggregator.instance.WinGame += OnWin;
        EventAggregator.instance.LoseGame += OnLose;
        EventAggregator.instance.GetDamage += OnGetDamage;

        winMenuStarText.text = PlayerPrefs.GetInt("GlobalStarCount").ToString();
        loseMenuStarText.text = PlayerPrefs.GetInt("GlobalStarCount").ToString();
        menuStarText.text = PlayerPrefs.GetInt("GlobalStarCount").ToString();
    }
    private void OnLose()
    {
        losePanel.SetActive(true);
        inMenu.TransitionTo(0.4f);
        EventAggregator.instance.LoseGame -= OnLose;
    }

    private void OnWin(object sender, WinGameEventArgs e)
    {
        StartCoroutine(WinGameCoroutine());
        EventAggregator.instance.WinGame -= OnWin;
    }

    private void AddAvailableLevel()
    {
        if (!PlayerPrefs.HasKey("availableLevels")) PlayerPrefs.SetInt("availableLevels", 1);
        PlayerPrefs.SetInt("availableLevels", PlayerPrefs.GetInt("availableLevels") + 1);
    }

    private void SaveStarData()
    {
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "StarCount"))
        {
            AddAvailableLevel();
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "StarCount", playerHealthPoint);
        }
        else if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "StarCount") < playerHealthPoint)
        {
            var currentStarCount = PlayerPrefs.GetInt("GlobalStarCount") + playerHealthPoint - PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "StarCount");
            PlayerPrefs.SetInt("GlobalStarCount", currentStarCount);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "StarCount", playerHealthPoint);
        }
    }

    private void IncreaseGlobalStarCount(int currentNumberOfStar)
    {
        if (currentNumberOfStar + 1 > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "StarCount"))
        {
            PlayerPrefs.SetInt("GlobalStarCount", PlayerPrefs.GetInt("GlobalStarCount") + 1);
            winMenuStarText.text = PlayerPrefs.GetInt("GlobalStarCount").ToString();
            starTextAnimator.SetTrigger("Increase");
        }
        
    }

    private void OnGetDamage()
    {
        playerHealthPoint--;
        StartCoroutine(DestroyHeart());
        cameraAnimator.SetTrigger("TakeDamage");
        if (playerHealthPoint == 0)
        {
            EventAggregator.instance.GetDamage -= OnGetDamage;
            EventAggregator.instance.OnLoseGame();
        }
    }
    public void Pause()
    {
        inMenu.TransitionTo(0.4f);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitPause()
    {
        normal.TransitionTo(0.4f);
        Time.timeScale = 1f;
        pausePanelAnimator.SetTrigger("closeMenu");
        pausePanel.SetActive(false);
    }
    public void LoadNextLevel(int nextLevel)
    {
        StartTime();
        StartCoroutine(LoadSceneCoroutine(nextLevel));
    }
    public void RestartGame()
    {
        cameraAnimator.SetTrigger("darker");
        StartTime();
        StartCoroutine(RestartLevelCoroutine());
    }

    public void QuitGame()
    {
        StartTime();
        StartCoroutine(LoadSceneCoroutine(0));
    }

    

    private IEnumerator DestroyHeart()
    {
        hearts[playerHealthPoint].GetComponent<Animator>().SetTrigger("Damage");
        yield return new WaitForSeconds(0.4f);
        Destroy(hearts[playerHealthPoint]);
    }
    private IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        normal.TransitionTo(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WinGameCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        winPanel.SetActive(true);
        inMenu.TransitionTo(0.4f);
        yield return new WaitForSeconds(0.5f);
        for(int i  = 0; i < playerHealthPoint; i++)
        {
            IncreaseGlobalStarCount(i);
            startsAnimator[i].SetTrigger("Win");
            yield return new WaitForSeconds(0.4f);
        }

        SaveStarData();
    }
    private IEnumerator LoadSceneCoroutine(int scene)
    {
        normal.TransitionTo(0.4f);
        cameraAnimator.SetTrigger("darker");
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(scene);
    }

    private void StartTime()
    {
        Time.timeScale = 1f;
        ballRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}

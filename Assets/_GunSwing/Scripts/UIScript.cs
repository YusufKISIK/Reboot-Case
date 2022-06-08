using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;
using DG.Tweening;

public class UIScript : MonoBehaviour
{
 public static int currentLevel;
    public static int totalCoins;
    public static bool completedTutorial = false;
    int levelReward;

    [SerializeField] GameObject[] _levelPlatforms;
    public static int currentLevelText;
    [SerializeField]
    Dictionary<string, GameObject> Instructions;
    [SerializeField]
    GameObject endUI;
    [SerializeField]
    GameObject HUD;   
    
    [SerializeField]
    TextMeshProUGUI levelTextHUD;
    [SerializeField]
    TextMeshProUGUI levelTextEndUI;
    [SerializeField]
    TextMeshProUGUI levelRewardText;
    [SerializeField]
    TextMeshProUGUI multiplierText;
    [SerializeField]
    TextMeshProUGUI successText;
    public //Used in PlayerInteractable
    TextMeshProUGUI itemCountText;
    [SerializeField]
    TextMeshProUGUI itemTotalText;

    [SerializeField]
    GameObject joystickTutorial;
    
    [SerializeField]
    Image blackout;
    [SerializeField]
    GameObject hiddenUI;
    
    [SerializeField]
    GameObject playAgainButton;
    [SerializeField]
    GameObject nextButton;
    
    [SerializeField]
    GameObject rewardCoinGroup;

    InputField levelInput;
    int hiddenMenuCount;   

    private void Awake()
    {
        currentLevel = GameManager.Instance.currentLevel;
        currentLevelText = GameManager.Instance.currentLevelText;
        //totalCoins = GameManager.Instance.totalCoins;
        completedTutorial = GameManager.Instance.completedTutorial;
        levelInput = transform.GetChild(5).GetChild(1).GetComponent<InputField>();
        hiddenMenuCount = 0;
        //Debug.Log("CurrentLevel: " + currentLevel);
    }
    // Start is called before the first frame update
    void Start()
    {
        levelReward = UnityEngine.Random.Range(20, 50);
        //levelRewardText.text = levelReward.ToString();
        levelTextHUD.text = "LEVEL " + currentLevelText;
        levelTextEndUI.text = levelTextHUD.text;
        //totalCoinsText.text = totalCoins.ToString();
        hiddenMenuCount = 0;
        _levelPlatforms[currentLevel - 1].SetActive(true);
        // Instantiate(_levelPlatforms[currentLevel - 1]);
        Debug.Log("currentLevel%5<0" + currentLevel % 5);
        Debug.Log("Current" + currentLevel);
     
    }
    public void Success(int multiplier)
    {
        if (!completedTutorial)
        {
            completedTutorial = true;
        }
        playAgainButton.SetActive(false);
        nextButton.SetActive(true);
        rewardCoinGroup.SetActive(true);
        successText.text = "SUCCESS";
        multiplierText.text = "x" + multiplier;
        endUI.SetActive(true);
        HUD.SetActive(false);
        StartCoroutine(IncreaseGold(multiplier));
    }
    public void Fail()
    {       
        playAgainButton.SetActive(true);
        nextButton.SetActive(false);
        rewardCoinGroup.SetActive(false);
        successText.text = "TRY AGAIN";
        HUD.SetActive(false);
        endUI.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void NextScene()
    {       
        if (currentLevel == _levelPlatforms.Length)
        {
            currentLevel = 0;
           // GameManager.Instance.currentLevel = 0;           
           // GameManager.Instance.SaveState();
        }
       
        nextButton.SetActive(false);
       // GameManager.Instance.currentLevel++;
       // GameManager.Instance.currentLevelText++;
       // GameManager.Instance.completedTutorial = completedTutorial;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;

        for (int i = 0; i < _levelPlatforms.Length; i++)
        {
            if (i == (currentLevel - 1))
            {
                _levelPlatforms[i].SetActive(true);
                Debug.Log("LevelPaltfrom" + _levelPlatforms.Length);
            }
        }
    }
    public IEnumerator IncreaseGold(int multiplier)
    {
        //TODO: Multiply the level reward
        yield return new WaitForSeconds(0.5f);
        int total = totalCoins;

        totalCoins += levelReward * multiplier; //make sure reward is fully applied;

        var rewardDiff = levelReward * (multiplier - 1);

        while (rewardDiff > 0)
        {
            var diff = Math.Min(9, rewardDiff);
            rewardDiff -= diff;
            levelReward += diff;
            //levelRewardText.text = levelReward.ToString();
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        var count = levelReward;
        while (count > 0)
        {
            var diff = Math.Min(3, count);
            count -= diff;
            total += diff;
            //totalCoinsText.text = total.ToString();
            yield return null;
        }
    }
    public void DisableJoystickTutorial()
    {
        joystickTutorial.SetActive(false);
    }
    public void EnableJoystickTutorial()
    {
        joystickTutorial.SetActive(true);
    }
 
    public void hiddenMenuPress()
    {
        StartCoroutine(HiddenMenuAction());
    }
    public void SkipToLevel()
    {
        currentLevel = Int16.Parse(levelInput.text) - 1;
        NextScene();
    }
    IEnumerator HiddenMenuAction()
    {
        hiddenMenuCount++;
        if (hiddenMenuCount == 3)
        {
            hiddenUI.SetActive(true);
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(1);
            hiddenMenuCount--;
        }
    }

}

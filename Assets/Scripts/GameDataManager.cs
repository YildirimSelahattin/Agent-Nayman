using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public int playSound;
    public int playMusic;
    public int playVibrate;
    public int levelToLoad;
    public int money = 0;
    public int currentGun;
    public int playerHealth;
    public int playerShield;
    public int playerHealthLevel;
    public int playerShieldLevel;
    public int playerShieldUpgradeStartMoney;
    public int playerHealthUpgradeStartMoney;
    public float playerHealthUpgradeIncreasePercent;
    public float playerShieldUpgradeIncreasePercent;
    // Start is called before the first frame update

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        LoadData();
    }
    void Start()
    {

    }
    public void LoadData()
    {
        levelToLoad = PlayerPrefs.GetInt("LevelToLoad", 1);
        playSound = PlayerPrefs.GetInt("PlaySoundKey", 1);
        playMusic = PlayerPrefs.GetInt("PlayMusicKey", 1);
        playVibrate = PlayerPrefs.GetInt("PlayVibrateKey", 1);
        currentGun = PlayerPrefs.GetInt("CurrentGun",0);
        playerHealth = PlayerPrefs.GetInt("PLayerHealth",100);
        playerShield = PlayerPrefs.GetInt("PlayerShield", 0);
        playerHealthLevel = PlayerPrefs.GetInt("PlayerHealthLevel",0);
        playerShieldLevel = PlayerPrefs.GetInt("PlayerShieldLevel",0);
        playerHealth = (int)(playerHealth * Mathf.Pow(1+0.20f,playerHealthLevel));
        playerShield = (int)(playerShield * Mathf.Pow(1+0.20f,playerShieldLevel));
    }
  


 
    public void SaveData()
    {
        PlayerPrefs.SetInt("LevelToLoad", levelToLoad);
        PlayerPrefs.SetInt("PlaySoundKey", playSound);
        PlayerPrefs.SetInt("PlayMusicKey", playMusic);
        PlayerPrefs.SetInt("PlayVibrateKey", playVibrate);
        playerHealth = PlayerPrefs.GetInt("PLayerHealth", 100);
        playerShield = PlayerPrefs.GetInt("PlayerShield", 0);
        playerHealthLevel = PlayerPrefs.GetInt("PlayerHealthLevel", 0);
        playerShieldLevel = PlayerPrefs.GetInt("PlayerShieldLevel", 0);
    }
}
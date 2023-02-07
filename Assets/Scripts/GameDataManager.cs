using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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
    }
  


 
    public void SaveData()
    {
        PlayerPrefs.SetInt("LevelToLoad", levelToLoad);
        PlayerPrefs.SetInt("PlaySoundKey", playSound);
        PlayerPrefs.SetInt("PlayMusicKey", playMusic);
        PlayerPrefs.SetInt("PlayVibrateKey", playVibrate);
    }
}
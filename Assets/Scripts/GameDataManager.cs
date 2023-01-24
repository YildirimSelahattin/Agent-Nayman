using UnityEngine;
using UnityEngine.UI;

//THE ONLY DATA READER , READS FROM JSONTEXT
public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public int playSound;
    public int playMusic;
    public int playVibrate;
    public AudioClip brushMachineMusic;
    public GameObject[] moneyMachineArray;
    public int[] gridArray = new int[6];
    public int maxLevelMachineAmount;
    public float beltSpeedButtonMoney;
    public float[] gridOpenWithMoneyPrices;

    

    



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playSound = PlayerPrefs.GetInt("PlaySoundKey",1);
            playMusic = PlayerPrefs.GetInt("PlayMusicKey",1);
            playVibrate = PlayerPrefs.GetInt("PlayVibrateKey",1);

           
        }

        LoadData();
    }

    public void LoadData()
    {
    }

       
        
    public void SaveData()
    {
        
    }

    private void OnDisable()
    {
        
        SaveData();
        PlayerPrefs.SetInt("PlaySoundKey", playSound);
        PlayerPrefs.SetInt("PlayMusicKey", playMusic);
        PlayerPrefs.SetInt("PlayVibrateKey",playVibrate);
    }

    
            
        
    }

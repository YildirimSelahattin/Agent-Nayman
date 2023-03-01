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
    int totalMoney;


    // Ters orantý bu artarsa rate azalýyo
    public float[] baseFireRate = new float[3];
    public int[] fireRateLevel = new int[3];
    public int[] damageLevel = new int[3];
    public float[] BulletDamage = new float[3];
    public float[] fireRateIncreasePercentagePerLevel = new float[3];
    public float[] damageIncreasePercentagePerLevel = new float[3];
    public float[] fireRateUpgradeStartMoney = new float[3];
    public float[] fireRateCostIncreasePercentage = new float[3];
    public float[] damageUpgradeStartMoney = new float[3];
    public float[] damageCostIncreasePercentage = new float[3];
    public ShootingConfig[] gunConfigsArray;
    //Duration artarsa daha yavaþ gidiyor 
    public float[] BulletDuration;
    public float[] Recoil;
    public bool[] isAvaliable;

    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            this.totalMoney = value;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.totalMoneyText.text = value.ToString() + " $";
                PlayerPrefs.SetInt("TotalMoney", totalMoney);
            }
        }
    }
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
        TotalMoney = PlayerPrefs.GetInt("TotalMoney", 100);
        levelToLoad = PlayerPrefs.GetInt("LevelToLoad", 10);
        playSound = PlayerPrefs.GetInt("PlaySoundKey", 1);
        playMusic = PlayerPrefs.GetInt("PlayMusicKey", 1);
        playVibrate = PlayerPrefs.GetInt("PlayVibrateKey", 1);
        currentGun = PlayerPrefs.GetInt("CurrentGun", 0);
        playerHealth = PlayerPrefs.GetInt("PLayerHealth", 100);
        playerShield = PlayerPrefs.GetInt("PlayerShield", 0);
        playerHealthLevel = PlayerPrefs.GetInt("PlayerHealthLevel", 0);
        playerShieldLevel = PlayerPrefs.GetInt("PlayerShieldLevel", 0);
        playerHealth = (int)(playerHealth * Mathf.Pow(1 + 0.20f, playerHealthLevel));
        playerShield = (int)(playerShield * Mathf.Pow(1 + 0.20f, playerShieldLevel));

        for (int i = 0; i < 3; i++)
        {
            gunConfigsArray[i].baseFireRate = PlayerPrefs.GetFloat("baseFireRategun" + i, baseFireRate[i]);
            gunConfigsArray[i].fireRateLevel = PlayerPrefs.GetInt("fireRateLevelgun" + i, fireRateLevel[i]);
            gunConfigsArray[i].damageLevel = PlayerPrefs.GetInt("damageLevelgun" + i, damageLevel[i]);
            gunConfigsArray[i].BulletDamage = PlayerPrefs.GetFloat("BulletDamagegun" + i, BulletDamage[i]);
            gunConfigsArray[i].fireRateIncreasePercentagePerLevel = PlayerPrefs.GetFloat("fireRateIncreasePercentagePerLevelgun" + i, fireRateIncreasePercentagePerLevel[i]);
            gunConfigsArray[i].damageIncreasePercentagePerLevel = PlayerPrefs.GetFloat("damageIncreasePercentagePerLevelgun" + i, damageIncreasePercentagePerLevel[i]);
            gunConfigsArray[i].fireRateUpgradeStartMoney = PlayerPrefs.GetFloat("fireRateUpgradeStartMoneygun" + i, fireRateUpgradeStartMoney[i]);
            gunConfigsArray[i].damageUpgradeStartMoney = PlayerPrefs.GetFloat("damageUpgradeStartMoneygun" + i, damageUpgradeStartMoney[i]);
            gunConfigsArray[i].fireRateCostIncreasePercentage = PlayerPrefs.GetFloat("fireRateCostIncreasePercentagegun" + i, fireRateCostIncreasePercentage[i]);
            gunConfigsArray[i].damageCostIncreasePercentage = PlayerPrefs.GetFloat("damageCostIncreasePercentagegun" + i, damageCostIncreasePercentage[i]);
            gunConfigsArray[i].BulletDuration = PlayerPrefs.GetFloat("BulletDurationgun" + i, BulletDuration[i]);
            gunConfigsArray[i].Recoil = PlayerPrefs.GetFloat("Recoilgun" + i, Recoil[i]);
            if(i == 0)
            {
                gunConfigsArray[i].isAvaliable = true;
            }
            else
            {
                int temp = PlayerPrefs.GetInt("isAvaliablegun" + i, 0);
                if (temp == 0)
                {

                    gunConfigsArray[i].isAvaliable = false;
                }
                else
                {
                    gunConfigsArray[i].isAvaliable = true;
                }
            }
           
        }
    }




    public void SaveData()
    {
        PlayerPrefs.SetInt("TotalMoney", TotalMoney);
        PlayerPrefs.SetInt("LevelToLoad", levelToLoad);
        PlayerPrefs.SetInt("PlaySoundKey", playSound);
        PlayerPrefs.SetInt("PlayMusicKey", playMusic);
        PlayerPrefs.SetInt("PlayVibrateKey", playVibrate);
        PlayerPrefs.GetInt("PLayerHealth", playerHealth);
        PlayerPrefs.GetInt("PlayerShield", playerShield);
        PlayerPrefs.GetInt("PlayerHealthLevel", playerHealthLevel);
        PlayerPrefs.GetInt("PlayerShieldLevel", playerShieldLevel);


        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetFloat("baseFireRategun" + i, gunConfigsArray[i].baseFireRate);
            PlayerPrefs.SetInt("fireRateLevelgun" + i, gunConfigsArray[i].fireRateLevel);
            PlayerPrefs.SetInt("damageLevelgun" + i, gunConfigsArray[i].damageLevel);
            PlayerPrefs.SetFloat("BulletDamagegun" + i, gunConfigsArray[i].BulletDamage);
            PlayerPrefs.SetFloat("fireRateIncreasePercentagePerLevelgun" + i, gunConfigsArray[i].fireRateIncreasePercentagePerLevel);
            PlayerPrefs.SetFloat("damageIncreasePercentagePerLevelgun" + i, gunConfigsArray[i].damageIncreasePercentagePerLevel);
            PlayerPrefs.SetFloat("fireRateUpgradeStartMoneygun" + i, gunConfigsArray[i].fireRateUpgradeStartMoney);
            PlayerPrefs.SetFloat("damageUpgradeStartMoneygun" + i, gunConfigsArray[i].damageUpgradeStartMoney);
            PlayerPrefs.SetFloat("fireRateCostIncreasePercentagegun" + i, gunConfigsArray[i].fireRateCostIncreasePercentage);
            PlayerPrefs.SetFloat("damageCostIncreasePercentagegun" + i, gunConfigsArray[i].damageCostIncreasePercentage);
            PlayerPrefs.SetFloat("BulletDurationgun" + i, gunConfigsArray[i].BulletDuration);
            PlayerPrefs.SetFloat("Recoilgun" + i, gunConfigsArray[i].Recoil);
            if (gunConfigsArray[i].isAvaliable == false)
            {
                PlayerPrefs.SetInt("isAvaliablegun" + i, 0);
            }
            else
            {
                PlayerPrefs.SetInt("isAvaliablegun" + i, 1);
            }
        }
    }
}
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    //yuvarlaklar closed gelecek
    // Start is called before the first frame update
    [SerializeField] GameObject[] fireRateLevelCirclesParent;
    [SerializeField] GameObject[] damageLevelCirclesParent;
    [SerializeField] GameObject healthLevelCirclesParent;
    [SerializeField] GameObject shieldLevelCirclesParent;
    [SerializeField] GameObject[] fireButtonParents;
    [SerializeField] GameObject[] damageButtonParents;
    [SerializeField] Button healthUpgradeButton;
    [SerializeField] Button shieldUpgradeButton;

    [SerializeField] GameObject verticalLayoutGroup;
    [SerializeField] GameObject[] closedImages;
    [SerializeField] Button[] FireRateButtons;
    [SerializeField] Button[] damageButtons;
    [SerializeField] GameObject[] selectImages;
    [SerializeField] GameObject avatarViewport;
    [SerializeField] GameObject weaponViewport;
    [SerializeField] GameObject weaponButton;
    [SerializeField] GameObject avatarButton;
    [SerializeField] TextMeshProUGUI[] fireRateTexts;
    [SerializeField] TextMeshProUGUI[] damageTexts;
    [SerializeField] GameObject[] infoTexts;
    [SerializeField] TextMeshProUGUI[] fireMoneyTexts;
    [SerializeField] TextMeshProUGUI[] damageMoneyTexts;

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI healthMoneyText;
    [SerializeField] TextMeshProUGUI shieldText;
    [SerializeField] TextMeshProUGUI shieldMoneyText;

    public Sprite openedSprite;
    public Sprite closedSprite;
    public PlayerManager playerManager;
    public float sizeOfGunPanel = 1000;
    public ShootingConfig[] gunConfigsArray;
    public int curPanelGun;
    public GameObject bottomSide;
    public GameObject rightArrow;
    public GameObject leftArrow;


    void Start()
    {

        verticalLayoutGroup.transform.DOLocalMoveY(0, 0.3f);
        verticalLayoutGroup.transform.DOLocalMoveX(-1*GameDataManager.Instance.currentGun * sizeOfGunPanel, 0.3f);
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        curPanelGun = GameDataManager.Instance.currentGun;
        selectImages[curPanelGun].SetActive(true);
        EditCurrentGunPanel();
        EditCurrentAvatarPanel();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void onOpenBottomSideButtonClicked()
    {
        if (bottomSide.active == true)
        {
            bottomSide.SetActive(false);
        }
        else
        {
            bottomSide.SetActive(true);
            EditCurrentAvatarPanel();
            EditCurrentGunPanel();
        }
    }
    public void onRightArrowButtonClicked()
    {
        selectImages[curPanelGun].SetActive(false);
        curPanelGun++;
        EditCurrentGunPanel();
        verticalLayoutGroup.transform.DOLocalMoveX(-1 * curPanelGun * sizeOfGunPanel, 1).OnComplete(() =>
        {
            if (gunConfigsArray[curPanelGun].isAvaliable == true)
            {
                selectImages[curPanelGun].SetActive(true);
                GunManager.Instance.Gun = (GunTypes)curPanelGun;
                GameDataManager.Instance.currentGun = curPanelGun;
                GunManager.Instance.SpawnGun(GunManager.Instance.Gun);

            }
            else
            {
                FireRateButtons[curPanelGun].interactable = false;
                damageButtons[curPanelGun].interactable = false;

            }
        });

    }
    public void onLeftArrowButtonClicked()
    {
        selectImages[curPanelGun].SetActive(false);
        curPanelGun--;
        EditCurrentGunPanel();
        verticalLayoutGroup.transform.DOLocalMoveX(-1 * curPanelGun * sizeOfGunPanel, 1).OnComplete(() =>
        {
            if (gunConfigsArray[curPanelGun].isAvaliable == true)
            {
                selectImages[curPanelGun].SetActive(true);
                GunManager.Instance.Gun = (GunTypes)curPanelGun;
                GameDataManager.Instance.currentGun = curPanelGun;
                GunManager.Instance.SpawnGun(GunManager.Instance.Gun);

            }
            else
            {
                FireRateButtons[curPanelGun].interactable = false;
                damageButtons[curPanelGun].interactable = false;

            }
        });

    }

    public void EditCurrentGunPanel()
    {
        if (weaponViewport.active)//if we are on gun panel side 
        {
            if (curPanelGun != 0)
            {
                fireButtonParents[curPanelGun].SetActive(gunConfigsArray[curPanelGun].isAvaliable);
                damageButtonParents[curPanelGun].SetActive(gunConfigsArray[curPanelGun].isAvaliable);
                infoTexts[curPanelGun].SetActive(!gunConfigsArray[curPanelGun].isAvaliable);
            }
            if ((int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel)) > GameDataManager.Instance.TotalMoney)
            {
                FireRateButtons[curPanelGun].interactable = false;
            }
            if ((int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel)) > GameDataManager.Instance.TotalMoney)
            {
                damageButtons[curPanelGun].interactable = false;
            }


            int fireRateLevel = gunConfigsArray[curPanelGun].fireRateLevel;
            int damageLevel = gunConfigsArray[curPanelGun].damageLevel;
            if (fireRateLevel >= 5)
            {
                FireRateButtons[curPanelGun].interactable = false;
                fireMoneyTexts[curPanelGun].text = "MAX";
            }
            else
            {
                int fireRateButtonMoney = (int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * (Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel)));
                fireMoneyTexts[curPanelGun].text = fireRateButtonMoney.ToString() + "$";
            }
            if (damageLevel >= 5)
            {
                damageButtons[curPanelGun].interactable = false;
                damageMoneyTexts[curPanelGun].text = "MAX";
            }
            else
            {
                int damageButtonMoney = (int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * (Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel)));
                damageMoneyTexts[curPanelGun].text = damageButtonMoney.ToString() + "$";
            }
            //fireRateCircles
            for (int i = 0; i < fireRateLevel; i++)
            {
                fireRateLevelCirclesParent[curPanelGun].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
            }
            for (int i = 0; i < damageLevel; i++)
            {
                damageLevelCirclesParent[curPanelGun].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
            }
            float currentFireRate = gunConfigsArray[curPanelGun].baseFireRate * Mathf.Pow(1 - gunConfigsArray[curPanelGun].fireRateIncreasePercentagePerLevel, gunConfigsArray[curPanelGun].fireRateLevel);
            fireRateTexts[curPanelGun].text = String.Format("{0:0.0}", currentFireRate);

            int currentDamage = (int)(gunConfigsArray[curPanelGun].BulletDamage * (Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageIncreasePercentagePerLevel, gunConfigsArray[curPanelGun].damageLevel)));
            damageTexts[curPanelGun].text = currentDamage.ToString();



        }

        if (curPanelGun == 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else if (curPanelGun == 2)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
        }
        else
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
        }
        GameDataManager.Instance.SaveData();
    }

    public void EditCurrentAvatarPanel()
    {
        int healthlevel = GameDataManager.Instance.playerHealthLevel;
        int shieldLevel = GameDataManager.Instance.playerShieldLevel;

        if ((int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel)) > GameDataManager.Instance.TotalMoney)
        {
            healthUpgradeButton.interactable = false;
        }
        if ((int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel)) > GameDataManager.Instance.TotalMoney)
        {
            shieldUpgradeButton.interactable = false;
        }
        if (healthlevel == 5)
        {
            healthUpgradeButton.interactable = false;
            healthMoneyText.text = "MAX";
        }
        else
        {
            int currentHealthMoney = (int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel));
            healthMoneyText.text = currentHealthMoney.ToString() + "$";
        }

        int currentShield = GameDataManager.Instance.playerShield;
        shieldText.text = currentShield.ToString();
        if (shieldLevel == 5)
        {
            shieldUpgradeButton.interactable = false;
            shieldMoneyText.text = "MAX";
        }
        else
        {
            int currentShieldMoney = (int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel));
            shieldMoneyText.text = currentShieldMoney.ToString() + "$";
        }

        for (int i = 0; i < healthlevel; i++)
        {
            healthUpgradeButton.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
        }
        for (int i = 0; i < shieldLevel; i++)
        {
            shieldUpgradeButton.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
        }
        int currentHealth = GameDataManager.Instance.playerHealth;
        healthText.text = currentHealth.ToString();

        GameDataManager.Instance.SaveData();
    }
    public void OnUpgradeFireRateClicked()
    {

        if ((int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel)) <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.TotalMoney -= (int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel));
            //increase fire RateLevel
            gunConfigsArray[curPanelGun].fireRateLevel++;
            fireRateTexts[curPanelGun].text = String.Format("{0:0.0}", GunManager.Instance.EditCurrentFireRate());

            int fireRateButtonMoney = (int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel));
            fireRateLevelCirclesParent[curPanelGun].transform.GetChild(gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel - 1).gameObject.GetComponent<Image>().sprite = openedSprite;

            if (gunConfigsArray[curPanelGun].fireRateLevel == 5)
            {
                FireRateButtons[curPanelGun].interactable = false;
                fireMoneyTexts[curPanelGun].text = "MAX";
                ControllIsUpgradesFinished();
            }
            else
            {
                fireMoneyTexts[curPanelGun].text = fireRateButtonMoney.ToString() + "$";
            }

            GameDataManager.Instance.SaveData();
            EditCurrentGunPanel();
        }
    }
    public void OnUpgradeDamageClicked()
    {
        //increase fire RateLevel
        if ((int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel)) <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.TotalMoney -= (int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel));
            gunConfigsArray[curPanelGun].damageLevel++;
            damageTexts[curPanelGun].text = GunManager.Instance.EditCurrentDamage().ToString();
            int damageButtonMoney = (int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel));
            Debug.Log(damageButtonMoney + "qwe");
            damageLevelCirclesParent[curPanelGun].transform.GetChild(gunConfigsArray[curPanelGun].damageLevel - 1).gameObject.GetComponent<Image>().sprite = openedSprite;
            if (gunConfigsArray[curPanelGun].damageLevel == 5)
            {
                damageButtons[curPanelGun].interactable = false;
                damageMoneyTexts[curPanelGun].text = "MAX";
                ControllIsUpgradesFinished();
            }
            else
            {
                damageMoneyTexts[curPanelGun].text = damageButtonMoney.ToString() + "$";

            }
            GameDataManager.Instance.SaveData();
            EditCurrentGunPanel();
        }
    }
    public void OnUpgradeHealthClicked()
    {
        if ((int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel)) <= GameDataManager.Instance.TotalMoney)
        {
            GameDataManager.Instance.TotalMoney -= (int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel));

            GameDataManager.Instance.playerHealth = (int)(1.20f * GameDataManager.Instance.playerHealth);
            GameDataManager.Instance.playerHealthLevel++;
            healthLevelCirclesParent.transform.GetChild(GameDataManager.Instance.playerHealthLevel - 1).gameObject.GetComponent<Image>().sprite = openedSprite;
            if (GameDataManager.Instance.playerHealthLevel == 5)
            {
                healthUpgradeButton.interactable = false;
                healthMoneyText.text = "MAX";
            }
            else
            {
                int currentHealthMoney = (int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel));
                healthMoneyText.text = currentHealthMoney.ToString() + "$";
            }
            healthText.text = GameDataManager.Instance.playerHealth.ToString();
            GameDataManager.Instance.SaveData();
            EditCurrentAvatarPanel();
        }
    }
    public void OnUpgradeShieldClicked()
    {
        if ((int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel)) <= GameDataManager.Instance.TotalMoney)
        {

            GameDataManager.Instance.TotalMoney -= (int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel));
            if (GameDataManager.Instance.playerShieldLevel == 0)
            {
                GameDataManager.Instance.playerShield = 10;
            }
            else
            {
                GameDataManager.Instance.playerShield = (int)(1.20f * GameDataManager.Instance.playerShield);
            }
            GameDataManager.Instance.playerShieldLevel++;
            shieldLevelCirclesParent.transform.GetChild(GameDataManager.Instance.playerShieldLevel - 1).gameObject.GetComponent<Image>().sprite = openedSprite;
            if (GameDataManager.Instance.playerShieldLevel == 5)
            {
                shieldUpgradeButton.interactable = false;
                shieldMoneyText.text = "MAX";
            }
            else
            {
                int currentShieldMoney = (int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel));
                shieldMoneyText.text = currentShieldMoney.ToString() + "$";
            }
            shieldText.text = GameDataManager.Instance.playerShield.ToString();
            GameDataManager.Instance.SaveData();
            EditCurrentAvatarPanel();

        }
    }
    public void ControllIsUpgradesFinished()
    {
        if (curPanelGun != 2)//if it is not the last gun
        {
            if (gunConfigsArray[curPanelGun].fireRateLevel == 5 && gunConfigsArray[curPanelGun].damageLevel == 5) //open new Gun
            {

                OpenNextGunAnim();
            }
        }
    }

    public void OpenNextGunAnim()
    {
        Debug.Log("SAAAAAAAAAAA");
        curPanelGun++;
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        Debug.Log(curPanelGun * sizeOfGunPanel);
        verticalLayoutGroup.transform.DOLocalMoveX(-1 * curPanelGun * sizeOfGunPanel, 1f).OnComplete(() =>
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            GunManager.Instance.Gun = (GunTypes)curPanelGun;
            GameDataManager.Instance.currentGun = curPanelGun;
            GunManager.Instance.SpawnGun(GunManager.Instance.Gun);
            gunConfigsArray[curPanelGun].isAvaliable = true;
            selectImages[curPanelGun].SetActive(true);
            EditCurrentGunPanel();

        });//move panel to the opened guns panel



    }

    public void OnAvatarButtonClicked()
    {
        avatarViewport.SetActive(true);
        weaponViewport.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        EditCurrentAvatarPanel();
    }

    public void OnWeaponButtonClicked()
    {
        avatarViewport.SetActive(false);
        weaponViewport.SetActive(true);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        EditCurrentGunPanel();

    }


}

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
    [SerializeField] Button healthUpgradeButton;
    [SerializeField] Button shieldUpgradeButton;

    [SerializeField] GameObject verticalLayoutGroup;
    [SerializeField] GameObject[] closedImages;
    [SerializeField] Button[] FireRateButtons;
    [SerializeField] Button[] damageButtons;
    [SerializeField] Button[] selectImages;
    [SerializeField] GameObject avatarViewport;
    [SerializeField] GameObject weaponViewport;
    [SerializeField] GameObject weaponButton;
    [SerializeField] GameObject avatarButton;
    [SerializeField] TextMeshProUGUI[] fireRateTexts;
    [SerializeField] TextMeshProUGUI[] damageTexts;
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
    public GameObject startScreen;
    public GameObject upgradeScreen;
    public int curPanelGun;
    public GameObject rightArrow;
    public GameObject leftArrow;
   
    
    void Start()
    {

        verticalLayoutGroup.transform.localPosition = new Vector3(0, GameDataManager.Instance.currentGun * sizeOfGunPanel, 0);
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        curPanelGun = GameDataManager.Instance.currentGun;
        EditCurrentGunPanel();
        EditCurrentAvatarPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void onOpenBottomSideButtonClicked()
    {
        startScreen.SetActive(false);
        upgradeScreen.SetActive(true);
    }
    public void onRightArrowButtonClicked()
    {
        curPanelGun++;
        
        verticalLayoutGroup.transform.DOLocalMoveX(-1* curPanelGun * sizeOfGunPanel, 1);
        EditCurrentGunPanel();
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
    } 
    public void onLeftArrowButtonClicked()
    {
        curPanelGun--;
        verticalLayoutGroup.transform.DOLocalMoveX(-1* curPanelGun * sizeOfGunPanel, 1);
        EditCurrentGunPanel();
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
    }

    public void OnSelectButtonClicked()
    {
        
        GunManager.Instance.Gun = (GunTypes)curPanelGun;
        GameDataManager.Instance.currentGun = curPanelGun;
        //selectButtons[GameDataManager.Instance.currentGun].interactable = false;
    }
    public void EditCurrentGunPanel()
    {
        if (weaponViewport.active)//if we are on gun panel side 
        {
            int fireRateLevel = gunConfigsArray[curPanelGun].fireRateLevel;
            int damageLevel = gunConfigsArray[curPanelGun].damageLevel;
            if(fireRateLevel == 5)
            {
                FireRateButtons[curPanelGun].interactable = false;
            }
            if(damageLevel == 5)
            {
                damageButtons[curPanelGun].interactable = false;
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

            int fireRateButtonMoney = (int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * (Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel)));
            fireMoneyTexts[curPanelGun].text = fireRateButtonMoney.ToString();

            int damageButtonMoney = (int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * (Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel)));
            Debug.Log(damageButtonMoney+"qwe");
            damageMoneyTexts[curPanelGun].text = damageButtonMoney.ToString();
            if (gunConfigsArray[curPanelGun].isAvaliable == true)
            {
                //selectImages[curPanelGun].interactable = true;
            }
        }

        if(curPanelGun == 0)
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
    }

    public void EditCurrentAvatarPanel()
    {
        int healthlevel =GameDataManager.Instance.playerHealthLevel;
        int shieldLevel = GameDataManager.Instance.playerShieldLevel;

        if (healthlevel == 5)
        {
            healthUpgradeButton.interactable = false;
        }
        if (shieldLevel == 5)
        {
            shieldUpgradeButton.interactable = false;
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
        int currentHealthMoney= (int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel));
        healthMoneyText.text =currentHealthMoney.ToString();
        int currentShield = GameDataManager.Instance.playerShield;
        shieldText.text = currentShield.ToString();
        int currentShieldMoney = (int)(GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel));
        shieldMoneyText.text = currentShieldMoney.ToString();
    }
    public void OnUpgradeFireRateClicked()
    {
        //increase fire RateLevel
        gunConfigsArray[curPanelGun].fireRateLevel++;
        fireRateTexts[curPanelGun].text = String.Format("{0:0.0}", GunManager.Instance.EditCurrentFireRate());

        int fireRateButtonMoney = (int)(gunConfigsArray[curPanelGun].fireRateUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].fireRateCostIncreasePercentage, gunConfigsArray[curPanelGun].fireRateLevel));
        fireMoneyTexts[curPanelGun].text = fireRateButtonMoney.ToString();
        fireRateLevelCirclesParent[curPanelGun].transform.GetChild(gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel-1).gameObject.GetComponent<Image>().sprite = openedSprite;

        if (gunConfigsArray[curPanelGun].fireRateLevel == 5)
        {
            FireRateButtons[curPanelGun].interactable = false;
            ControllIsUpgradesFinished();
        }
    }
    public void OnUpgradeDamageClicked()
    {
        //increase fire RateLevel
        gunConfigsArray[curPanelGun].damageLevel++;
        damageTexts[curPanelGun].text= GunManager.Instance.EditCurrentDamage().ToString();
        int damageButtonMoney = (int)(gunConfigsArray[curPanelGun].damageUpgradeStartMoney * Mathf.Pow(1 + gunConfigsArray[curPanelGun].damageCostIncreasePercentage, gunConfigsArray[curPanelGun].damageLevel));
        Debug.Log(damageButtonMoney+   "qwe");
        damageMoneyTexts[curPanelGun].text = damageButtonMoney.ToString();
        damageLevelCirclesParent[curPanelGun].transform.GetChild(gunConfigsArray[curPanelGun].damageLevel-1).gameObject.GetComponent<Image>().sprite = openedSprite;
        if (gunConfigsArray[curPanelGun].damageLevel == 5)
        {
            damageButtons[curPanelGun].interactable = false;
            ControllIsUpgradesFinished();
        }
    }
    public void OnUpgradeHealthClicked()
    {
        GameDataManager.Instance.playerHealth=(int)(1.20f* GameDataManager.Instance.playerHealth);
        GameDataManager.Instance.playerHealthLevel++;
        healthLevelCirclesParent.transform.GetChild(GameDataManager.Instance.playerHealthLevel- 1).gameObject.GetComponent<Image>().sprite = openedSprite;
        if (GameDataManager.Instance.playerHealthLevel == 5)
        {
            healthUpgradeButton.interactable = false;
        }
        healthText.text = GameDataManager.Instance.playerHealth.ToString();
        int currentHealthMoney = (int)(GameDataManager.Instance.playerHealthUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerHealthUpgradeIncreasePercent, GameDataManager.Instance.playerHealthLevel));
        healthMoneyText.text = currentHealthMoney.ToString();
    }
    public void OnUpgradeShieldClicked()
    {
        if (GameDataManager.Instance.playerShieldLevel == 0)
        {
            GameDataManager.Instance.playerShield = 10;
        }
        else
        {
            GameDataManager.Instance.playerShield =(int)( 1.20f*GameDataManager.Instance.playerShield);
        }
        GameDataManager.Instance.playerShieldLevel++;
        shieldLevelCirclesParent.transform.GetChild(GameDataManager.Instance.playerShieldLevel - 1).gameObject.GetComponent<Image>().sprite = openedSprite;
        if (GameDataManager.Instance.playerShieldLevel == 5)
        {
            shieldUpgradeButton.interactable = false;
        }
        shieldText.text = GameDataManager.Instance.playerShield.ToString();
        int currentShieldMoney =(int)( GameDataManager.Instance.playerShieldUpgradeStartMoney * Mathf.Pow(1 + GameDataManager.Instance.playerShieldUpgradeIncreasePercent, GameDataManager.Instance.playerShieldLevel));
        shieldMoneyText.text = currentShieldMoney.ToString();
    }
    public void ControllIsUpgradesFinished()
    {
        if(gunConfigsArray.Length != curPanelGun- 1)//if it is not the last gun
        {
            if(gunConfigsArray[curPanelGun].fireRateLevel == 5 && gunConfigsArray[curPanelGun].damageLevel == 5 ) //open new Gun
            {
                StartCoroutine(OpenNextGunAnim());
            }
        }
    }

    public IEnumerator OpenNextGunAnim()
    {
        curPanelGun++;
        yield return new WaitForSeconds(1f);
        Debug.Log(curPanelGun * sizeOfGunPanel);
        verticalLayoutGroup.transform.DOLocalMoveX(curPanelGun * sizeOfGunPanel, 3f);//move panel to the opened guns panel
        closedImages[curPanelGun].SetActive(false);
        EditCurrentGunPanel();


    }

    public void OnAvatarButtonClicked()
    {
        avatarViewport.SetActive(true);
        weaponViewport.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

    }

    public void OnWeaponButtonClicked()
    {
        avatarViewport.SetActive(false);
        weaponViewport.SetActive(true);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
}

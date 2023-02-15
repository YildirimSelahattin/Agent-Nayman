using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] GameObject verticalLayoutGroup;
    [SerializeField] GameObject[] closedImages;
    [SerializeField] Button[] FireRateButtons;
    [SerializeField] Button[] damageButtons;
    [SerializeField] GameObject avatarViewport;
    [SerializeField] GameObject weaponViewport;
    [SerializeField] GameObject weaponButton;
    [SerializeField] GameObject avatarButton;
    [SerializeField] TextMeshProUGUI[] fireRateTexts;
    [SerializeField] TextMeshProUGUI[] damageTexts;
    public Sprite openedSprite;
    public Sprite closedSprite;
    public PlayerManager playerManager;
    float sizeOfGunPanel = 700;
    public ShootingConfig[] gunConfigsArray;
   
    
    void Start()
    {

        verticalLayoutGroup.transform.localPosition = new Vector3(0, GameDataManager.Instance.currentGun * sizeOfGunPanel, 0);
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        
        EditCurrentGunPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    public void onUpArrowButtonClicked()
    {
        GameDataManager.Instance.currentGun--;
        verticalLayoutGroup.transform.DOLocalMoveY(GameDataManager.Instance.currentGun * sizeOfGunPanel, 1);
        EditCurrentGunPanel();
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        if (gunConfigsArray[GameDataManager.Instance.currentGun].isAvaliable == true)
        {
            GunManager.Instance.SpawnGun(GunManager.Instance.Gun);
           
        }
    } 
    public void onDownArrowButtonClicked()
    {
        GameDataManager.Instance.currentGun++;
        verticalLayoutGroup.transform.DOLocalMoveY(GameDataManager.Instance.currentGun * sizeOfGunPanel, 1);
        EditCurrentGunPanel();
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        if (gunConfigsArray[GameDataManager.Instance.currentGun].isAvaliable == true)
        {
            GunManager.Instance.SpawnGun(GunManager.Instance.Gun);
      
        }
    }

    public void EditCurrentGunPanel()
    {
        if (weaponViewport.active)//if we are on gun panel side 
        {
            int fireRateLevel = gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel;
            int damageLevel = gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel;
            //fireRateCircles
            for (int i = 0; i < fireRateLevel; i++)
            {
                fireRateLevelCirclesParent[GameDataManager.Instance.currentGun].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
            }
            for (int i = 0; i < damageLevel; i++)
            {
                damageLevelCirclesParent[GameDataManager.Instance.currentGun].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = openedSprite;
            }
            float currentFireRate = gunConfigsArray[GameDataManager.Instance.currentGun].baseFireRate * Mathf.Pow(1 - gunConfigsArray[GameDataManager.Instance.currentGun].fireRateIncreasePercentagePerLevel, gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel);
            fireRateTexts[GameDataManager.Instance.currentGun].text = String.Format("{0:0.0}", currentFireRate);

            int currentDamage = (int)(gunConfigsArray[GameDataManager.Instance.currentGun].BulletDamage * (Mathf.Pow(1 + gunConfigsArray[GameDataManager.Instance.currentGun].damageIncreasePercentagePerLevel, gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel)));
            damageTexts[GameDataManager.Instance.currentGun].text = currentDamage.ToString();
        }
        //upgrading avatar TODO
    }
    public void OnUpgradeFireRateClicked()
    {
        //increase fire RateLevel
        gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel++;
        fireRateTexts[GameDataManager.Instance.currentGun].text = String.Format("{0:0.0}", GunManager.Instance.EditCurrentFireRate());
        fireRateLevelCirclesParent[GameDataManager.Instance.currentGun].transform.GetChild(gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel-1).gameObject.GetComponent<Image>().sprite = openedSprite;

        if (gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel == 5)
        {
            FireRateButtons[GameDataManager.Instance.currentGun].interactable = false;
            ControllIsUpgradesFinished();
        }
    }
    public void OnUpgradeDamageClicked()
    {
        //increase fire RateLevel
        gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel++;
        damageTexts[GameDataManager.Instance.currentGun].text= GunManager.Instance.EditCurrentDamage().ToString();
        damageLevelCirclesParent[GameDataManager.Instance.currentGun].transform.GetChild(gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel-1).gameObject.GetComponent<Image>().sprite = openedSprite;
        if (gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel == 5)
        {
            damageButtons[GameDataManager.Instance.currentGun].interactable = false;
            ControllIsUpgradesFinished();
        }
    }
    public void OnUpgradeHealthClicked()
    {
        GameDataManager.Instance.playerHealth*= 10;
        GameDataManager.Instance.playerHealthLevel++;

    }
    public void OnUpgradeShieldClicked()
    {
        if (GameDataManager.Instance.playerShieldLevel == 0)
        {
            GameDataManager.Instance.playerShield = 10;
        }
        else
        {
            GameDataManager.Instance.playerShield *= 10;
        }
        GameDataManager.Instance.playerShieldLevel++;
    }
    public void ControllIsUpgradesFinished()
    {
        if(gunConfigsArray.Length != GameDataManager.Instance.currentGun - 1)//if it is not the last gun
        {
            if(gunConfigsArray[GameDataManager.Instance.currentGun].fireRateLevel == 5 && gunConfigsArray[GameDataManager.Instance.currentGun].damageLevel == 5 && GameDataManager.Instance.currentGun != 2) //open new Gun
            {
                StartCoroutine(OpenNextGunAnim());
            }
        }
    }

    public IEnumerator OpenNextGunAnim()
    {
        GameDataManager.Instance.currentGun++;
        yield return new WaitForSeconds(1f);
        Debug.Log(GameDataManager.Instance.currentGun* sizeOfGunPanel);
        verticalLayoutGroup.transform.DOLocalMoveY(GameDataManager.Instance.currentGun* sizeOfGunPanel, 3f);//move panel to the opened guns panel
        closedImages[GameDataManager.Instance.currentGun].SetActive(false);
        EditCurrentGunPanel();
        GunManager.Instance.Gun = (GunTypes)GameDataManager.Instance.currentGun;
        GunManager.Instance.SpawnGun(GunManager.Instance.Gun);

    }

    public void OnAvatarButtonClicked()
    {
        avatarViewport.SetActive(true);
        weaponViewport.SetActive(false);
    }

    public void OnWeaponButtonClicked()
    {
        avatarViewport.SetActive(false);
        weaponViewport.SetActive(true);
    }
}

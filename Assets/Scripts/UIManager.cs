using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using RengeGames.HealthBars;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using GoogleMobileAds.Api;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
    public bool isSell = false;
    [SerializeField] private GameObject ButtonPanel;
    public GameObject MoneyFromSellText;
    public GameObject TotalMoneyText;
    public GameObject Grid;
    public GameObject beltSpeedButton;
    public GameObject incomeButton;
    public GameObject workerSpeedButton;
    public GameObject addMachineButton;
    public GameObject adBeltSpeedButton;
    public GameObject adIncomeButton;
    public GameObject adWorkerSpeedButton;
    public GameObject adAddMachineButton;

    public int isSoundOn;
    public int isMusicOn;
    public int isVibrateOn;
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject vibrationOff;
    [SerializeField] GameObject vibrationOn;
    [SerializeField] private GameObject OptionsButton;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject InfoButton;
    [SerializeField] private GameObject InfoPanel;
    bool openedOptionsPanel = false;
    public int buttonIndex = 0;
    public GameObject[] gridMoneyOpenInteractableArray;
    public GameObject[] gridMoneyOpenNotInteractableArray;
    public GameObject[] gridAddArray;
    public GameObject tappingHand;
    public GameObject MergeHand;
    public int addMachineTapAmount;

    void Start()
    {
       
        UpdateSound();
        UpdateMusic();
        UpdateVibrate();
        
        
        

        
    }

 

    public void UpdateSound()
    {
        isSoundOn = GameDataManager.Instance.playSound;
        if (isSoundOn == 0)
        {
            soundOff.gameObject.SetActive(true);
            SoundsOff();
        }

        if (isSoundOn == 1)
        {
            soundOn.gameObject.SetActive(true);
            SoundsOn();
        }
    }

    public void UpdateMusic()
    {
        isMusicOn = GameDataManager.Instance.playMusic;
        if (isMusicOn == 0)
        {
            musicOff.gameObject.SetActive(true);
            MusicOff();
        }

        if (isMusicOn == 1)
        {
            musicOn.gameObject.SetActive(true);
            MusicOn();
        }
    }

    public void UpdateVibrate()
    {
        isVibrateOn = GameDataManager.Instance.playVibrate;
        if (isVibrateOn == 0)
        {
            vibrationOff.gameObject.SetActive(true);
            VibrationOff();
        }

        if (isVibrateOn == 1)
        {
            vibrationOn.gameObject.SetActive(true);
            VibrationOn();
        }
    }

    public void MusicOff()
    {
        GameDataManager.Instance.playMusic = 0;
        musicOn.gameObject.SetActive(false);
        musicOff.gameObject.SetActive(true);
        //UpdateMusic();

    }

    public void MusicOn()
    {
        GameDataManager.Instance.playMusic = 1;
        musicOff.gameObject.SetActive(false);
        musicOn.gameObject.SetActive(true);
        //UpdateMusic();
    }

    public void SoundsOff()
    {
        GameDataManager.Instance.playSound = 0;
        soundOn.gameObject.SetActive(false);
        soundOff.gameObject.SetActive(true);
        //UpdateSound();
    }

    public void SoundsOn()
    {
        GameDataManager.Instance.playSound = 1;
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
        //UpdateSound();
    }

    public void VibrationOff()
    {
        GameDataManager.Instance.playVibrate = 0;
        vibrationOn.gameObject.SetActive(false);
        vibrationOff.gameObject.SetActive(true);
        Handheld.Vibrate();
        //UpdateVibrate();
    }

    public void VibrationOn()
    {
        GameDataManager.Instance.playVibrate = 1;
        vibrationOff.gameObject.SetActive(false);
        vibrationOn.gameObject.SetActive(true);
        Handheld.Vibrate();
       // UpdateVibrate();

    }

    public void OnOpenOptionsPanel()
    {
        if(openedOptionsPanel == false)
        {
            OptionsPanel.SetActive(true);
            openedOptionsPanel = true;
        }
        else
        {
            OptionsPanel.SetActive(false);
            openedOptionsPanel = false;
        }
    }

    public void VibratePhone(){
        Handheld.Vibrate();
    }
    public void OnOpenInfoPanel()
    {
        InfoPanel.SetActive(true);
    }

    public void OnSpace()
    {
        OptionsPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }
    
    

    
}
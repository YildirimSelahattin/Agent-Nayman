using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using FIMSpace.Basics;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
    int isSoundOn;
    int isMusicOn;
    int isVibrateOn;
    
    public ParticleSystem windEffect;
    public GameObject startScreen;
    public GameObject flyingScreen;
    public GameObject endScreen;
    public GameObject RevivePanelScreen;

    [SerializeField] GameObject agent;
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject vibrationOff;
    [SerializeField] GameObject vibrationOn;
    [SerializeField] TextMeshProUGUI healthText; 
    [SerializeField] TextMeshProUGUI shieldText;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
        /*UpdateSound();
        UpdateMusic();
        UpdateVibrate();*/
     
    }
    public void OnTapToStartButtonClicked()
    {

        startScreen.SetActive(false);
        flyingScreen.SetActive(true);
        PlayerManager.Instance.StartFalling();
    }
    public void ReviveButtonClicked(){
        flyingScreen.SetActive(true);
        RevivePanelScreen.SetActive(false);
        endScreen.SetActive(false);
        PlayerManager.Instance.isAdPlayed = true;
         PlayerManager.Instance.myAnimator.SetBool("isDead", false);
        PlayerManager.Instance.environmentMoveScript.enabled = true;
        PlayerManager.Instance.Health = 100;
        UIManager.Instance.ChangeHealthText(100);
        
    }
    public void LoadSceneButton()
    {
        //GameDataManager.Instance.levelToLoad += 1;
        
        SceneManager.LoadScene(0);

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
    public void ChangeHealthText(int healthleft)
    {
        healthText.text = healthleft.ToString();
    }
    public void ChangeShieldText(int shieldLeft)
    {
        shieldText.text = shieldLeft.ToString();
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

    public void VibratePhone(){
        Handheld.Vibrate();
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
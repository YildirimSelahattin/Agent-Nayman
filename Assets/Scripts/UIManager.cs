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
    public GameObject Heli;
    public ParticleSystem windEffect;
    public GameObject startScreen;
    public GameObject endScreen;
    [SerializeField] GameObject agent;
    [SerializeField] GameObject soundOn;
    [SerializeField] GameObject soundOff;
    [SerializeField] GameObject musicOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject vibrationOff;
    [SerializeField] GameObject vibrationOn;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
        /*UpdateSound();
        UpdateMusic();
        UpdateVibrate();*/
        //OnTapToStartButtonClicked();
    }
    public void OnTapToStartButtonClicked()
    {
        
        Debug.Log("sa");
        windEffect.gameObject.SetActive(true);
        windEffect.Play();
        startScreen.SetActive(false);
        PlayerManager.Instance.StartFalling();
        Heli.SetActive(false);
        
    }
    public void LoadSceneButton()
    {
        GameDataManager.Instance.levelToLoad += 1;
        
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

    public void Choose1(){
    if (GunManager.Instance.ActiveGun != null)
    {
        Destroy(GunManager.Instance.ActiveGun.Model.gameObject);
    }
    GunTypes gun = GunTypes.rayGun;
    GunManager.Instance.Gun = GunTypes.rayGun;
    GunManager.Instance.SpawnGun(gun);

    }
    public void Choose2(){
    if (GunManager.Instance.ActiveGun != null)
    {
    Destroy(GunManager.Instance.ActiveGun.Model.gameObject);
    }
    GunTypes gun = GunTypes.pistol;
    GunManager.Instance.Gun = GunTypes.pistol;
    GunManager.Instance.SpawnGun(gun);

        
    }
    public void Choose3(){
    if (GunManager.Instance.ActiveGun != null)
    {
        Destroy(GunManager.Instance.ActiveGun.Model.gameObject);
    }
    GunTypes gun = GunTypes.revolver;
    GunManager.Instance.Gun = GunTypes.revolver;
    GunManager.Instance.SpawnGun(gun);
    

        
    }
}
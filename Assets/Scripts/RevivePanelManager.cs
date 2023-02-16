using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RevivePanelManager : MonoBehaviour
{
    public TextMeshProUGUI TimeCounter;
    public Button ReviveButton;
    public float seconds;
    public bool timeFinished = false;
    void Start()
    {
        seconds = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeFinished != true)
        {
            seconds -= (Time.deltaTime);
            TimeCounter.text = ((int)(seconds)).ToString();

            if (seconds < 0)
            {
                UIManager.Instance.RevivePanelScreen.SetActive(false);
                UIManager.Instance.endScreen.SetActive(true);

                timeFinished = true;
            }
        }

    }
}

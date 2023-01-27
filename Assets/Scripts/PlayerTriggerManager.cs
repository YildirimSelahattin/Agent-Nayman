using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerManager : MonoBehaviour
{
    private bool isOpenParachute = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Money"))
        {
            GameDataManager.Instance.money += 10;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("SpeedUp"))
        {
        }
        if (other.CompareTag("shield"))
        {
        }
        if (other.CompareTag("parachute"))
        {
            isOpenParachute = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            UIManager.Instance.windEffect.Stop();
            gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 20, -42), 2f);
            gameObject.transform.DORotate(new Vector3(-20, 0, 0), 1f);
        }
    }
}
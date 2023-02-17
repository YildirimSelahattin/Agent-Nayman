using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimModelManager : MonoBehaviour
{

    Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        SizeUpAndDown();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void SizeUpAndDown()
    {
        transform.DOScale(originalScale * 1.4f, 0.5f).OnComplete(() =>
        {
            transform.DOScale(originalScale, 0.3f).OnComplete(()=>SizeUpAndDown());
        });
    }
}

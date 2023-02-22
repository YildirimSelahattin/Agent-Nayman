using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BuildingPointerMove : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        originalPos = transform.localPosition;
        StartCoroutine(MoveLoop());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveLoop()
    {
        yield return new WaitForSeconds(0.3f);
        transform.DOScale(originalScale*1.2f,0.5f);
        transform.DOLocalMoveZ(originalPos.z - 0.015f, 0.5f).OnComplete(() =>
        {
            transform.DOScale(originalScale , 0.5f);
            transform.DOLocalMoveZ(originalPos.z, 0.5f).OnComplete(()=>StartCoroutine(MoveLoop()));
        });
    }
}

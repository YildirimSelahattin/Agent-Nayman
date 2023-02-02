using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BuildingPointerMove : MonoBehaviour
{
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
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
        transform.DOLocalMoveZ(originalPos.z - 0.015f, 0.5f).OnComplete(() =>
        {
            transform.DOLocalMoveZ(originalPos.z, 0.5f).OnComplete(()=>StartCoroutine(MoveLoop()));
        });
    }
}

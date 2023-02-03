using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField]Material arrowMaterial;
    [SerializeField] float  moveZOffset;
    [SerializeField] float  moveXOffset;
    Vector3 originalPos;
    public bool isHorizontal;
    public bool isVertical; 
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.localPosition;
        StartCoroutine(FadeInFadeOutLoop());
        if (isVertical)
        {
            StartCoroutine(UpDownLoop());
        }
        if (isHorizontal)
        {
            StartCoroutine(LeftRightLoop());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeInFadeOutLoop()
    {

        yield return new WaitForSeconds(2);
        arrowMaterial.DOFade(0.5F, 0.3F).OnComplete(()=>arrowMaterial.DOFade(1f,0.3f).OnComplete(()=>StartCoroutine(FadeInFadeOutLoop())));
    }
    public IEnumerator UpDownLoop()
    {
        yield return new WaitForSeconds(2);
        transform.DOLocalMoveZ(originalPos.z + moveZOffset, 0.2f).OnComplete(() => transform.DOLocalMoveZ(originalPos.z, 2f).OnComplete(()=>StartCoroutine(UpDownLoop())));


    }
    public IEnumerator LeftRightLoop()
    {
        yield return new WaitForSeconds(2);
        transform.DOLocalMoveX(originalPos.x + moveXOffset, 0.2f).OnComplete(() => transform.DOLocalMoveX(originalPos.x, 2f).OnComplete(()=>StartCoroutine(UpDownLoop())));


    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}

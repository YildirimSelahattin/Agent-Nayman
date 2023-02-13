using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HeliMove : MonoBehaviour
{
    public GameObject heli;
    public float y = -90;

    void Start()
    {
        //StartCoroutine(heliMove());
    }
    private void Update() {
        y -= 30;
        heli.transform.DOLocalRotate(new Vector3(0,y,0),0.3f);


    }
    
    public IEnumerator heliMove(){
       
         yield return new WaitForSeconds(0.1f);
        heliMove();
    }
}

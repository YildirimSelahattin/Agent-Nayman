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
    }
    private void Update() {
        y -= 30;
        heli.transform.DOLocalRotate(new Vector3(0,y,0),0.3f);


    }
    
   
}

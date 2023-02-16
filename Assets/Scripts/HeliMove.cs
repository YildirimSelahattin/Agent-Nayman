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
        heli.transform.RotateAroundLocal(transform.position, 5*Time.deltaTime);


    }
    
   
}

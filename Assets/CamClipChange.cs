using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamClipChange : MonoBehaviour
{
    public CinemachineVirtualCamera activeCam;
    
    void Start()
    {
        activeCam = GameObject.FindGameObjectWithTag("FlyCam").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activeCam.m_Lens.FarClipPlane = 250;
        }
    }
}

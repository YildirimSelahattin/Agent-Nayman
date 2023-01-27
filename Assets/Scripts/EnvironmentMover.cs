using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMover : MonoBehaviour
{
    public float forwardMoveSpeed;
    public static EnvironmentMover Instance = null;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * forwardMoveSpeed * Time.deltaTime;//regular go forward
    }
}
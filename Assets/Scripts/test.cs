using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Mathf.Rad2Deg * Mathf.Acos(-2 / Mathf.Sqrt(26)));
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsSquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
    
    public void Deselect()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}

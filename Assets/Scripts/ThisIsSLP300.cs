using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsSLP300 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 2);
        Destroy(gameObject, 10f);
    }
}

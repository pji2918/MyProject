using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false, isLocked = false;
    private Vector3 originPos, newPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;

        newPos = originPos + (transform.right * 3);
    }

    private float time = 0f;

    public bool isOpening = false;

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            time += Time.deltaTime;
            if (!isOpen)
            {
                transform.position = Vector3.Lerp(originPos, newPos, time);
                if (time >= 1)
                {
                    isOpen = true;
                    isOpening = false;
                    time = 0;
                }
            }
            else
            {
                transform.position = Vector3.Lerp(newPos, originPos, time);
                if (time >= 1)
                {
                    isOpen = false;
                    isOpening = false;
                    time = 0;
                }
            }
        }
    }

    public void OpenTheDoor()
    {
        isOpening = true;
    }
}
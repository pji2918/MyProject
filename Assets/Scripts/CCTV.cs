using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private float detectTime = 0f;
    [SerializeField] private float rotatingSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, LayerMask.GetMask("Player")))
        if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 10f, LayerMask.GetMask("Player")))
        {
            if (hit.collider.CompareTag("Player"))
            {
                detectTime = 3f;
                transform.LookAt(GameObject.Find("Player").transform);
                hit.collider.GetComponent<PlayerController>().DetectedByCCTV();
            }
        }

        if (detectTime > 0)
        {
            detectTime -= Time.deltaTime;
        }
        else if (detectTime <= 0)
        {
            detectTime = 0;

            CCTV[] cctvs = transform.parent.GetComponentsInChildren<CCTV>();

            // if all cctvs are not detecting player
            bool allNotDetecting = true;
            foreach (CCTV cctv in cctvs)
            {
                if (cctv.detectTime > 0)
                {
                    allNotDetecting = false;
                    break;
                }
                else
                {
                    allNotDetecting = true;
                }
            }

            if (allNotDetecting)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().UnDetect();
            }
            transform.rotation = Quaternion.Euler(45, PingPong(Time.time * rotatingSpeed, maxAngle, minAngle), 0);
        }
    }

    public int maxAngle = 160, minAngle = 0;

    public float PingPong(float t, float max = 160, float min = 0)
    {
        return min + Mathf.PingPong(t - min, max - min);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //
            // if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            // {
            //     Debug.DrawRay(ray.origin,ray.direction * 20, Color.red);
            //     if (!hit.transform.gameObject.GetComponent<Animator>().GetBool("isRotating"))
            //     {
            //         hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            //     }
            //     else
            //     {
            //         hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            //         
            //         hit.transform.rotation = Quaternion.Euler(0,0,0);
            //     }
            //     hit.transform.gameObject.GetComponent<Animator>().SetBool("isRotating", !hit.transform.gameObject.GetComponent<Animator>().GetBool("isRotating"));
            // }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (!hit.transform.gameObject.GetComponent<Animator>().GetBool("isRotating"))
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

                    hit.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                hit.transform.gameObject.GetComponent<Animator>().SetBool("isRotating", !hit.transform.gameObject.GetComponent<Animator>().GetBool("isRotating"));
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
        }
    }
}

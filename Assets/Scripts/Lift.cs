using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 25, transform.position.z), Time.deltaTime);

            if (transform.position.y >= 25)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndHell");
            }
        }
    }

    public void MoveLift()
    {
        isMoving = true;
    }
}

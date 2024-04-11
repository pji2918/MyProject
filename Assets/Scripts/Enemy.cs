using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Found,
        RemoteFound,
        Warning
    }

    [SerializeField] private bool drawAngles;

    [Range(0f, 360f)][SerializeField] private float viewAngle = 0f;
    [SerializeField] private float viewRadius = 1f;

    private NavMeshAgent agent;
    private Transform floorDetectionRayDirection;

    Vector3 AngleToDirection(float ang)
    {
        float radianAngle = ang * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radianAngle), 0, Mathf.Cos(radianAngle));
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        floorDetectionRayDirection = transform.GetChild(1);
    }

    public State state = State.Idle;
    public Transform player;

    void OnDrawGizmos()
    {
        if (drawAngles)
        {
            Vector3 pos = transform.position + Vector3.up * 0.5f;
            Gizmos.DrawWireSphere(pos, viewRadius);
        }
    }

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;

    private float warningTime = 5f;

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        Ray groundRay = new Ray(floorDetectionRayDirection.position, floorDetectionRayDirection.forward);
        Debug.DrawRay(groundRay.origin, groundRay.direction * 1.5f, Color.blue, 0.1f);

        Vector3 pos = transform.position + Vector3.up * 0.5f;

        float lookingAngle = transform.eulerAngles.y;
        Vector3 rightDir = AngleToDirection(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 leftDir = AngleToDirection(transform.eulerAngles.y - viewAngle * 0.5f);
        Vector3 lookDir = AngleToDirection(lookingAngle);

        if (drawAngles)
        {
            Debug.DrawRay(pos, rightDir * viewRadius, Color.blue);
            Debug.DrawRay(pos, leftDir * viewRadius, Color.blue);
            Debug.DrawRay(pos, lookDir * viewRadius, Color.cyan);
        }

        targetList.Clear();
        Collider[] targets = Physics.OverlapSphere(pos, viewRadius, playerMask);

        if (targets.Length != 0)
        {
            foreach (Collider col in targets)
            {
                Vector3 targetPos = col.transform.position;
                Vector3 targetDir = (targetPos - pos).normalized;
                float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
                if (targetAngle <= viewAngle * 0.5f && !Physics.Raycast(pos, targetDir, viewRadius, obstacleMask))
                {
                    targetList.Add(col);
                    if (drawAngles)
                    {
                        Debug.DrawLine(pos, targetPos, Color.red);
                    }
                }
            }
        }

        switch (state)
        {
            case State.Idle:
                {
                    if (targetList.Contains(player.GetComponent<Collider>()))
                    {
                        state = State.Found;
                    }

                    agent.SetDestination(transform.position + transform.forward * 2);

                    if (!Physics.Raycast(groundRay, 1.5f, LayerMask.GetMask("Ground")))
                    {
                        transform.Rotate(0, Random.Range(-180, 180), 0);
                    }
                    else if (Physics.Raycast(groundRay, 1.5f, LayerMask.GetMask("Wall")))
                    {
                        transform.Rotate(0, Random.Range(-180, 180), 0);
                    }

                    break;
                }
            case State.Found:
                {
                    if (targetList.Contains(player.GetComponent<Collider>()))
                    {
                        targetList[0].GetComponent<PlayerController>().DetectedByEnemy(transform);
                        agent.SetDestination(targetList[0].transform.position);
                    }
                    else
                    {
                        state = State.Idle;
                    }
                    break;
                }
            case State.RemoteFound:
                {
                    agent.SetDestination(player.position);

                    if (Vector3.Distance(transform.position, player.position) > 10)
                    {
                        state = State.Warning;
                    }
                    break;
                }
            case State.Warning:
                {
                    viewAngle = 360;
                    viewRadius = 10;

                    if (warningTime > 0)
                    {
                        warningTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = State.Idle;

                        viewAngle = 120;
                        viewRadius = 5;

                        warningTime = 5f;
                    }
                    break;
                }
        }
    }

    public Vector3 ReturnDirectionByQuaternion(Vector3 dir, int rot)
    {
        Quaternion rotation = Quaternion.Euler(0, rot, 0);

        return dir + rotation.eulerAngles;
    }

    List<Collider> targetList = new List<Collider>();

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().Die();
        }
    }
}

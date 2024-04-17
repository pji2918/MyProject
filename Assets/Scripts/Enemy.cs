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
        Warning,
        Sleeping
    }

    private Rigidbody rb;

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
        agent = gameObject.GetComponent<NavMeshAgent>();
        floorDetectionRayDirection = transform.GetChild(1);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();
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

    private float warningTime = 8f, sleepingTime = 5f;


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

        if (transform.position.y < -10)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            rb.velocity = Vector3.zero;

            state = State.Warning;
            transform.SetPositionAndRotation(player.position - (player.forward * 2f), Quaternion.Euler(0, 0, 0));

            transform.LookAt(player);

            agent.enabled = true;
        }

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

                    if (agent.enabled && agent.isOnNavMesh)
                    {
                        agent.SetDestination(transform.position + transform.forward * 2);
                    }

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
                    if (agent.enabled && agent.isOnNavMesh)
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
                    }
                    break;
                }
            case State.RemoteFound:
                {
                    if (agent.enabled && agent.isOnNavMesh)
                    {
                        agent.SetDestination(player.position);
                    }

                    if (Physics.Raycast(groundRay, out RaycastHit hit, 1.5f, LayerMask.GetMask("Wall")))
                    {
                        if (hit.collider.CompareTag("Door") && !hit.collider.GetComponent<Door>().isOpen && !hit.collider.GetComponent<Door>().isLocked)
                        {
                            hit.collider.GetComponent<Door>().OpenTheDoor();
                        }
                    }

                    if (Vector3.Distance(transform.position, player.position) > 10)
                    {
                        state = State.Warning;
                    }
                    break;
                }
            case State.Warning:
                {
                    viewAngle = 360;
                    viewRadius = 20;

                    agent.enabled = true;

                    rb.constraints = RigidbodyConstraints.FreezeAll;

                    rb.velocity = Vector3.zero;

                    if (transform.rotation.eulerAngles.x != 0)
                    {
                        transform.SetPositionAndRotation(new Vector3(transform.position.x, 11, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    }
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


                    if (agent.enabled && agent.isOnNavMesh)
                    {
                        agent.SetDestination(transform.position + transform.forward * 2);
                    }


                    if (!Physics.Raycast(groundRay, 1.5f, LayerMask.GetMask("Ground")))
                    {
                        transform.Rotate(0, Random.Range(-180, 180), 0);
                    }
                    else if (Physics.Raycast(groundRay, 1.5f, LayerMask.GetMask("Wall")))
                    {
                        transform.Rotate(0, Random.Range(-180, 180), 0);
                    }

                    if (targetList.Contains(player.GetComponent<Collider>()))
                    {
                        state = State.Found;
                    }
                    break;
                }
            case State.Sleeping:
                {
                    if (sleepingTime > 0)
                    {
                        rb.constraints = RigidbodyConstraints.None;
                        agent.enabled = false;
                        sleepingTime -= Time.deltaTime;
                    }
                    else
                    {
                        agent.enabled = true;
                        rb.constraints = RigidbodyConstraints.FreezeAll;

                        rb.velocity = Vector3.zero;

                        transform.SetPositionAndRotation(new Vector3(transform.position.x, 11, transform.position.z), Quaternion.Euler(0, Random.Range(0, 360), 0));
                        state = State.Warning;
                        sleepingTime = 5f;
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
        if (other.gameObject.CompareTag("Player") && state != State.Sleeping)
        {
            other.transform.GetComponent<PlayerController>().Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SLP-300") && state != State.Sleeping)
        {
            state = State.Sleeping;
            Destroy(other.gameObject);

            rb.AddForce(-transform.forward * 3, ForceMode.Impulse);
        }
    }
}

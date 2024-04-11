using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllable = true;

    [HideInInspector] public Rigidbody rb;
    private Camera playerCamera;
    [SerializeField][Range(0f, 10f)] private float mouseSensitivity = 1f;
    [SerializeField] private float moveSpeed = 30f;

    [SerializeField] private float doorOpenTime = 3f, inventoryOpenTime = 1f;

    public List<Item> inventory = new List<Item>();

    private int score = 0;

    private float currentActionTime = 0f;

    private bool isDoorOpening = false, isInvOpening = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private float xRotation;
    private float yRotation;

    // Update is called once per frame
    void Update()
    {
        if (controllable)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = moveSpeed * x * transform.right + moveSpeed * z * transform.forward;

            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity * 100) * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity * 100) * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            Ray ray = new Ray(transform.position, transform.forward);

            if (!UIManager.instance.inventoryUI.activeSelf)
            {
                if (Input.GetKey(KeyCode.Tab) && !isDoorOpening)
                {
                    isInvOpening = true;
                    UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "주머니 여는 중...";
                    UIManager.instance.progressBarContainer.SetActive(true);
                    currentActionTime += Time.deltaTime;
                    UIManager.instance.progressBar.fillAmount = currentActionTime / inventoryOpenTime;
                    if (currentActionTime >= inventoryOpenTime)
                    {
                        isInvOpening = false;
                        UIManager.instance.progressBarContainer.SetActive(false);
                        score += 10;
                        UIManager.instance.OpenInventory();
                    }
                }
                else
                {
                    isInvOpening = false;
                    if (!isDoorOpening)
                    {
                        UIManager.instance.progressBarContainer.SetActive(false);
                        currentActionTime = 0f;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    UIManager.instance.OpenInventory();
                }
            }

            if (Physics.Raycast(ray, out RaycastHit hit, 1.5f))
            {
                if (hit.collider.CompareTag("Door") && !hit.transform.GetComponent<Door>().isOpening)
                {
                    if (hit.collider.GetComponent<Door>().isOpen)
                    {
                        UIManager.instance.interactText.text = "F를 길게 눌러 문 닫기";
                    }
                    else
                    {
                        UIManager.instance.interactText.text = "F를 길게 눌러 문 열기";
                    }

                    if (Input.GetKey(KeyCode.F) && !isInvOpening)
                    {
                        isDoorOpening = true;
                        UIManager.instance.interactText.gameObject.SetActive(false);
                        if (hit.collider.GetComponent<Door>().isOpen)
                        {
                            UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "문 닫는 중...";
                        }
                        else
                        {
                            UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "문 여는 중...";
                        }
                        UIManager.instance.progressBarContainer.SetActive(true);
                        currentActionTime += Time.deltaTime;
                        UIManager.instance.progressBar.fillAmount = currentActionTime / doorOpenTime;
                        if (currentActionTime >= doorOpenTime)
                        {
                            score += 10;
                            isDoorOpening = false;
                            UIManager.instance.progressBarContainer.SetActive(false);
                            hit.collider.GetComponent<Door>().OpenTheDoor();
                        }
                    }
                    else
                    {
                        isDoorOpening = false;
                        if (!isInvOpening)
                        {
                            score += 10;
                            UIManager.instance.interactText.gameObject.SetActive(true);
                            UIManager.instance.progressBarContainer.SetActive(false);
                            currentActionTime = 0f;
                        }
                    }
                }
            }
            else
            {
                isDoorOpening = false;
                if (!isInvOpening)
                {
                    currentActionTime = 0f;
                    UIManager.instance.interactText.gameObject.SetActive(false);
                    UIManager.instance.progressBarContainer.SetActive(false);
                }
            }

            if (transform.position.y < -10)
            {
                Die();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                UIManager.instance.OpenInventory();
            }
        }
    }

    public void Die()
    {
        UIManager.instance.youDiedScreen.SetActive(true);
        UIManager.instance.scoreText.text = score.ToString();
        controllable = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        rb.constraints = RigidbodyConstraints.None;
    }

    public void DetectedByCCTV()
    {
        // Debug.LogWarning("위치 발각됨!");
        UIManager.instance.detectedWarning.SetActive(true);
        UIManager.instance.detectedWarning.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "CCTV에 감지됨";
        Enemy[] enemy = FindObjectsOfType<Enemy>();

        foreach (Enemy e in enemy)
        {
            e.state = Enemy.State.RemoteFound;
        }
    }

    public void DetectedByEnemy(Transform pos)
    {
        UIManager.instance.detectedWarning.SetActive(true);
        UIManager.instance.detectedWarning.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "적에게 발각됨";
        Collider[] enemycolls = Physics.OverlapSphere(pos.position, 15f, LayerMask.GetMask("Enemy"));

        foreach (var e in enemycolls)
        {
            e.GetComponent<Enemy>().state = Enemy.State.RemoteFound;
        }
    }

    public void UnDetect()
    {
        score += 1;
        Enemy[] enemy = FindObjectsOfType<Enemy>();
        UIManager.instance.detectedWarning.SetActive(false);
        foreach (Enemy e in enemy)
        {
            if (e.state == Enemy.State.RemoteFound)
            {
                e.state = Enemy.State.Warning;
            }
        }
    }
}

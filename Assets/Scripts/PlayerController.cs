using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllable = true;

    [HideInInspector] public Rigidbody rb;
    private Camera playerCamera;
    [SerializeField][Range(0f, 10f)] private float mouseSensitivity = 1f;
    [SerializeField] private float moveSpeed = 30f;

    [SerializeField] private float doorOpenTime = 3f, inventoryOpenTime = 1f;

    public Item[] inventory = new Item[6];

    private int score = 0;

    private float currentActionTime = 0f;

    private bool isDoorOpening = false, isInvOpening = false;

    public Item holdingItem;

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
        }

        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Tab))
        {
            currentActionTime = 0f;
            UIManager.instance.progressBarContainer.SetActive(false);
        }

        Ray ray = new Ray(transform.position, transform.forward);

        if (!UIManager.instance.inventoryUI.activeSelf)
        {
            if (Input.GetKey(KeyCode.Tab) && !isDoorOpening)
            {
                if (holdingItem != null)
                {
                    if (rb.velocity == Vector3.zero)
                    {
                        isInvOpening = true;
                        UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "주머니에 아이템 넣는 중...";
                        UIManager.instance.progressBarContainer.SetActive(true);
                        UIManager.instance.interactText.gameObject.SetActive(false);
                        currentActionTime += Time.deltaTime;
                        UIManager.instance.progressBar.fillAmount = currentActionTime / holdingItem.EquipTime;
                        if (currentActionTime >= holdingItem.EquipTime)
                        {
                            score += 10;
                            UIManager.instance.progressBarContainer.SetActive(false);
                            currentActionTime = 0f;
                            UIManager.instance.ItemUI.SetActive(false);
                            holdingItem = null;
                            UIManager.instance.OpenInventory();
                        }
                    }
                    else
                    {
                        isInvOpening = false;
                        if (!isEquipping && !isDoorOpening)
                        {
                            currentActionTime = 0f;
                            UIManager.instance.progressBarContainer.SetActive(false);
                        }
                    }
                }
                else
                {
                    if (rb.velocity == Vector3.zero)
                    {
                        isInvOpening = true;
                        UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "주머니 여는 중...";
                        UIManager.instance.progressBarContainer.SetActive(true);
                        UIManager.instance.interactText.gameObject.SetActive(false);
                        currentActionTime += Time.deltaTime;
                        UIManager.instance.progressBar.fillAmount = currentActionTime / inventoryOpenTime;
                        if (currentActionTime >= inventoryOpenTime)
                        {
                            isInvOpening = false;
                            UIManager.instance.progressBarContainer.SetActive(false);
                            currentActionTime = 0f;
                            score += 10;
                            UIManager.instance.OpenInventory();
                        }
                    }
                    else
                    {
                        isInvOpening = false;
                        if (!isEquipping && !isDoorOpening)
                        {
                            currentActionTime = 0f;
                            UIManager.instance.progressBarContainer.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                isInvOpening = false;
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
            if (hit.collider.CompareTag("Door"))
            {
                if (!hit.collider.gameObject.GetComponent<Door>().isLocked)
                {
                    if (!hit.transform.GetComponent<Door>().isOpening && !isInvOpening)
                    {
                        UIManager.instance.interactText.gameObject.SetActive(true);

                        if (hit.collider.GetComponent<Door>().isOpen)
                        {
                            UIManager.instance.interactText.text = "F를 길게 눌러 문 닫기";
                        }
                        else
                        {
                            UIManager.instance.interactText.text = "F를 길게 눌러 문 열기";
                        }

                        if (Input.GetKey(KeyCode.F))
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
                                UIManager.instance.progressBarContainer.SetActive(false);
                                score += 10;
                                isDoorOpening = false;
                                hit.collider.GetComponent<Door>().OpenTheDoor();
                            }
                        }
                        else
                        {
                            isDoorOpening = false;
                        }
                    }
                }
                else
                {
                    isDoorOpening = false;
                    if (holdingItem == null)
                    {
                        UIManager.instance.interactText.gameObject.SetActive(true);
                        UIManager.instance.interactText.text = "이 문은 잠겨 있습니다.\n" +
                        "열려면 " + hit.collider.GetComponent<Door>().key.Description[..15] + hit.collider.GetComponent<Door>().key.ItemName + "</color>" + "가 필요합니다.";
                    }
                    else
                    {
                        UIManager.instance.interactText.gameObject.SetActive(true);
                        UIManager.instance.interactText.text = "F를 길게 눌러 문 잠금 해제";

                        if (Input.GetKey(KeyCode.F) && rb.velocity == Vector3.zero)
                        {
                            isDoorOpening = true;
                            UIManager.instance.interactText.gameObject.SetActive(false);
                            UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "문 잠금 해제 중...";
                            UIManager.instance.progressBarContainer.SetActive(true);
                            currentActionTime += Time.deltaTime;
                            UIManager.instance.progressBar.fillAmount = currentActionTime / hit.collider.GetComponent<Door>().unlockTime;

                            if (currentActionTime >= hit.collider.GetComponent<Door>().unlockTime * 0.8 && hit.collider.GetComponent<Door>().key != holdingItem)
                            {
                                currentActionTime = 0f;
                                UIManager.instance.progressBarContainer.SetActive(false);

                                UIManager.instance.interactText.gameObject.SetActive(true);
                                UIManager.instance.interactText.text = "열쇠가 맞지 않습니다.";


                                isDoorOpening = false;
                            }
                            else if (currentActionTime >= hit.collider.GetComponent<Door>().unlockTime)
                            {
                                currentActionTime = 0f;
                                UIManager.instance.progressBarContainer.SetActive(false);
                                score += 100;
                                isDoorOpening = false;
                                hit.collider.GetComponent<Door>().isLocked = false;

                                inventory[Array.IndexOf(inventory, holdingItem)] = null;
                                holdingItem = null;

                                UIManager.instance.RefreshInventory();
                            }
                        }
                        else
                        {
                            isDoorOpening = false;

                            if (!isEquipping && !isInvOpening)
                            {
                                currentActionTime = 0f;
                                UIManager.instance.progressBarContainer.SetActive(false);
                            }
                        }
                    }
                }
            }
            else
            {
                isDoorOpening = false;
                UIManager.instance.interactText.gameObject.SetActive(false);

                if (!isEquipping && !isInvOpening)
                {
                    currentActionTime = 0f;
                    UIManager.instance.progressBarContainer.SetActive(false);
                }
            }
        }
        else
        {
            isDoorOpening = false;
            UIManager.instance.interactText.gameObject.SetActive(false);

            if (!isEquipping && !isInvOpening)
            {
                currentActionTime = 0f;
                UIManager.instance.progressBarContainer.SetActive(false);
            }
        }

        if (transform.position.y < -10)
        {
            Die();
        }

        if (isEquipping)
        {
            if (rb.velocity == Vector3.zero)
            {
                currentActionTime += Time.deltaTime;
                UIManager.instance.progressBar.fillAmount = currentActionTime / inventory[idx].EquipTime;
                UIManager.instance.progressBarContainer.SetActive(true);
                UIManager.instance.progressBarContainer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0} 장착 중...", inventory[idx].ItemName);

                if (currentActionTime >= inventory[idx].EquipTime)
                {
                    holdingItem = inventory[idx];
                    UIManager.instance.progressBarContainer.SetActive(false);
                    isEquipping = false;
                    currentActionTime = 0f;
                }
            }
            else
            {
                isEquipping = false;
            }
        }

        if (holdingItem != null)
        {
            UIManager.instance.ItemUI.SetActive(true);
            UIManager.instance.ItemUI.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = holdingItem.Icon;
            UIManager.instance.ItemUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0} / {1}", holdingItem.Amount, inventory[Array.IndexOf(inventory, holdingItem)].Amount);
        }
        else
        {
            UIManager.instance.ItemUI.SetActive(false);
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

    private bool isEquipping = false;

    private int idx = -1;

    public void EquipItem(int index)
    {
        isInvOpening = false;
        UIManager.instance.OpenInventory();
        idx = index;
        currentActionTime = 0f;
        isEquipping = true;
    }
}

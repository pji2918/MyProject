using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI interactText, itemName, itemDescription, scoreText;
    public GameObject detectedWarning, youDiedScreen, progressBarContainer, inventoryUI, tooltipUI;
    public Image progressBar;
    public RectTransform mouseBox;

    private GameObject player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryUI.activeSelf)
        {
            mouseBox.position = Input.mousePosition;
        }
    }

    public void OpenInventory()
    {
        if (!inventoryUI.activeSelf)
        {
            GameObject inventorySlot = inventoryUI.transform.GetChild(3).gameObject;

            foreach (var (value, i) in player.GetComponent<PlayerController>().inventory.Select((value, i) => (value, i)))
            {
                if (value is null)
                {
                    inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                    inventorySlot.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = value.Icon;
                    inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => CallTooltip(value));
                    inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => CloseTooltip());
                }
            }

            inventoryUI.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            FindObjectOfType<PlayerController>().controllable = false;
        }
        else
        {
            inventoryUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            FindObjectOfType<PlayerController>().controllable = true;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CallTooltip(Item item)
    {
        itemName.text = item.ItemName;
        itemDescription.text = item.Description;

        tooltipUI.SetActive(true);
    }

    public void CloseTooltip()
    {
        tooltipUI.SetActive(false);
    }

    public void SortInventory()
    {
        player.GetComponent<PlayerController>().inventory.Sort();

        GameObject inventorySlot = inventoryUI.transform.GetChild(3).gameObject;

        foreach (var (value, i) in player.GetComponent<PlayerController>().inventory.Select((value, i) => (value, i)))
        {
            if (value is null)
            {
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                inventorySlot.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = value.Icon;
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => CallTooltip(value));
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => CloseTooltip());
            }
        }
    }
}

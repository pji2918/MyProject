using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI interactText, itemName, itemDescription, scoreText, gunUsage, warningText, itemNameText, gunChargeText, debugInfoText;
    public GameObject detectedWarning, youDiedScreen, progressBarContainer, inventoryUI, tooltipUI, ItemUI, gunChargeUIContainer, crosshair;
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

    private bool debugScreenOn = false;

    // Update is called once per frame
    void Update()
    {
        if (inventoryUI.activeSelf)
        {
            mouseBox.position = Input.mousePosition;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugScreenOn = !debugScreenOn;

            debugInfoText.gameObject.SetActive(debugScreenOn);
        }

        if (debugScreenOn)
        {
            if (Application.targetFrameRate == -1)
            {
                frameRateCap = System.Convert.ToInt32(Screen.currentResolution.refreshRateRatio.value);
            }
            else
            {
                frameRateCap = Application.targetFrameRate;
            }

            debugInfoText.text = string.Format("FPS: {0:0} / {1}\nDisplay: {2}x{3}", 1 / Time.smoothDeltaTime, frameRateCap, Screen.width, Screen.height);
        }
    }

    private int frameRateCap;

    public void RefreshInventory()
    {
        GameObject inventorySlot = inventoryUI.transform.GetChild(3).gameObject;

        foreach (var (value, i) in player.GetComponent<PlayerController>().inventory.Select((value, i) => (value, i)))
        {
            if (value == null)
            {
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[0].callback.RemoveAllListeners();
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[1].callback.RemoveAllListeners();
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = false;
                inventorySlot.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = value.Icon;
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => CallTooltip(value));
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => CloseTooltip());
                inventorySlot.transform.GetChild(i).GetChild(0).GetComponent<Button>().interactable = true;
                inventorySlot.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void OpenInventory()
    {
        RefreshInventory();
        CloseTooltip();
        if (!inventoryUI.activeSelf)
        {

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
        Array.Sort(player.GetComponent<PlayerController>().inventory);

        player.GetComponent<PlayerController>().inventory = player.GetComponent<PlayerController>().inventory.OrderBy(x => x == null).ToArray();

        RefreshInventory();
    }

    public void EquipItem(int index)
    {
        if (player.GetComponent<PlayerController>().inventory[index].EquipTime != -1)
        {
            player.GetComponent<PlayerController>().EquipItem(index);
        }
    }

    public void ShowWarning(string text)
    {
        StopAllCoroutines();

        StartCoroutine(WarnTextCoroutine(text));
    }

    private IEnumerator WarnTextCoroutine(string text)
    {
        warningText.text = text;

        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        warningText.gameObject.SetActive(false);
    }
}

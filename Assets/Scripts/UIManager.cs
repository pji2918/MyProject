using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using pji2918.Input;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI itemName, itemDescription, scoreText, gunUsage, warningText, itemNameText, gunChargeText, debugInfoText;
    public GameObject detectedWarning, youDiedScreen, progressBarContainer, inventoryUI, tooltipUI, gunChargeUIContainer, crosshair;
    public Image progressBar;
    public RectTransform mouseBox;
    public GameObject itemUIWindows;
    public TextMeshProUGUI interactTextWindows;
    public GameObject itemUIMobile, mobileUI;
    public TextMeshProUGUI interactTextMobile;

    private GameObject itemUI;
    private TextMeshProUGUI interactText;
    private GameObject player;
    private Inputs input;
    private bool debugScreenOn = false;
    private int frameRateCap;

    public GameObject ItemUI
    {
        get
        {
            return itemUI;
        }
    }
    public TextMeshProUGUI InteractText
    {
        get
        {
            return interactText;
        }
    }

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
        input = new Inputs();
        input.Enable();
    }

    /// <summary>
    /// 현재 플레이어가 사용하는 입력에 따라 상호작용 텍스트를 반환합니다.
    /// </summary>
    /// <param name="key">눌러야 하는 키를 입력합니다. Windows 환경에서는 반드시 제공되어야 합니다.</param>
    /// <param name="longPress">길게 눌러야 하는가를 입력합니다.</param>
    /// <returns>매우 굉장한 상호작용 텍스트를 마법같이 만들어 반환합니다.</returns>
    /// <exception cref="ArgumentNullException">Windows 환경에서 눌러야 하는 키를 제공하지 않을 경우 발생합니다.</exception>
    public string GetInteractText(string key = null, bool longPress = false)
    {
        switch (player.GetComponent<PlayerController>().input.currentControlScheme)
        {
            case "PC":
                {
                    if (key == null)
                    {
                        throw new System.ArgumentNullException(key, "Windows 환경에서는 눌러야 하는 키를 제공해야 합니다.");
                    }
                    else if (longPress)
                    {
                        return string.Format("{0}을 길게 눌러", key);
                    }
                    else
                    {
                        return string.Format("{0}을 눌러", key);
                    }
                }
            case "Mobile":
                {
                    return "상호작용하여";
                }
            default:
                {
                    return key;
                }
        }
    }

    public void ShowOrHideDebugScreen()
    {
        debugScreenOn = !debugScreenOn;

        debugInfoText.gameObject.SetActive(debugScreenOn);
    }

    public void TakeScreenshot()
    {
        ScreenCapture.CaptureScreenshot(string.Format("{0}.png", System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        player.GetComponent<PlayerController>().input.neverAutoSwitchControlSchemes = true;
        player.GetComponent<PlayerController>().input.defaultControlScheme = "Mobile";
        player.GetComponent<PlayerController>().input.SwitchCurrentControlScheme("Mobile");
#endif

        if (player.GetComponent<PlayerController>().input.currentControlScheme == "Mobile" || player.GetComponent<PlayerController>().input.actions["isTouched"].IsPressed() || Application.isMobilePlatform)
        {
            itemUI = itemUIMobile;
            interactText = interactTextMobile;

            mobileUI.SetActive(true);
        }
        else if (player.GetComponent<PlayerController>().input.currentControlScheme == "PC")
        {
            itemUI = itemUIWindows;
            interactText = interactTextWindows;

            mobileUI.SetActive(false);
        }

        if (inventoryUI.activeSelf)
        {
            mouseBox.position = input.System.MousePos.ReadValue<Vector2>();
        }

        if (input.System.Screenshot.WasPressedThisFrame())
        {
            TakeScreenshot();
        }

        if (input.System.Debug.WasPressedThisFrame())
        {
            ShowOrHideDebugScreen();
        }

        if (input.System.Escape.WasPressedThisFrame())
        {
            Exit();
        }

        if (debugScreenOn)
        {
            frameRateCap = Application.targetFrameRate;

            if (frameRateCap == -1)
            {
                debugInfoText.text = string.Format("FPS: {0:0}\nDisplay: {1}x{2}", 1 / Time.smoothDeltaTime, Screen.width, Screen.height);
            }
            else
            {
                debugInfoText.text = string.Format("FPS: {0:0} / {1}\nDisplay: {2}x{3}", 1 / Time.smoothDeltaTime, frameRateCap, Screen.width, Screen.height);
            }
        }
    }

    public void DisableInputSystem()
    {
        input.Disable();
        input.Dispose();
    }

    public void RefreshInventory()
    {
        GameObject inventorySlot = inventoryUI.transform.GetChild(1).GetChild(3).gameObject;

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

            FindFirstObjectByType<PlayerController>().controllable = false;
        }
        else
        {
            inventoryUI.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            FindFirstObjectByType<PlayerController>().controllable = true;
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
        System.Array.Sort(player.GetComponent<PlayerController>().inventory);

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

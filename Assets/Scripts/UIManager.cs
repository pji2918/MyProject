using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

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
        // 시작 시에도 현재 사용 가능한 입력을 강제로 판별 (InputSystem.onDeviceChange가 시작 시 호출되지 않는 플랫폼 대비)
        DetectCurrentDevice();
    }

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        // 장치 변경 콜백 외에 최초 감지를 강제 실행
        DetectCurrentDevice();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public enum Device
    {
        KeyboardAndMouse,
        Touch,
        Gamepad
    }

    public Device currentDevice;

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Keyboard || device is Mouse)
        {
            currentDevice = Device.KeyboardAndMouse;
        }
        else if (device is Touchscreen)
        {
            currentDevice = Device.Touch;
        }
        else if (device is Gamepad)
        {
            currentDevice = Device.Gamepad;
        }
        else
        {
            // 알 수 없는 디바이스 변경 시에도 한 번 판별 실행
            DetectCurrentDevice();
        }
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
        if (IsTouchDevice())
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
        else
        {
            return "상호작용하여";
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
        // 모바일 감지 로직을 중앙화된 판별 함수로 교체
        if (IsTouchDevice())
        {
            itemUI = itemUIMobile;
            interactText = interactTextMobile;

            mobileUI.SetActive(true);
        }
        else
        {
            itemUI = itemUIWindows;
            interactText = interactTextWindows;

            mobileUI.SetActive(false);
        }

        if (inventoryUI.activeSelf)
        {
            mouseBox.position = InputSystem.actions["Point"].ReadValue<Vector2>();
        }

        if (InputSystem.actions["Screenshot"].WasPressedThisFrame())
        {
            TakeScreenshot();
        }

        if (InputSystem.actions["Debug"].WasPressedThisFrame())
        {
            ShowOrHideDebugScreen();
        }

        if (InputSystem.actions["Cancel"].WasPressedThisFrame())
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

            Cursor.lockState = CursorLockMode.Confined;
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

    /// <summary>
    /// 터치 입력 가능성을 포함해 현재 장치를 판별합니다.
    /// </summary>
    private void DetectCurrentDevice()
    {
        // Touchscreen.current 또는 InputSystem에 등록된 Touchscreen 장치가 있거나 모바일 플랫폼이면 터치로 판단
        if (Touchscreen.current != null || InputSystem.devices.Any(d => d is Touchscreen) || Application.isMobilePlatform)
        {
            currentDevice = Device.Touch;
            return;
        }

        // Gamepad가 연결된 경우
        if (Gamepad.current != null || InputSystem.devices.Any(d => d is Gamepad))
        {
            currentDevice = Device.Gamepad;
            return;
        }

        // 키보드/마우스가 확인되면 그쪽으로 설정, 아니면 기본값으로 설정
        if (Keyboard.current != null || Mouse.current != null || InputSystem.devices.Any(d => d is Keyboard || d is Mouse))
        {
            currentDevice = Device.KeyboardAndMouse;
            return;
        }

        // 최후 보류: 기본적으로 키보드/마우스
        currentDevice = Device.KeyboardAndMouse;
    }

    /// <summary>
    /// 현재 상태가 터치 기반 UI를 보여줘야 하는지 여부를 반환합니다.
    /// </summary>
    private bool IsTouchDevice()
    {
        return currentDevice == Device.Touch
            || Touchscreen.current != null
            || InputSystem.devices.Any(d => d is Touchscreen)
            || Application.isMobilePlatform;
    }
}

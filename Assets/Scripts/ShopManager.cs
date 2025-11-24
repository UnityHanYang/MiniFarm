using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private GameObject shopUI;

    [Header("Menu Settings")]
    [SerializeField] private MenuUI groundUI;
    [SerializeField] private MenuUI cropsUI;
    [SerializeField] private MenuUI waterUI;

    private readonly Color selectedColor = new Color(246 / 255f, 225 / 255f, 156 / 255f);
    private readonly Color normalColor = Color.white;

    private MenuUI[] allMenus;

    private void Awake()
    {
        allMenus = new MenuUI[] { groundUI, cropsUI, waterUI };
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void OpenGroundMenu() => OpenMenu(0);
    public void OpenCropsMenu() => OpenMenu(1);
    public void OpenWaterMenu() => OpenMenu(2);

    private void OpenMenu(int menuIndex)
    {
        if (allMenus[menuIndex].menu.activeSelf)
            return;

        for (int i = 0; i < allMenus.Length; i++)
        {
            bool isSelected = (i == menuIndex);
            allMenus[i].menu.SetActive(isSelected);
            allMenus[i].focus.SetActive(isSelected);
            allMenus[i].text.color = isSelected ? selectedColor : normalColor;
        }
    }

    [System.Serializable]
    public class MenuUI
    {
        public GameObject menu;
        public GameObject focus;
        public TextMeshProUGUI text;
    }
}
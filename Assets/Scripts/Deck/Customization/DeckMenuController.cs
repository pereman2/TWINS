using UnityEngine;
using UnityEngine.UI;

public class DeckMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject decksMenu;
    [SerializeField]
    private GameObject customizationMenu;

    public void OpenCustomization()
    {
        GameObject custom = Instantiate(customizationMenu, decksMenu.transform);
        custom.transform.SetParent(transform);
        custom.SetActive(true);
    }

    public GameObject OpenCustomizationEdit()
    {
        GameObject custom = Instantiate(customizationMenu, decksMenu.transform);
        Text ButtonText = GameObject.Find("CreateButtonText").GetComponent<Text>();

        ButtonText.text = "Save";
        custom.transform.SetParent(transform);
        custom.SetActive(true);

        return custom;
    }

    private void SwapMenus(GameObject oldMenu, GameObject newMenu)
    {
        ToggleMenu(oldMenu);
        ToggleMenu(newMenu);
    }

    private void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}

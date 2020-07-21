using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CategorizeDropdown : MonoBehaviour
{
    [SerializeField]
    private DeckCustomization deckCustomization;

    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private Image uploadedCard;

    [SerializeField]
    private GameObject errorWrapper;
    [SerializeField]
    private Text errorText;

    public void SetImage(Sprite sprite)
    {
        uploadedCard.type = Image.Type.Tiled;
        uploadedCard.sprite = sprite;
    }

    public void SetCategories(List<string> deckCategories)
    {
        dropdown.ClearOptions();

        var dropdownCategories = deckCategories.Union(DeckDataBase.GetUniqueCategories());

        dropdown.AddOptions(dropdownCategories.ToList());
        dropdown.value = 0;
    }

    public void AddNewCategory(InputField field)
    {
        Dropdown.OptionData optionData = new Dropdown.OptionData() { text = field.text.ToUpper().Trim() };

        if (!CategoryAlreadyInDropdown(optionData))
        {
            dropdown.options.Add(optionData);
            dropdown.value = dropdown.options.Count - 1;
            dropdown.RefreshShownValue();
        }
        else
            DisplayErrorMessage("Category already exists");

        field.text = "";
    }

    private bool CategoryAlreadyInDropdown(Dropdown.OptionData category)
    {
        foreach (Dropdown.OptionData optionData in dropdown.options)
            if (optionData.text == category.text)
                return true;

        return false;
    }

    private void DisplayErrorMessage(string message)
    {
        errorWrapper.SetActive(true);
        errorText.text = message;
    }

    public void SelectCategory()
    {
        if (IsSelectionValid())
        {
            deckCustomization.AddCategory(dropdown.options[dropdown.value].text);
            deckCustomization.ToggleCategorizePopUp();
        }
        else
            DisplayErrorMessage("Category not selected");
    }

    private bool IsSelectionValid()
    {
        return dropdown.options.Count > dropdown.value && dropdown.options[dropdown.value].text != null;
    }
}

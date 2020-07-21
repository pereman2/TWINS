using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckCustomization : MonoBehaviour
{
    [SerializeField]
    private GameObject deckPreview;
    [SerializeField]
    private GameObject repeatedPopUp;
    [SerializeField]
    private GameObject categorize;
    [SerializeField]
    private GameObject nameIt;

    [SerializeField]
    private Image lastFrontImage;
    [SerializeField]
    private Image backImage;
    [SerializeField]
    private Image repeatedImage;
    [SerializeField]
    private InputField newName;

    private GameObject deckMenu;
    private DeckListController deckListController;

    private bool isBeingEdited;
    private string previousDeckName;
    
    private List<Sprite> cardsImages = new List<Sprite>();
    private List<string> categories = new List<string>();

    private void Start()
    {
        deckMenu = GameObject.Find("DeckMenu");
        deckListController = GameObject.Find("DeckList").GetComponent<DeckListController>();

        deckMenu.SetActive(false);
    }

    public void AddCategory(string category)
    {
        categories.Add(category);
    }

    public void EditDeck(Deck deck)
    {
        isBeingEdited = true;
        previousDeckName = deck.Name;

        RetrieveInformationFromDeck(deck);
    }

    private void RetrieveInformationFromDeck(Deck deck)
    {
        AddBackSideImage(deck.backSide);
        cardsImages = deck.CardsImages;
        categories = deck.Categories;
    }

    public void AddBackSideImage(Sprite backside)
    {
        backImage.type = Image.Type.Tiled;
        backImage.sprite = backside;
        ImageUtils.RescaleBackSideSprite(backside);
    }

    public void AddImageToDeck(Sprite newCardImage)
    {
        ImageUtils.RescaleCardImageSprite(newCardImage);

        if (IsImageRepeated(newCardImage))
        {
            repeatedImage.type = Image.Type.Tiled;
            repeatedImage.sprite = newCardImage;

            ToggleRepeatedPopUp();
        }
        else
        {
            lastFrontImage.type = Image.Type.Tiled;
            lastFrontImage.sprite = newCardImage;
            cardsImages.Add(newCardImage);

            OpenCategorize();
        }
    }

    private bool IsImageRepeated(Sprite newCardImage)
    {
        return cardsImages.Exists((sprite) => sprite.texture == newCardImage.texture);
    }

    private void ToggleRepeatedPopUp()
    {
        ToggleMenu(repeatedPopUp);
    }

    private void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    private void OpenCategorize()
    {
        CategorizeDropdown instance = categorize.GetComponent<CategorizeDropdown>();
        instance.SetImage(lastFrontImage.sprite);
        instance.SetCategories(categories);

        ToggleCategorizePopUp();
    }

    public void ToggleCategorizePopUp()
    {
        ToggleMenu(categorize);
    }

    public void BackToDeckMenu()
    {
        deckMenu.SetActive(true);
        deckListController.PopulateDeckList();
        Destroy(gameObject);
    }

    public void FinishDeckCreation()
    {
        if (newName.text != "")
        {
            SaveDeck();
            ToggleNameItCanvas();
        }
    }

    private void SaveDeck()
    {
        if (IsDeckElegible())
        {
            Deck deck = new Deck(newName.text, backImage.sprite, cardsImages, categories);

            if (isBeingEdited)
                DeckDataBase.Modify(deck, previousDeckName);
            else
                DeckDataBase.SaveDeck(deck);

            BackToDeckMenu();
        }
    }

    private bool IsDeckElegible()
    {
        return backImage != null && cardsImages.Count >= 4
            && categories.Count == cardsImages.Count;
    }

    public void ToggleNameItCanvas()
    {
        if(cardsImages.Count > 3) ToggleMenu(nameIt);
    }

    public void OpenPreview()
    {
        DeckPreview instance = deckPreview.GetComponent<DeckPreview>();
        instance.SetCards(cardsImages);
        instance.PopulatePreviewCanvas();

        TogglePreview();
    }

    public void TogglePreview()
    {
        ToggleMenu(deckPreview);
    }

    public void ShowCardImageBrowser()
    {
        TWINSFileBrowser fileBrowser = new TWINSFileBrowser(this);
        fileBrowser.ShowDialog(true);
        fileBrowser.SetWindowBehind(gameObject);

        gameObject.SetActive(false);
    }

    public void ShowBackgroundImageBrowser()
    {
        TWINSFileBrowser fileBrowser = new TWINSFileBrowser(this);
        fileBrowser.ShowDialog(false);
        fileBrowser.SetWindowBehind(gameObject);

        gameObject.SetActive(false);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckPreview : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    private List<Sprite> cardsImages;

    public void SetCards(List<Sprite> cardsImages)
    {
        this.cardsImages = cardsImages;
    }

    public void PopulatePreviewCanvas()
    {
        ResetCards();

        foreach (Sprite sprite in cardsImages)
        {
            GameObject imagePreview = new GameObject();
            imagePreview.transform.parent = canvas.transform;

            Image image = imagePreview.AddComponent<Image>();
            image.sprite = sprite;
            image.type = Image.Type.Tiled;

            imagePreview.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void ResetCards()
    {
        foreach (Transform child in canvas.transform)
            Destroy(child.gameObject);
    }
}

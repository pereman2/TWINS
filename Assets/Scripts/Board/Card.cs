using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject frontsideCanvas;

    [SerializeField]
    private int pairID;
    public int PairID
    {
        get { return pairID; }
        set { pairID = value; }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    [SerializeField]
    private string category;
    public string Category
    {
        get { return category; }
        set { category = value; }
    }

    private bool isFlippedToFrontside;
    private bool ableToFlip = true;


    void Start()
    {
        Assert.IsNotNull(animator, "The animator reference on Card is null. Check if it's assigned on the editor.");
        Assert.IsNotNull(frontsideCanvas, "The front side canvas on Card is null. Check if it's assigned on the editor.");
        Assert.IsNotNull(sprite, "The sprite on Card is null. Check if it's assigned on the editor.");
        Assert.AreNotEqual(PairID, 0, "The pair ID for this card is not setted. The pair ID must be greater than 0");

        SetCardSpriteToCanvas();
    }

    public void SetCardSpriteToCanvas()
    {
        frontsideCanvas.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnMouseDown()
    {
        if (LevelManager.Instance.AbleToFlip())
            FlipCard();
    }

    public void FlipCard()
    {
        if (ableToFlip && !LevelManager.Instance.IsGamePaused())
        {
            EventChannel.Send(new CustomEvent(CustomEvent.EventType.cardFlips));
            if (!isFlippedToFrontside)
                FlipCardToFrontside();
            else
                FlipCardToBackside();
        }
    }

    public void ForceFlipCard()
    {
        ableToFlip = true;
        FlipCard();
    }

    private void FlipCardToFrontside()
    {
        animator.Play("cardFrontFlip");
        frontsideCanvas.SetActive(true);
        isFlippedToFrontside = true;
        EventChannel.Send(new CustomEvent(CustomEvent.EventType.cardFlipsToFrontside, this));
    }

    private void FlipCardToBackside()
    {
        animator.Play("cardBackFlip");
        frontsideCanvas.SetActive(false);
        isFlippedToFrontside = false;
    }

    public void DisableFlip()
    {
        ableToFlip = false;
    }

    public void EnableFlip()
    {
        ableToFlip = true;
    }
}

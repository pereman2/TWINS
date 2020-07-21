using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDeck
{
    public string Name { get; set; }
    public byte[] BackSide { get; set; }
    public List<byte[]> CardsImages { get; set; }
    public List<string> Categories { get; }

    private int[] backSideDimensions;
    private List<int[]> cardsImagesDimensions;

    public SerializableDeck(Deck deck)
    {
        Name = deck.Name;
        BackSide = GetSerializableSprite(deck.backSide);
        backSideDimensions = GetBackSideDimensions(deck.backSide);
        CardsImages = GetSerializableSprites(deck.CardsImages);
        cardsImagesDimensions = GetImagesDimensions(deck.CardsImages);
        Categories = deck.Categories;
    }

    private int[] GetBackSideDimensions(Sprite backside)
    {
        int[] dimensions = { backside.texture.height, backside.texture.width };
        return dimensions;
    }

    private List<byte[]> GetSerializableSprites(List<Sprite> sprites)
    {
        List<byte[]> serializableSprites = new List<byte[]>();

        foreach (Sprite sprite in sprites)
            serializableSprites.Add(GetSerializableSprite(sprite));

        return serializableSprites;
    }

    private byte[] GetSerializableSprite(Sprite sprite)
    {
        Texture2D tex = sprite.texture;
        return tex.EncodeToPNG();
    }

    private List<int[]> GetImagesDimensions(List<Sprite> cardsImages)
    {
        List<int[]> imagesDimensions = new List<int[]>();

        foreach (Sprite cardImage in cardsImages)
        {
            int[] imageDimensions = { cardImage.texture.height, cardImage.texture.width };
            imagesDimensions.Add(imageDimensions);
        }

        return imagesDimensions;
    }

    public Deck ToCommonDeck()
    {
        return new Deck(Name, GetSpriteFromBytes(BackSide, backSideDimensions), GetSpritesFromBytes(CardsImages), Categories);
    }

    private Sprite GetSpriteFromBytes(byte[] serializedSprite, int[] dimensions)
    {
        Texture2D tex = new Texture2D(dimensions[0], dimensions[1], TextureFormat.ARGB32, false);
        tex.LoadImage(serializedSprite);
        tex.Apply();
        tex.EncodeToPNG();

        return Sprite.Create(tex, new Rect(new Vector2(0, 0), new Vector2(dimensions[1], dimensions[0])), new Vector2(0.5f, 0.5f));
    }

    private List<Sprite> GetSpritesFromBytes(List<byte[]> serializedSprites)
    {
        List<Sprite> sprites = new List<Sprite>();

        for (int i = 0; i < serializedSprites.Count; i++)
        {
            byte[] serializedSprite = serializedSprites[i];
            int[] dimensions = cardsImagesDimensions[i];

            sprites.Add(GetSpriteFromBytes(serializedSprite, dimensions));
        }

        return sprites;
    }
}

using System.IO;
using UnityEngine;

public class ImageUtils
{
    public static Sprite GetSpriteFrom(string path)
    {
        return GetSpriteFrom(GetTextureFrom(path));
    } 

    public static Sprite GetSpriteFrom(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100);
    }

    public static Texture2D GetTextureFrom(string path)
    {
        var fileData = File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.LoadImage(fileData);
        tex.Apply();

        return tex;
    }

    public static void RescaleBackSideSprite(Sprite backside)
    {
        backside.texture.wrapMode = TextureWrapMode.Clamp;
        TextureScale.Bilinear(backside.texture, 150, 200);
    }

    public static void RescaleCardImageSprite(Sprite cardImage)
    {
        cardImage.texture.wrapMode = TextureWrapMode.Clamp;
        TextureScale.Bilinear(cardImage.texture, 120, 180);
    }
}

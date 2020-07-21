using System.Collections;
using UnityEngine;
using SimpleFileBrowser;

public class TWINSFileBrowser
{
    private GameObject windowBehind = null;
    private DeckCustomization requester;

    public TWINSFileBrowser(DeckCustomization requester)
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));

        this.requester = requester;
    }

    public void ShowDialog(bool isCardImage)
    {
        FileBrowser.ShowLoadDialog((path) =>
            {
                if (isCardImage)
                    requester.AddImageToDeck(ImageUtils.GetSpriteFrom(path));
                else
                    requester.AddBackSideImage(ImageUtils.GetSpriteFrom(path));

                ActiveWindowBehind();
            },
            () => { ActiveWindowBehind(); },
            false, null, "Select Folder", "Select");
    }

    public void SetWindowBehind(GameObject gameObject)
    {
        windowBehind = gameObject;
    }

    public void ActiveWindowBehind()
    {
        windowBehind.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Page : MonoBehaviour, ISlide
{
    int Id;
    Texture RenderTexture;

    public Page InitPage(int id, Texture texture)
    {
        Id = id;
        RenderTexture = texture;
        return this;
    }
    public virtual void EnterPage()
    {
        Debug.Log("Enter page");
    }
    public virtual void ExitPage()
    {
        Debug.Log("Exit page");
    }

    public SlideInfo GetSlideInfo() => SlideInfo.InitSlide(Id, RenderTexture);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppHandler : MonoBehaviour
{
    public List<SlideInfo> Textures = new List<SlideInfo>();
    public SlideshowHandler SlideshowHandler;
    public ContentHandler ContentHandler;

    private void Awake()
    {
        ContentHandler.InitContent(Textures);
        SlideshowHandler.InitSlideshow();
    }
}


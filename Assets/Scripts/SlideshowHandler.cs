using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideshowHandler : MonoBehaviour
{
    public Dictionary<int, Texture> TexturesMap = new Dictionary<int, Texture>();
    public RawImage LeftImage;
    public RawImage RightImage;
    float FadeValue;
    [Range(0, 1)]
    public float FadeScale;
    public void InitSlideshow()
    {
        ISlide[] slides=GetComponentsInChildren<ISlide>();
        for(int i = 0; i < slides.Length; i++)
        {
            SlideInfo slider=slides[i].GetSlideInfo();
            TexturesMap.Add(slider.Id, slider.RenderTexture);
        }
        FadeValue = 1 / (float)(TexturesMap.Count - 1);
    }
    public Texture GetTexture(int id)
    {
        if (TexturesMap.TryGetValue(id, out Texture texture)) return texture;
        return null;

    }
    private void Update()
    {
        if (TexturesMap.Count == 0) return;
        float currentValue = (TexturesMap.Count - 1) * FadeScale;
        EdgeValues EdgeValues = EdgeValues.CreateEdges(currentValue, 0, TexturesMap.Count - 1);
        float leftVal = EdgeValues.RightEdge - currentValue; 
        float rightVal = currentValue - EdgeValues.LeftEdge;
        LeftImage.texture = TexturesMap[EdgeValues.LeftEdge];
        RightImage.texture = TexturesMap[EdgeValues.RightEdge];
        Color leftColor = LeftImage.color;
        Color rightColor = RightImage.color;
        LeftImage.color = new Color(leftColor.r, leftColor.g, leftColor.b, leftVal);
        RightImage.color = new Color(rightColor.r, rightColor.g, rightColor.b, rightVal);
    }

}
public interface ISlide
{
    public SlideInfo GetSlideInfo();
}
[System.Serializable]
public struct SlideInfo
{
    public int Id;
    public Texture RenderTexture;

    public SlideInfo(int id, Texture renderTexture)
    {
        Id = id;
        RenderTexture = renderTexture;
    }
    public static SlideInfo InitSlide(int id, Texture renderTexture) => new SlideInfo(id, renderTexture);
}
[System.Serializable]
public struct EdgeValues
{
    public int LeftEdge;
    public int RightEdge;

    public EdgeValues(int leftEdge, int rightEdge)
    {
        LeftEdge = leftEdge;
        RightEdge = rightEdge;
    }

    public static EdgeValues CreateEdges(float currentValue, int min, int max)
    {
        int value = (int)currentValue;
        if (currentValue >= max)
            return new EdgeValues(value - 1, value);
        return new EdgeValues(value, value + 1);
    }
}

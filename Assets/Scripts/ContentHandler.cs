using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ContentHandler : MonoBehaviour, IDragHandler, IScrollHandler
{
    private RectTransform selffRectTransform;
    private RectTransform pageContent;
    private int pagesCount;
    private int currentPage=0;
    private float pageHeight=0;
    private Vector2 goToPosition;

    public float moveSpeed=5;
    public float swipeIntensity=5;


    private void Start()
    {
        selffRectTransform = GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(selffRectTransform);
        if (transform.childCount == 0) return;
        pageContent = transform.GetChild(0).GetComponent<RectTransform>();
        pageHeight = pageContent.rect.height;
        pagesCount = (int)(selffRectTransform.rect.height / pageHeight);
        currentPage = 0;
        Debug.Log("total pages: "+pagesCount);
        Debug.Log("current page: "+ currentPage);
    }
    private void Update()
    {
        PageCount();
        getInput();
        selffRectTransform.anchoredPosition = Vector2.Lerp(selffRectTransform.anchoredPosition, goToPosition, Time.deltaTime* moveSpeed);
    }
    private void PageCount()
    {
        float currentPosition = selffRectTransform.anchoredPosition.y + pageHeight / 2;
        if (currentPosition < pageHeight * currentPage && currentPage > 0)
        {
            currentPage--;
            Debug.Log("Previous page");
        }
        else if (currentPosition > pageHeight * (currentPage + 1) && currentPage < pagesCount-1)
        {
            currentPage++;
            Debug.Log("Next page");
        }
    }
    #region Input
    public void OnDrag(PointerEventData eventData)
    {
        goToPosition += new Vector2(0, eventData.delta.y * swipeIntensity);
    }

    public void OnScroll(PointerEventData eventData)
    {
        bool isPositive = -eventData.scrollDelta.y > 0;
        if (currentPage < pagesCount - 1 && isPositive)
        {
            currentPage++;
        }
        else if(currentPage > 0 && !isPositive)
        {
            currentPage--;
        }
        goToPosition= new Vector2(0, pageHeight * currentPage);
    }

    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            goToPosition = selffRectTransform.anchoredPosition;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            goToPosition = new Vector2(0, pageHeight * currentPage);
        }
    }
    #endregion
}

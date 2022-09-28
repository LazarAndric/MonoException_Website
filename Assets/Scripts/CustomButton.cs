using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CustomButton : Button
{
    Image AnimationImage;
    Sequence AnimationSequence;
    protected override void Start()
    {
        AnimationImage =transform.GetChild(0).GetComponent<Image>();
        base.Start();
        AnimationSequence = DOTween.Sequence();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClick(()=> base.OnPointerClick(eventData));
    }
    public void OnButtonClick(Action onDone)
    {
        AnimationSequence.Kill();
        AnimationImage.rectTransform.DOKill();
        AnimationImage.rectTransform.sizeDelta = Vector2.zero;
        AnimationImage.transform.position = Input.mousePosition;
        AnimationSequence = FadeIn(onDone).Append(FadeOut());
    }
    public virtual Sequence FadeIn(Action OnDone)
    {
        return DOTween.Sequence().Append(AnimationImage.rectTransform.DOSizeDelta(gameObject.GetComponent<RectTransform>().sizeDelta * 2, 0.9f))
            .Join(AnimationImage.DOFade(1, 0.9f))
            .SetEase(Ease.Linear)
            .InsertCallback(0.5f, () =>
            {
                OnDone?.Invoke();
            }
        );
    }
    public virtual Sequence FadeOut()
    {
        return DOTween.Sequence().Append(AnimationImage.DOFade(0, 0.9f))
            .SetEase(Ease.Flash);
    }
    public void ResetButton()
    {
        AnimationSequence.Kill();
        AnimationImage.rectTransform.DOKill();
        AnimationImage.rectTransform.sizeDelta = Vector2.zero;
        AnimationImage.rectTransform.anchoredPosition = Vector2.one * 2000;
    }
}

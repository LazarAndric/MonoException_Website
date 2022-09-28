using UnityEngine;

public class IntroPage : Page
{
    public CustomButton animationClick;
    public CustomButton animationClick1;
    public override void EnterPage()
    {
        base.EnterPage();
    }
    public override void ExitPage()
    {
        base.ExitPage();
        animationClick.ResetButton();
        animationClick1.ResetButton();
    }
}

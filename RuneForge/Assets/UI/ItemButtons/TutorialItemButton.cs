using UnityEngine;

public class TutorialItemButton : ItemButton
{
    private TutorialDialogue tutorialDialogue;

    private void Awake()
    {
        GameObject TutorialManager = GameObject.Find("TutorialManager");
        if (!TutorialManager)
            Debug.LogError("TutorialButton created without a TutorialManager");
        else
            tutorialDialogue = TutorialManager.GetComponent<TutorialDialogue>();
    }

    public void TutorialPush()
    {
        
        tutorialDialogue.ComplexButtonActivate(this.item);
    }

    override public void OnHover(bool active)
    {
        if (showHover)
        {
            if (active)
            {
                HoverInfo.Load();
                HoverInfo.instance.DisplayItem(this);
            }
            else
            {
                HoverInfo.instance.Hide();
            }
        }
    }
}
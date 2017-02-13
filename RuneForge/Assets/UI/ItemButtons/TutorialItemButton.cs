using UnityEngine;

public class TutorialItemButton : ItemButton
{
    private TutorialDialogue tutorialDilogue;

    private void Awake()
    {
        GameObject TutorialManager = GameObject.Find("TutorialManager");
        if (!TutorialManager)
            Debug.LogError("TutorialButton created without a TutorialManager");
        else
            tutorialDilogue = TutorialManager.GetComponent<TutorialDialogue>();
    }

    public void TutorialPush()
    {
        
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
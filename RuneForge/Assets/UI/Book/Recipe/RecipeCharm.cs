using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecipeCharm : MonoBehaviour
{
    public Image charmIcon;
    public Text countText;
    string ingredient;
    int count;

    public void Initialize(string ingredient, int count)
    {
        this.ingredient = ingredient;
        this.count = count;
        charmIcon.sprite = Resources.Load<Sprite>(string.Format("ItemSprites/Charms/{0} Charm", ingredient));
        countText.text = string.Format("x{0}", count);
    }

    public void OnHover(bool active)
    {
        if (active)
        {
            HoverInfo.Load();
            HoverInfo.instance.DisplayText(charmIcon.gameObject, ingredient);
        }
        else
        {
            HoverInfo.instance.Hide();
        }
    }
}

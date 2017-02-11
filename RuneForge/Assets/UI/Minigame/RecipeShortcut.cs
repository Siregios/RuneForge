using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeShortcut : MonoBehaviour {
    
    public BookUI book;

    public void ShowRecipe()
    {
        book.DisplayRecipe();
    }
}

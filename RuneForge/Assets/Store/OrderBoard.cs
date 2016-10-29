using UnityEngine;
using System.Collections;

public class OrderBoard : MonoBehaviour {
    public OrderBoardUI orderPanel;

    void OnMouseDown()
    {
        orderPanel.DisplayBoard();
    }
}

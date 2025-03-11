using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemDescription;


    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.tootipUI.ShowTooltp(itemDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.tootipUI.HideTooltip();
    }

}

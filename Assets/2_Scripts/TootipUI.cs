using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TootipUI : MonoBehaviour
{
    public GameObject tooltipPanel;  // ÅøÆÁ ÆÐ³Î
    public TextMeshProUGUI tooltipText;         // ÅøÆÁ ³»¿ë
    public RectTransform tooltipRectTransform; // ÅøÆÁ À§Ä¡ Á¶Á¤
    public Condition lanchGage;

    private void Awake()
    {
        GameManager.Instance.tootipUI = this;
        tooltipPanel.SetActive(false);
    }

    public void ShowTooltp(string message)
    {
        tooltipText.text = message;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 mousePos = Input.mousePosition;
        }
    }


}

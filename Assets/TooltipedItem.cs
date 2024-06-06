using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TooltipedItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI txtMain;
    [SerializeField] TextMeshProUGUI txtTooltip;
    [SerializeField] GameObject panel;

    public void SetText(string main, string tooltip)
    {
        txtMain.text = main;
        txtTooltip.text = tooltip;
    }

    public void SetTooltip(string msg)
    {
        txtTooltip.text = msg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}

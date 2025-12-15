using TMPro;
using UnityEngine;

public class CandleUI : MonoBehaviour
{
    public CandleManager candleManager;
    public TMP_Text candleText;
    public Color normalColor;
    public Color openColor;
    public string originText = "{0} / {1}";
    public string allLightedText = "The path is open";

    void Update()
    {
        if (candleManager == null || candleText == null) return;
        int lighted = candleManager.GetLitCount();
        int total = candleManager.candles.Length;

        if (lighted >= total)
        {
            candleText.text = allLightedText;
            candleText.color = openColor;
        }
        else
        {
            candleText.text = string.Format(originText, lighted, total);
            candleText.color = normalColor; // ← important
        }
    }
}


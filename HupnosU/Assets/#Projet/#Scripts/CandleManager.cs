using UnityEngine;

public class CandleManager : MonoBehaviour
{
    public Candles[] candles;
    public GameObject exit;

    void Update()
    {
        if (candles == null || candles.Length == 0) return;
        int lightedCount = 0;
        foreach (var candle in candles)
            if (candle != null && candle.IsLighted) lightedCount++;

        if (lightedCount >= candles.Length && exit != null && !exit.activeSelf)
        {
            exit.SetActive(true);
        }
    }

    public int GetLitCount()
    {
        int lightedCount = 0;
        foreach (var candle in candles)
            if (candle != null && candle.IsLighted) lightedCount++;

        return lightedCount;
    }
}




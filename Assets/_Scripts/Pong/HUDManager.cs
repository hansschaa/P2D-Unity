using System;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public TextMeshProUGUI lifesText;
    public TextMeshProUGUI pointsText;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public void UpdateLifesText(int lifes) {

        lifesText.text = "Vidas: " + lifes.ToString();
    }

    internal void UpdatePointsText(int points)
    {
        pointsText.text = "Puntos: " + points.ToString();
    }
}

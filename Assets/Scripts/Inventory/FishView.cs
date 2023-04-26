using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishView : MonoBehaviour
{
    public Fish fish;
    public FishViewManager fm;
    public FishActiveToggle activeToggle;

    public TMP_Text nameText;
    public TMP_Text dateText;
    public TMP_Text colorText;
    public SpriteRenderer fishSprite;
    public SpriteRenderer fishColorSprite;
    public SpriteRenderer colorBox;

    void Start()
    {
        fm = GameObject.Find("FishViewPage").GetComponent<FishViewManager>();
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = fish.fishName;
        dateText.text = fish.dateObtained;
        colorText.text = "#" + ColorUtility.ToHtmlStringRGB(fish.fishColor);

        fishSprite.sprite = fish.GetComponent<SpriteRenderer>().sprite;
        colorBox.color = fish.fishColor;

        fishColorSprite.sprite = fish.colorSprite.sprite;
        fishColorSprite.color = fish.fishColor;
    }

    public void Sell()
    {
        if(fish.isActive)
        {
            activeToggle.toggleActive();
        }
        fm.SellFish(fish);
    }
}
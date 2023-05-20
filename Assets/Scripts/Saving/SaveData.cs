using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int pearls = 0;

    public List<FishData> fishInventory = new List<FishData>();
    public List<FishHistoryData> fishHistory = new List<FishHistoryData>();

    public List<DecorData> plants = new List<DecorData>();
    public List<DecorData> rocks = new List<DecorData>();

    public List<ActiveDecorData> activeDecor = new List<ActiveDecorData>();

    public int currentBg = 0;

    public SaveData(Game g)
    {
        // pearls
        pearls = g.pearls;

        // fish
        foreach (Fish f in g.fishInventory)
        {
            FishData fishData = new FishData(f);
            fishInventory.Add(fishData);
        }

        // fish history
        foreach (FishHistoryRecord r in g.fishHistory)
        {

        }

        // plant data
        foreach (DecorManager d in g.decorInventory.plants)
        {
            DecorData decorData = new DecorData(d);
            plants.Add(decorData);
        }

        // rock data
        foreach (DecorManager d in g.decorInventory.rocks)
        {
            DecorData decorData = new DecorData(d);
            rocks.Add(decorData);
        }

        // active decor data
        foreach (GameObject d in g.activeDecor)
        {
            ActiveDecorData activeDecorData = new ActiveDecorData(d);
            activeDecor.Add(activeDecorData);
        }        

        // background
        currentBg = g.bg.currentBackground;

    }
}
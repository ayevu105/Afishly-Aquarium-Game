using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public Game game;
    public GameObject parentObject;
    public Dictionary<string, DecorManager> decorManagers;

    public DecorManager Plant_1;
    public DecorManager Plant_2;
    public DecorManager Plant_3;
    public DecorManager Rock_1;
    public DecorManager Rock_2;
    public DecorManager Rock_3;

    private Renderer buttonRenderer;
    private Color originalColor;


    private void Update()
    {
        bool anyChildSelected = false;

        PlantRock[] allPlantRocks = parentObject.GetComponentsInChildren<PlantRock>(true);

        foreach (PlantRock plantRock in allPlantRocks)
        {
            if (plantRock.selected)
            {
                anyChildSelected = true;
                break;
            }
        }

        if (anyChildSelected)
        {
            SetOpacity(1f);
        }
        else
        {
            SetOpacity(0f);
        }
    }

    private void SetOpacity(float alpha)
    {
        Color color = buttonRenderer.material.color;
        color.a = alpha;
        buttonRenderer.material.color = color;
    }

    private void Start()
    {
        // Initialize the decorManagers dictionary
        decorManagers = new Dictionary<string, DecorManager>();
        decorManagers.Add("Plant_1(Clone)", Plant_1);
        decorManagers.Add("Plant_2(Clone)", Plant_2);
        decorManagers.Add("Plant_3(Clone)", Plant_3);
        decorManagers.Add("Rock_1(Clone)", Rock_1);
        decorManagers.Add("Rock_2(Clone)", Rock_2);
        decorManagers.Add("Rock_3(Clone)", Rock_3);

        buttonRenderer = GetComponent<Renderer>();
        originalColor = buttonRenderer.material.color;
    }

    private void OnMouseDown()
    {
        // Get all child objects of the parent object, including nested children
        PlantRock[] childPlantRocks = parentObject.GetComponentsInChildren<PlantRock>(true);

        foreach (PlantRock plantRock in childPlantRocks)
        {
            if (plantRock.selected)
            {
                // Get the parent object of the selected child
                GameObject parent = plantRock.transform.parent.gameObject;
                
                string name = parent.name;
                DecorManager decorManager;
                if (decorManagers.TryGetValue(name, out decorManager))
                {
                    game.activeDecor.Remove(parent);
                    // Destroy the parent object
                    Destroy(parent);
                    // Call the storeDecor function in the DecorManager script
                    decorManager.storeDecor();
                }
            }
        }
    }
}
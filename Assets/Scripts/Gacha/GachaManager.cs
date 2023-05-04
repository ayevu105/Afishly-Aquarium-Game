using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GachaManager : MonoBehaviour
{
    public Game game;

    public GameObject transitionScreen;
    public float fadeTime = .5f;

    private Material screenMaterial;
    private Color screenColor;

    public GameObject GachaBoxes;
    public List<GachaBox> boxes;

    public GachaBox selectedBox;
    public TMP_Text selectedText;
    public TMP_Text costText;

    public GameObject FishParentObject;
    string spriteAsset = "<sprite name=\"Pearl\">";

    // Start is called before the first frame update
    void Start()
    {
        // auto adds all gacha boxes within the GachaBoxes object
        Transform[] children = GachaBoxes.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            boxes.Add(child.gameObject.GetComponent<GachaBox>());
        }
        boxes.RemoveAt(0); // removes empty parent object from list
        selectedBox = boxes[0];

        selectedBox.gameObject.SetActive(true);
        for(int i = 1; i < boxes.Count; i++)
        {
            boxes[i].gameObject.SetActive(false);
        }
        
        // Get the Material of the transition screen object
        Renderer screenRenderer = transitionScreen.GetComponent<Renderer>();
        screenMaterial = screenRenderer.material;

        // Get the initial color of the Material
        screenColor = screenMaterial.color;

        // Set the alpha value to 0 to make the object transparent
        screenColor.a = 0f;
        screenMaterial.color = screenColor;
    }

    // Update is called once per frame
    void Update()
    {
        selectedText.text = selectedBox.gachaName;
        costText.text = "Open\n " + selectedBox.cost + " " + spriteAsset+ "";
    }

    public void ScrollLeft()
    {
        selectedBox.gameObject.SetActive(false);
        if (boxes.IndexOf(selectedBox) == 0)
        {
            selectedBox = boxes[boxes.Count - 1];
        }
        else
        {            
            selectedBox = boxes[(boxes.IndexOf(selectedBox) - 1)];
        }
        selectedBox.gameObject.SetActive(true);
    }

    public void ScrollRight()
    {
        selectedBox.gameObject.SetActive(false);
        if (boxes.IndexOf(selectedBox) == boxes.Count-1)
        {
            selectedBox = boxes[0];
        }
        else
        {
            selectedBox = boxes[(boxes.IndexOf(selectedBox) + 1)];
        }
        selectedBox.gameObject.SetActive(true);
    }

    public void buyBox()
    {
        if (game.pearls >= selectedBox.cost)
        {
            game.pearls -= selectedBox.cost;
            Fish newFish = selectedBox.OpenBox();
            Fish f = Instantiate(newFish, FishParentObject.transform);
            game.fishInventory.Add(f);

            transitionScreen.SetActive(true);

            // creates a temp fish for display
            GameObject tempDisplayFish = Instantiate(newFish, transitionScreen.transform).gameObject;
            tempDisplayFish.SetActive(true);
            tempDisplayFish.GetComponent<Fish>().setFishColor();
            tempDisplayFish.GetComponent<Fish>().colorSprite.color = tempDisplayFish.GetComponent<Fish>().fishColor;

            tempDisplayFish.GetComponent<SpriteRenderer>().sortingLayerName = "Transition";
            tempDisplayFish.GetComponent<Fish>().colorSprite.sortingLayerName = "Transition";
            tempDisplayFish.GetComponent<Fish>().enabled = false; // disables movement script from fish

            tempDisplayFish.transform.localPosition = new Vector3(0, 0, -3);
            tempDisplayFish.transform.localScale = new Vector3(1.05f, 1.5f, 1.5f);

            transitionScreen.GetComponent<TransitionScript>().displayFish = tempDisplayFish;

            StartCoroutine(FadeInTransitionScreen());
        }
    }

    IEnumerator FadeInTransitionScreen()
    {
        float elapsedTime = 0f;

        yield return new WaitForSeconds(1f);

        GameObject displayFish = transitionScreen.GetComponent<TransitionScript>().displayFish;
        displayFish.SetActive(true);
        
        //Color fishOutlineColor = displayFish.GetComponent<SpriteRenderer>().color;
        //Color fishColor = displayFish.GetComponent<Fish>().colorSprite.color;
        //fishColor.a = 0f;

        // Set the alpha value to 0 to make the object transparent
        screenColor.a = 0f;
        screenMaterial.color = screenColor;

        // Gradually increase the alpha value over time
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            screenColor.a = alpha;
            screenMaterial.color = screenColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // display tap to continue text
        transitionScreen.GetComponent<TransitionScript>().continueText.SetActive(true);

        // Set the alpha value to 1 to make the object opaque
        screenColor.a = 1f;
        screenMaterial.color = screenColor;
    }
}

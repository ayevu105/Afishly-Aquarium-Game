using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishActiveToggle : MonoBehaviour
{
    public bool isActive = true;
    public FishView box;

    public Sprite activeSprite;
    public Sprite inactiveSprite;

    // public GameObject sliderCircle;

    // add fish sound
    public AudioSource addFishSFX;

    // remove fish sound
    public AudioSource removeFishSFX;

    // Start is called before the first frame update
    void Start()
    {
        isActive = box.fish.isActive;

        addFishSFX = GameObject.Find("FishAddSFX").GetComponent<AudioSource>();
        removeFishSFX = GameObject.Find("FishRemoveSFX").GetComponent<AudioSource>();

        /*
        if(isActive)
        {
            sliderCircleOn();
        } 
        else
        {
            sliderCircleOff();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            gameObject.GetComponent<Image>().sprite = activeSprite;
        } else
        {
            gameObject.GetComponent<Image>().sprite = inactiveSprite;
        }
    }

    public void toggleActive()
    {
        if (isActive)
        {
            if(removeFishSFX != null)
            {
                removeFishSFX.PlayOneShot(removeFishSFX.clip);
            }
            

            box.fish.isActive = false;
            box.fish.gameObject.SetActive(false);
            isActive = false;
            box.fm.activeCount--;
            box.fm.game.activeFish.Remove(box.fish);

            // move slider circle
            //sliderCircleOff();
        }
        else if (box.fm.game.activeFish.Count < box.fm.game.activeFishMax)
        {
            addFishSFX.PlayOneShot(addFishSFX.clip);

            box.fish.isActive = true;
            box.fish.gameObject.SetActive(true);
            isActive = true;
            box.fm.activeCount++;
            box.fm.game.activeFish.Add(box.fish);

            // move slider circle
            //sliderCircleOn();
        }

        // sorting
        FilterBoxManager filter = box.fm.filter;
        filter.SelectTab(filter.curSelectedTab);
    }

    /*
    void sliderCircleOff()
    {
        Vector3 startPosition = sliderCircle.transform.localPosition;
        Vector3 targetPosition = new Vector3((float)-0.568, startPosition.y, startPosition.z);
        sliderCircle.transform.localPosition = targetPosition;
    }

    void sliderCircleOn()
    {
        Vector3 startPosition = sliderCircle.transform.localPosition;
        Vector3 targetPosition = new Vector3((float)0.568, startPosition.y, startPosition.z);
        sliderCircle.transform.localPosition = targetPosition;
    }
    */
}
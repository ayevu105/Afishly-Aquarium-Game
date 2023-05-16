using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundButtonManager : MonoBehaviour
{
    public BackgroundManager bm;
    public List<GameObject> selectedSprites;

    // Start is called before the first frame update
    void Start()
    {
        SelectBackground(0); // selected original background
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBackground(int bg)
    {
        bm.setBackground(bg);
        
        for(int i = 0; i < selectedSprites.Count; i++)
        {
            if(i == bg)
            {
                selectedSprites[i].SetActive(true);
            }
            else
            {
                selectedSprites[i].SetActive(false);
            }
        }
    }
}
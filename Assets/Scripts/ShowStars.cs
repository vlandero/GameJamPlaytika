using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStars : MonoBehaviour { 

    [SerializeField] GameObject star_FULL_0;
    [SerializeField] GameObject star_FULL_1;
    [SerializeField] GameObject star_FULL_2; 
    
    [SerializeField] GameObject star_EMPTY_0;
    [SerializeField] GameObject star_EMPTY_1;
    [SerializeField] GameObject star_EMPTY_2;

    public int starNumber;

    // Start is called before the first frame update

    public void Start()
    {
        ShowFullStars();
    }
    public void ShowFullStars()
    {
        if (starNumber == 0)
        {
            star_FULL_0.SetActive(false);
            star_FULL_1.SetActive(false);
            star_FULL_2.SetActive(false);

            star_EMPTY_0.SetActive(true);
            star_EMPTY_1.SetActive(true);
            star_EMPTY_2.SetActive(true);

        }

        if (starNumber == 1)
        {
            star_FULL_0.SetActive(true);
            star_FULL_1.SetActive(false);
            star_FULL_2.SetActive(false);

            star_EMPTY_0.SetActive(false);
            star_EMPTY_1.SetActive(true);
            star_EMPTY_2.SetActive(true);

        }

        if (starNumber == 2)
        {
            star_FULL_0.SetActive(true);
            star_FULL_1.SetActive(true);
            star_FULL_2.SetActive(false);

            star_EMPTY_0.SetActive(false);
            star_EMPTY_1.SetActive(false);
            star_EMPTY_2.SetActive(true);

        }

        if (starNumber == 3)
        {
            star_FULL_0.SetActive(true);
            star_FULL_1.SetActive(true);
            star_FULL_2.SetActive(true);

            star_EMPTY_0.SetActive(false);
            star_EMPTY_1.SetActive(false);
            star_EMPTY_2.SetActive(false);
    
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlace : MonoBehaviour
{
    public GameObject[] places;

    // Start is called before the first frame update
    void Start()
    {
        int choice = Random.Range(-1, places.Length);
        if (choice != -1)
        {
            gameObject.transform.position = places[choice].transform.position;
            gameObject.transform.rotation = places[choice].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objective : MonoBehaviour
{
    public int obj_code = 0;
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.GetComponent<GameManager>().objective_is_done(obj_code);
    }
}

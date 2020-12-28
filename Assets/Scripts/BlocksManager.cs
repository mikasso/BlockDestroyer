using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject block;
    public int amount;

    void Start()
    {
        Instantiate(block, new Vector3(0, 6.0f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

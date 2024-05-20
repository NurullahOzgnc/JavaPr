using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InsantiateCharacter : MonoBehaviour
{
    public GameObject InstantiatedCharacter;
    public GameObject InstantiaonPosition;
    
    void Start()
    {
        Instantiate(InstantiatedCharacter,InstantiaonPosition.transform.position,Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

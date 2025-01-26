using System;
using UnityEngine;
public enum PlayerState
{
    Water,
    WaterSurface,
    Land,
}

public class PlayerTransition : MonoBehaviour
{
    private PlayerState currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = PlayerState.Water;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.CompareTag("Water"))
        {
            currentState = PlayerState.Water;
            Debug.Log("water state");
        }
        if (other.gameObject.CompareTag("WaterSurface"))
        {
            currentState = PlayerState.WaterSurface;
            Debug.Log("Water surface collision");
        }
    }
}



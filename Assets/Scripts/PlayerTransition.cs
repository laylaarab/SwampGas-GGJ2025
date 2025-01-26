using System.Collections;
using TMPro;
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
    public Canvas playerHUD;
    public TextMeshProUGUI playerHint;
    public EffectsOverlay effectsOverlay;
    public Transform teleportationPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = PlayerState.Water;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Water:
                break;
            case PlayerState.WaterSurface:
                playerHint.gameObject.SetActive(true);
                playerHint.text = "press [E] to go to ground";
                break;
            case PlayerState.Land:
                break;
        }


        if (Input.GetKeyDown(KeyCode.E) && currentState == PlayerState.WaterSurface)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    public IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        transform.position = teleportationPoint.position;
        effectsOverlay.FadeIn(1.0f);
        currentState = PlayerState.Land;
        yield return new WaitForSeconds(1.0f);
        playerHint.gameObject.SetActive(false);
        playerHint.text = "";
        effectsOverlay.FadeOut(1.0f);
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



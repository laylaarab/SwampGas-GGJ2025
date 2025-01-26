using System.Collections;
using Cinemachine;
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
    public Rigidbody rb;

    public CinemachineFreeLook freeFlyCamera;
    public CinemachineVirtualCamera groundCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeStateToWater();
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
        effectsOverlay.FadeIn(1.0f);
        yield return new WaitForSeconds(1.0f);
        transform.position = teleportationPoint.position;
        currentState = PlayerState.Land;
        yield return new WaitForSeconds(1.0f);
        playerHint.gameObject.SetActive(false);
        ChangeStateToLand();
        playerHint.text = "";
        effectsOverlay.FadeOut(1.0f);
    }

    void ChangeStateToLand()
    {
        currentState = PlayerState.Land;
        rb.useGravity = true;
        // enable player movement script
        GetComponent<Movement>().enabled = true;
        GetComponent<WaterPlayerController>().enabled = false;
        groundCamera.Priority = 20;
        freeFlyCamera.Priority = 0;
    }

    void ChangeStateToWater()
    {
        currentState = PlayerState.Water;
        playerHint.gameObject.SetActive(false);
        playerHint.text = "";
        rb.useGravity = false;
        GetComponent<Movement>().enabled = false;
        GetComponent<WaterPlayerController>().enabled = true;
        groundCamera.Priority = 0;
        freeFlyCamera.Priority = 20;
    }

    void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.CompareTag("Water") && currentState != PlayerState.Water)
        {
            ChangeStateToWater();
        }
        if (other.gameObject.CompareTag("WaterSurface"))
        {
            currentState = PlayerState.WaterSurface;
        } 
    }
}



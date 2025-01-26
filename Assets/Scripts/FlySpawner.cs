using UnityEngine;

public class FlySpawner : MonoBehaviour
{

    public GameObject flyPrefab;
    public BoxCollider boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i = 0; i < 150; i++) 
        {
            GameObject fly = Instantiate(flyPrefab, transform, worldPositionStays:false);

            // Randomize fly rotation
            fly.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0);

            // Set the fly's parent to this spawner
            fly.transform.SetParent(transform);
            fly.transform.localPosition  = new Vector3(Random.Range(-boxCollider.size.x / 2, boxCollider.size.x / 2), Random.Range(-boxCollider.size.y / 2, boxCollider.size.y / 2), Random.Range(-boxCollider.size.z / 2, boxCollider.size.z / 2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

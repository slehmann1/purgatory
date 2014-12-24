using UnityEngine;
using System.Collections;
public class CloudSpawner : MonoBehaviour
{

    public bool horizontallyFlipped = false;
    public float percentChance;
    public float minTime;
    public float minSpeed;
    public float maxSpeed;
    private float timeElapsed = 0.0f;
    public float posRandomness, yScaleRandomness, xScaleRandomness;
    public GameObject[] cloud;
    [Tooltip("Initial scaling")]
    public float initX, initY;
    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > minTime)
        {
            if (Random.Range(0.0f, 100.0f) >= percentChance)
            {
                GameObject c = (GameObject)Instantiate(cloud[Random.Range(0, cloud.Length)], new Vector3(transform.position.x, transform.position.y + Random.Range(-posRandomness, posRandomness), transform.position.z), Quaternion.identity);
                c.transform.parent = this.transform;
                
                Vector3 localScale= new Vector3((initX + Random.Range(-xScaleRandomness, xScaleRandomness)), (initY + Random.Range(-yScaleRandomness, yScaleRandomness)), 1);
                if (horizontallyFlipped)
                {
                    localScale.x *= -1;
                }
                c.transform.localScale = localScale;
                c.GetComponent<CloudMovement>().setSpeed(Random.Range(minSpeed, maxSpeed));
                c.layer = gameObject.layer;
                timeElapsed = 0.0f;
            }

            else
            {
                timeElapsed = 0.0f;
            }
        }
    }
}

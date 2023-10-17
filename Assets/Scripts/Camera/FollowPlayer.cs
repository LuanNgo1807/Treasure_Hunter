using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float camSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if(player.transform.position.x >= -1f)
        {
            float playerX = player.transform.position.x;
            float playerY = player.transform.position.y;
            Vector3 newCamPosition = transform.position;
            newCamPosition.x = playerX;
            newCamPosition.y = playerY + 0.5f;
            transform.position = Vector3.Lerp(transform.position, newCamPosition, camSpeed * Time.deltaTime);
        }
    }
    
}

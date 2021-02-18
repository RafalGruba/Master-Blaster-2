using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        var targetPosition = new Vector2(0, 5);
        var movementThisFrame = 1 * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<Level>().LoadNextScene();
    }
}

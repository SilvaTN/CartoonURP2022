using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    Vector3 positionUpdated;

    private void Start()
    {
        positionUpdated = player.position;

        if ((offset.x + offset.y + offset.z) <= 0)
        {
            offset = new Vector3(-14.4f, 7.1f, 15.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        positionUpdated.x = player.position.x;
        transform.position = positionUpdated + offset;
    }
}

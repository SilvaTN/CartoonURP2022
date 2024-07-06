using UnityEngine;

public class JumpVfxFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    private ParticleSystem ps;
    Vector3 positionUpdated;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        positionUpdated = player.position;

        if ((offset.x + offset.y + offset.z) <= 0)
        {
            offset = new Vector3(-14.4f, 7.1f, 15.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.isPlaying)
        {
            positionUpdated = player.position;
            transform.position = positionUpdated + offset;
        }

    }
}

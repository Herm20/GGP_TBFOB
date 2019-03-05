using UnityEngine;

/* The base for this prototype was made using a tutorial
 * by the Youtuber "Brackeys". It was then modified to match our needs.
 * Link to video series: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0*/

public class Enemy : MonoBehaviour
{
    public float speed;

    private Transform target;
    private int wavepointIndex = 0;

    public WaveSpawner ws;

    private void Start()
    {
        speed = 18.0f;
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}

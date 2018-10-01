using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge;
    public float smoothing;
    public float tilt;

    public Vector2 startWait; // Range
    public Vector2 maneuverTime; // Range
    public Vector2 maneuverWait; // Range

    public Boundary boundary;

    private float currentSpeed;
    private float maneuverTarget;
    private Rigidbody rigidBody;

    void Start()
    {
        StartCoroutine(Evade());
        rigidBody = GetComponent<Rigidbody>();
        currentSpeed = rigidBody.velocity.z;
    }
	
	void FixedUpdate()
    {
        float newMeneuver = Mathf.MoveTowards(rigidBody.velocity.x, maneuverTarget, Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newMeneuver, 0, currentSpeed);
        rigidBody.position = new Vector3
        (
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            maneuverTarget = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x); // Keeping our enemy spaceship inside the screen
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            maneuverTarget = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
}

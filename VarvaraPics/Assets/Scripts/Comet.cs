using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {
    //Time that takes in activate the shooting star after generation
    [Range(0f, 30.0f)]
    public float spawnTime = 4f;
    float currentSpawnTime;

    //This speed value to move the shooting star
    [Range(0.02f, 0.5f)]
    public float speed = 1f;
    float currentSpeed;

    //If not activated, the shooting star does not move
    public bool activated = false;

    public bool randomColor;
    public TrailRenderer trail;
    public SpriteRenderer body;

    Vector3 direction = new Vector3();

    // Use this for initialization
    void Start () {
        Generate();
    }

    public void Generate()
    {
        //Deactivate the shoting star
        Activate(false);
        //Randomize spawn time and speed
        currentSpawnTime = Random.Range(0.3f, spawnTime);
        currentSpeed = Random.Range(0.3f, speed);
        if (randomColor) {
            Color color = Random.ColorHSV(0.16f, 0.9f, 0.5f, 0.8f, 0.85f, 1f);
            Debug.Log("1 " + color);
            body.color = color;

            //gradient color
            Gradient gradient = new Gradient();

            GradientColorKey[] gck = new GradientColorKey[trail.colorGradient.colorKeys.Length];
            GradientAlphaKey[] gak = new GradientAlphaKey[trail.colorGradient.colorKeys.Length];

            for (int i = 0; i < trail.colorGradient.colorKeys.Length; i++) {
                gck[i].color = color;
                gck[i].time = trail.colorGradient.colorKeys[i].time;

                gak[i].alpha = trail.colorGradient.alphaKeys[i].alpha;
                gak[i].time = trail.colorGradient.alphaKeys[i].time;
            }
            gradient.SetKeys(gck, gak);
            trail.colorGradient = gradient;
            Debug.Log("2 " + trail.colorGradient.colorKeys[0].color);

        }
        //Wait for currentSpawnTime to reactivate the shooting star
        StartCoroutine(waitToActivate(currentSpawnTime));
    }

    IEnumerator waitToActivate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //After waiting, activate the shooting star
        Activate(true);
    }

    public void Activate(bool activate)
    {
        activated = activate;
        if (activated)
        {
            //Once activated, the first action is to give the shooting star a new position
            Vector3 newPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(0f, 0.5f));

            transform.position = newPosition;
            //It defines the point to the shooting star will be pointing
            int directionMult = newPosition.x < 0 ? 1 : -1;

            direction = new Vector3(directionMult * Random.Range(0.2f, 1f), 0f, Random.Range(-0.2f, -1f));
            //Force the forwardDirection to don't change the position in the z axis
            //transform.forward = forwardDirection;
        }
    }

    void Update()
    {
        //If is not activated, don't update
        if (!activated) return;
        transform.position += direction * currentSpeed * Time.deltaTime;
        
        if (transform.position.z < -1f)
        {
            Generate();
        }
    }
}

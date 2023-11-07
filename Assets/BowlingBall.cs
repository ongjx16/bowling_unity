using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    public float force;
    // Use this for initialization
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    public TextMeshPro scoreText;
    private int currentFrame = 1;
    private int currentRoll = 1;
    private int totalScore = 0;
    private int[] frameScores;

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip collisionSound; // The audio clip to play on collision



    void Start()
    {
        var pins = GameObject.FindGameObjectsWithTag("Pin");
        pinPositions = new List<Vector3>();
        pinRotations = new List<Quaternion>();
        foreach (var pin in pins)
        {
            pinPositions.Add(pin.transform.position);
            pinRotations.Add(pin.transform.rotation);
        }

        ballPosition = GameObject.FindGameObjectWithTag("Ball").transform.position;

        frameScores = new int[10];
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -force));
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0), ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0), ForceMode.Impulse);
        if (Input.GetKeyUp(KeyCode.R))
        {
            var pins = GameObject.FindGameObjectsWithTag("Pin");

            for (int i = 0; i < pins.Length; i++)
            {
                //collision.gameObject.transform.parent.gameObject.tag
                var pinPhysics = pins[i].GetComponent<Rigidbody>();
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.position = pinPositions[i];
                pinPhysics.rotation = pinRotations[i];
                pinPhysics.velocity = Vector3.zero;
                pinPhysics.angularVelocity = Vector3.zero;

                var ball = GameObject.FindGameObjectWithTag("Ball");
                ball.transform.position = ballPosition;
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            var ball = GameObject.FindGameObjectWithTag("Ball");
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.transform.position = ballPosition;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Add your code for handling pin collisions and scoring here
            // Example:
            int pinsKnockedDown = CalculatePinsKnockedDown();
            UpdateScore(pinsKnockedDown);

            if (currentRoll == 2 || pinsKnockedDown == 10)
            {
                currentFrame++;
                currentRoll = 1;
            }
            else
            {
                currentRoll++;
            }

            // Play the collision sound
            if (pinsKnockedDown > 0)
            {
                audioSource.PlayOneShot(collisionSound);
            }

        }

       

    }

    int CalculatePinsKnockedDown()
    {
        int thresholdYPosition = 2;
        int pinsKnockedDown = 0;

        var pins = GameObject.FindGameObjectsWithTag("Pin");

        foreach (var pin in pins)
        {
            // Check a specific condition that represents a knocked-down pin
            // For example, you can check if the pin's y-position is below a certain threshold.
            if (pin.transform.position.y < thresholdYPosition)
            {
                pinsKnockedDown++;
            }
        }

        return pinsKnockedDown;
    }




    void UpdateScore(int pinsKnockedDown)
    {
        if (currentFrame <= 10) // Make sure you're within the valid frame range
        {
            frameScores[currentFrame - 1] += pinsKnockedDown;

            if (currentFrame > 1)
            {
                if (frameScores[currentFrame - 2] == 10)
                {
                    frameScores[currentFrame - 2] += pinsKnockedDown;
                }
            }

            totalScore = 0;
            for (int i = 0; i < currentFrame; i++)
            {
                totalScore += frameScores[i];
            }

            scoreText.text = "SCORE: " + totalScore;
        } else
        {
            scoreText.text = "GAME OVER!" + totalScore;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Pin")
           //GetComponent<AudioSource>().Play();
    }
}
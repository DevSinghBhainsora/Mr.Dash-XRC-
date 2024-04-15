using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public float leftRightSpeed = 4;
    public AudioSource coinCollectSound;
    public GameObject thePlayer;
    public GameObject charModel;
    private LevelDistance levelDistance;
    public AudioSource CrashThud;
    public GameObject LevelControl;
    public GameObject EndScreen;
    public GameObject StopUI;
    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject playerObject;

    void Start()
    {
        levelDistance = FindObjectOfType<LevelDistance>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            if (coinCollectSound != null)
            {
                CollactableControl.coinCount += 1;
                coinCollectSound.Play();
            }
            else
            {
                Debug.LogWarning("Coin collect sound is not assigned to the player!");
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("obstacle"))
        {
            levelDistance.StopDistanceCount();
            this.gameObject.GetComponent<Collider>().enabled = false;
            thePlayer.GetComponent<PlayerMove>().enabled = false;
            charModel.GetComponent<Animator>().Play("Stumble Backwards");
            LevelControl.GetComponent<LevelDistance>().enabled = false;
            CrashThud.Play();
            StopUI.SetActive(false);
            EndScreen.SetActive(true);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (isJumping == false)
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().Play("Jump");
                StartCoroutine(JumpSequence());
            }
        }
        if (isJumping == true)
            {
                if (comingDown == false)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World);
                }

                if (comingDown == true)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * -3, Space.World);
                }
            }

        IEnumerator JumpSequence()
        {
            yield return new WaitForSeconds(0.4f);
            comingDown = true;
            yield return new WaitForSeconds(0.4f);
            isJumping = false;
            comingDown = false;
            playerObject.GetComponent<Animator>().Play("Running");
        }
    }
}
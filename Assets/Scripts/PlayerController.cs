using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 direction;
    private int desiredLane = 1; // 0:left, 1:middle, 2: right

    public float forwardSpeed;
    public float maxSpeed;
    public float laneDistance;
    public float smoothSpeed;
    public float jumpForce;
    public float gravityForce;

    public Animator animator;

    private float appliedGravity; // the gravity that is used during update (for swipe down gesture)
    private bool isSliding;



    void Start() {
        controller = GetComponent<CharacterController>();
        appliedGravity = gravityForce;
        isSliding = false;
    }

    void Update() {
        if (GameManager.isGameStarted == false) {
            return;
        }

        if (forwardSpeed < maxSpeed) {
            forwardSpeed += Time.deltaTime;
        }

        animator.SetBool("isGameStarted", true);
        animator.SetBool("isGrounded", controller.isGrounded);

        direction.z = forwardSpeed;

        if (controller.isGrounded) {
            if (appliedGravity > gravityForce) {
                StartCoroutine(Slide());

            }
            appliedGravity = gravityForce;

            if (SwipeManager.swipeUp) {
                StopAllCoroutines();
                StopSlide();
                Jump();
            }

            if (SwipeManager.swipeDown && !isSliding) {
                StartCoroutine(Slide());
            }
        } else {
            if (SwipeManager.swipeDown) {
                appliedGravity = 10 * gravityForce;
            }

            direction.y -= appliedGravity * Time.deltaTime;
        }

        if (SwipeManager.swipeRight && desiredLane < 2) {
            desiredLane++;
        }

        if (SwipeManager.swipeLeft && desiredLane > 0) {
            desiredLane--;
        }

    }

    private void FixedUpdate() {
        if (GameManager.isGameStarted == false) {
            return;
        }

        controller.Move(direction * Time.fixedDeltaTime);

        Vector3 targetPosition = transform.position;

        if (desiredLane == 0) {
            targetPosition.x = -laneDistance;
        } else if (desiredLane == 2) {
            targetPosition.x = laneDistance;
        } else {
            targetPosition.x = 0f;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
    }

    private void Jump() {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.transform.tag == "Obstacle") {
            FindObjectOfType<AudioManager>().StopSound("Background");
            FindObjectOfType<AudioManager>().PlaySound("GameOver");

            GameManager.gameOver = true;
            Time.timeScale = 0;
        }
    }

    private IEnumerator Slide() {
        StartSlide();
        yield return new WaitForSeconds(1.3f);
        StopSlide();
    }

    private void StartSlide() {
        isSliding = true;
        animator.SetBool("isSliding", isSliding);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1f;
    }

    private void StopSlide() {
        isSliding = false;
        animator.SetBool("isSliding", isSliding);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2f;
    }

}

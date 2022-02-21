using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public float rotationSpeed;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            Destroy(gameObject);
            GameManager.numberOfCoins++;

            FindObjectOfType<AudioManager>().PlaySound("Coin");
        }
    }

}

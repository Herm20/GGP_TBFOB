using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public GameObject spellPrefab;
    public float power;
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Shoooot");
            GameObject spellObj = Instantiate(spellPrefab, this.transform.position + this.transform.forward, Quaternion.identity) as GameObject;
            spellObj.GetComponent<Rigidbody>().AddForce(spellObj.transform.forward * power);
        }
	}
}

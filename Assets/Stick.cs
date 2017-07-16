using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour {

    public GameObject character;

	void LateUpdate () {

        Vector2 newPos = character.transform.position;

        newPos.y -= .8F;

        transform.position = newPos;
        

	}
}

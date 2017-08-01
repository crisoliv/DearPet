using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick2 : MonoBehaviour
{

    public GameObject character;

    void LateUpdate()
    {

        Vector2 newPos = character.transform.position;

        newPos.y -= 1F;
        newPos.x -= 0.05F;

        transform.position = newPos;


    }
}

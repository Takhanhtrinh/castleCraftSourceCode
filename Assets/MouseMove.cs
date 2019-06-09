using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public GameObject m_sprite;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        int tempX = (int)mousePos.x / 16;
        int tempY = (int)mousePos.y / 16;
        mousePos.x = 16 * tempX;
        mousePos.y = 16 * tempY;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePos);

        newPos.z = m_sprite.transform.position.z;
        m_sprite.transform.position = newPos;
    }
	
    void OnTriggerEnter2D(Collider2D col)
    {

    }
    void OnTriggerExit2D(Collider2D col)
    {
		//m_sprite.GetComponent<SpriteRenderer>().enabled = false;
    }

}

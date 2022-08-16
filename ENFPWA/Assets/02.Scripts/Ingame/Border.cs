using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player_Atk") || collision.gameObject.tag.Equals("PyroBall") ||
            collision.gameObject.tag.Equals("ElectroBall") || collision.gameObject.tag.Equals("IceBall") ||
            collision.gameObject.tag.Equals("WaterBall") || collision.gameObject.tag.Equals("Obstacle"))
            collision.gameObject.SetActive(false);
    }
}

using UnityEngine;

public class UFOBillet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerShip = collision.GetComponent<PlayerShip>();
        {
            if (playerShip != null)
            {
                playerShip.GetDamage();
            }
        }
    }
}

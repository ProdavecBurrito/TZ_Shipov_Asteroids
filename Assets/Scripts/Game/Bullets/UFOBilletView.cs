using UnityEngine;

public class UFOBilletView : BaseBulletView
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerShip = collision.GetComponent<ShipView>();
        {
            if (playerShip != null)
            {
                playerShip.GetDamage();
            }
        }
    }
}

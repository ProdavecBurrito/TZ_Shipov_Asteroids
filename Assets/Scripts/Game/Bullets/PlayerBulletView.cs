using UnityEngine;

public class PlayerBulletView : BaseBulletView
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ufo = collision.GetComponent<UFOView>();
        {
            if (ufo != null)
            {
                ufo.GetDamage();
            }
        }
    }
}

using System;
using UnityEngine;

public class Basket : MonoBehaviour
{
    private Action<int> _onCollectApple;

    public void Init(Action<int> onCollectApple)
    {
        _onCollectApple = onCollectApple;
    }

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = transform.position;
        pos.x = mousePos3D.x;
        if (pos.x > 30)
            pos.x = 30;
        if (pos.x < -30)
            pos.x = -30;
        transform.position = pos;
    }

    void OnCollisionEnter (Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Apple")
        {
            Destroy(collidedWith);
            _onCollectApple.Invoke(100);
        }
    }
}

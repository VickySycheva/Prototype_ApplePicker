using System;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    public bool NeedToInvokeOnDestroy { get; set; }

    private Action _onOutOfBoundsAction;
    private Action<Apple> _onDestroy;

    public void Init(Action onOutOfBounds, Action<Apple> onDestroy)
    {
        _onOutOfBoundsAction = onOutOfBounds;
        _onDestroy = onDestroy;
        NeedToInvokeOnDestroy = true;
    }

    void Update()
    {
        if (transform.position.y < bottomY){
            NeedToInvokeOnDestroy = false;

            Destroy(this.gameObject);

            _onOutOfBoundsAction.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (NeedToInvokeOnDestroy)
        {
            _onDestroy.Invoke(this);
        }
    }
}

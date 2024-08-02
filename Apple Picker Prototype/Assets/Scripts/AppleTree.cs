using System;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public Apple applePrefab;

    public float speed = 10f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.02f;
    public float secondsBetweenAppleDrops = 1f;

    private ApplePicker _applePicker;
    private Action _onAppleOutOfBounds;

    private List<Apple> _apples;

    public List<Apple> GetApples() => _apples;

    public void Init(ApplePicker applePicker, Action onAppleDestroy)
    {
        _applePicker = applePicker;
        _onAppleOutOfBounds = onAppleDestroy;

        _apples = new List<Apple>();

        Invoke(nameof(DropApple), 2f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAppleTree();
    }

    void FixedUpdate()
    {
        if (UnityEngine.Random.value < chanceToChangeDirections)
        {
            speed *= -1; 
        }
    }

    public void DropApple()
    {
        Apple apple = Instantiate(applePrefab);
        _apples.Add(apple);
        apple.Init(OnAppleOutOfBounds, OnAppleDestroy);

        // apple.Init(delegate
        // {
        //     OnAppleDestroyMethod();
        //     _onAppleDestroy.Invoke();
        // });
        // apple.Init(() => OnAppleDestroyMethod());
        // apple.Init(() => _onAppleDestroy.Invoke());

        apple.transform.position = transform.position;
        Invoke(nameof(DropApple), secondsBetweenAppleDrops / _applePicker.LevelUP);
    }

    private void OnAppleOutOfBounds()
    {
        _onAppleOutOfBounds.Invoke();
    }

    private void OnAppleDestroy(Apple apple)
    {
        _apples.Remove(apple);
    }

    void MoveAppleTree()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime * _applePicker.LevelUP;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = - Mathf.Abs(speed);
        }
    }
}

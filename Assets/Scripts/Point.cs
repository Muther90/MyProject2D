using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Point : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Circle");
    }

    void Start()
    {
        _spriteRenderer.enabled = false;
    }
}

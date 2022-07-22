using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class StateImage : MonoBehaviour
{
    [SerializeField] private float _moveYDistance;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _fadeDuration;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        MoveVertical();
        FadeOut();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    private void MoveVertical()
    {
        gameObject.LeanMoveLocalY(transform.position.y + _moveYDistance, _moveDuration);
    }

    private void FadeOut()
    {
        gameObject.LeanAlpha(0f, _fadeDuration).setOnComplete(DestroyImage);
    }

    private void DestroyImage()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
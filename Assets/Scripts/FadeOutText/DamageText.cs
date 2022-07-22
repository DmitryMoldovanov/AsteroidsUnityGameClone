using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class DamageText : PooledObject<DamageText>
{
    [SerializeField] private Color _criticalDamageColor;
    [SerializeField] private Color _defaultDamageColor;
    [SerializeField] protected Vector3 _scaleTo;
    [SerializeField] protected float _scaleDuration;
    [SerializeField] protected float _fadeOutDuration;
    [SerializeField] protected float _moveYDistance;
    [SerializeField] protected float _criticalTextYOffset = 0.3f;
    [SerializeField] protected int _criticalTextFontSizeOffset = 5;

    private TextMesh _text;

    private int _defaultFontSize;
    
    private void Awake()
    {
        _text = GetComponent<TextMesh>();
        _defaultFontSize = _text.fontSize;
        _criticalTextFontSizeOffset = _defaultFontSize + _criticalTextFontSizeOffset;
    }

    public void ShowText(bool isCritical, Vector2 position)
    {
        if (isCritical)
        {
            _text.color = _criticalDamageColor;
            _text.fontSize = _criticalTextFontSizeOffset;
            SetPosition(new Vector2(position.x, position.y + _criticalTextYOffset));
        }
        else
        {
            _text.color = _defaultDamageColor;
            SetPosition(position);
        }

        LeanTween.scale(gameObject, _scaleTo, _scaleDuration);
        
        ResetScale();
        MoveUp();
        FadeOutText();
    }
    
    public void SetText(string text)
    {
        _text.text = "-" + text;
    }
    
    private void SetPosition(Vector2 position)
    {
        _text.transform.position = position;
    }

    private void ResetScale()
    {
        LeanTween.scale(gameObject, Vector3.one, _scaleDuration);
    }
    
    private void MoveUp()
    {
        LeanTween.moveLocalY(gameObject, transform.localPosition.y + _moveYDistance, _fadeOutDuration);
    }
    
    private void FadeOutText()
    {
        LeanTween.alpha(gameObject,0f,_fadeOutDuration).setOnComplete(ReturnToPool);
    }

    private void ReturnToPool()
    {
        ReturnToPool(this);
    }

    protected override void ResetObject()
    {
        LeanTween.alpha(gameObject, 1f, 0.01f);
        _text.fontSize = _defaultFontSize;
    }

    private void OnDisable()
    {
        ResetObject();
    }
}

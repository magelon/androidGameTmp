using UnityEngine;
using UnityEngine.UI;

public class TextLocalizer : MonoBehaviour
{
    public string key = "";

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        if (text == null || string.IsNullOrEmpty(key)) Destroy(this);
        Localization.Instance.onLocalize += OnLocalize;
    }

    void OnDestroy()
    {
        if (Localization.IsInstance) Localization.Instance.onLocalize -= OnLocalize;
    }

    void OnEnable()
    {
        OnLocalize();
    }

    private void OnLocalize()
    {
        text.text = Localization.Get(key);
    }
}

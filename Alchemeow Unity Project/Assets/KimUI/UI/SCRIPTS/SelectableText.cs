using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableText : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI textMesh;
    public bool showSelectors;
    [SerializeField]
    private float glowPower = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnHighlight()
    {
        textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);
    }
    public void OnDeHighlight()
    {
        textMesh.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0.0f);
    }
}

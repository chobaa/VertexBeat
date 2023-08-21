using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI stageText;

    private float power_level = 0.3f;
    private float multiple = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        power_level += Time.deltaTime * multiple;
        titleText.fontSharedMaterial.SetFloat(ShaderUtilities.ID_GlowPower, power_level);
        if (power_level > 0.95f || power_level < 0.05f) multiple *= -1f;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public ParabolaForce cubeObj;
    public Slider forceSlider;
    public TMP_InputField inputForce;



    void Start()
    {        
        forceSlider.onValueChanged.AddListener(slider);
        inputForce.text = forceSlider.value +"";
    }

    public void OnResetClick()
    {
        cubeObj.Reset();
        forceSlider.value = 1.6f;
    }

    public void slider(float val)
    {
        cubeObj.currentImpactMul = val;
        inputForce.text = val + "";
    }
}
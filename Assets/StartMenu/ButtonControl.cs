using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[ExecuteAlways]
public class ButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color color = Color.white;
    public Color BorderColor = Color.black;
    public Color HoverColor = Color.grey;
    public Color PressColor = Color.black;
    public float BorderThickness = 0.1F;
    public Material baseMaterial;

    public UnityEvent onClick;
    
    private Image _image;
    private Material _instancedMaterial;
    private Vector2 _buttonSize; 

    void OnValidate()
    {
        UpdateButton();
    }

    void Start()
    {
        UpdateButton();
    }

    private void UpdateButton()
    {
        if (_image == null) _image = GetComponent<Image>();
        if (baseMaterial == null) return;

        RectTransform rt = (RectTransform)transform;
        _buttonSize = rt.sizeDelta;

        if (_instancedMaterial == null || _instancedMaterial.shader != baseMaterial.shader)
        {
            _instancedMaterial = new Material(baseMaterial);
            _instancedMaterial.name = baseMaterial.name + " (Instance)";
            
            _image.material = _instancedMaterial;
        }

        _instancedMaterial.SetColor("_Tint", color);
        _instancedMaterial.SetColor("_HoverColor", HoverColor);
        _instancedMaterial.SetColor("_PressColor", PressColor);
        _instancedMaterial.SetColor("_BorderColor", BorderColor);
        _instancedMaterial.SetFloat("_BorderThickness", BorderThickness);
        _instancedMaterial.SetVector("_BSize", _buttonSize);
    }

    public void OnPointerEnter(PointerEventData eventData) => _instancedMaterial.SetFloat("_State", 1f); // Hover
    public void OnPointerExit(PointerEventData eventData)  => _instancedMaterial.SetFloat("_State", 0f); // Normal
    public void OnPointerDown(PointerEventData eventData)  =>  PressedAction(); // Pressed
    public void OnPointerUp(PointerEventData eventData)    => _instancedMaterial.SetFloat("_State", 1f); // Back to Hover

    void PressedAction(){
        _instancedMaterial.SetFloat("_State", 2f);
        if (onClick != null)
        {
            onClick.Invoke();
        }
    }

    void LoadScene(){

    }
}

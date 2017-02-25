using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SpriteGlow : MonoBehaviour
{
    public Color color = Color.white;

    [Range(0, 16)]
    public int outlineSize = 1;

    private SpriteRenderer spriteRenderer;
    private Image image;


    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        UpdateOutline(true);
    }

    void OnDisable()
    {
        UpdateOutline(false);
    }

    void LateUpdate()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        Sprite sprite = null;
        if (spriteRenderer != null)
            sprite = spriteRenderer.sprite;
        else if (image != null)
            sprite = image.sprite;

        if (sprite != null)
        {
            Vector4 result = new Vector4(sprite.textureRect.min.x / sprite.texture.width,
            sprite.textureRect.min.y / sprite.texture.height,
            sprite.textureRect.max.x / sprite.texture.width,
            sprite.textureRect.max.y / sprite.texture.height);

            //MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            //spriteRenderer.GetPropertyBlock(mpb);
            //mpb.SetFloat("_Outline", outline ? 1f : 0);
            //mpb.SetColor("_OutlineColor", color);
            //mpb.SetFloat("_OutlineSize", outlineSize);
            //mpb.SetVector("_Rect", result);
            //spriteRenderer.SetPropertyBlock(mpb);

            Material mat = null;
            if (spriteRenderer)
            {
                mat = spriteRenderer.material;
            }
            else if (image)
                mat = image.material;

            mat.SetFloat("_Outline", outline ? 1f : 0);
            mat.SetColor("_OutlineColor", color);
            mat.SetFloat("_OutlineSize", outlineSize);
            mat.SetVector("_Rect", result);

            //Debug.Log("Outline : " + result);
        }
    }

    public void EnableGlow()
    {
        GetComponentInChildren<SpriteGlow>().enabled = true;
    }

    public void DisableGlow()
    {
        GetComponentInChildren<SpriteGlow>().enabled = false;
    }

    public void EnableHoverAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("MouseHover", true);
    }

    public void DisableHoverAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("MouseHover", false);
    }

}
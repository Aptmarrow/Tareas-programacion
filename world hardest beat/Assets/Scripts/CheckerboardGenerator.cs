using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteAlways]
public class CheckerboardGenerator : MonoBehaviour
{
    public Color color1 = new Color(0.9f, 0.9f, 1f); // Celeste claro
    public Color color2 = new Color(0.8f, 0.8f, 0.95f); // Celeste más oscuro
    public int columns = 10;
    public int rows = 5;
    public int pixelsPerCell = 32;

    void Start()
    {
        GenerateCheckerboard();
    }

    void OnValidate()
    {
        GenerateCheckerboard();
    }

    void GenerateCheckerboard()
    {
        // Prevenir errores si los valores son muy chicos
        if (columns <= 0) columns = 1;
        if (rows <= 0) rows = 1;
        if (pixelsPerCell <= 0) pixelsPerCell = 1;

        int texWidth = columns * pixelsPerCell;
        int texHeight = rows * pixelsPerCell;
        
        Texture2D texture = new Texture2D(texWidth, texHeight);
        texture.filterMode = FilterMode.Point; // Para que los bordes sean nítidos (estilo pixel art)

        for (int y = 0; y < texHeight; y++)
        {
            for (int x = 0; x < texWidth; x++)
            {
                int cellX = x / pixelsPerCell;
                int cellY = y / pixelsPerCell;
                
                if ((cellX + cellY) % 2 == 0)
                    texture.SetPixel(x, y, color1);
                else
                    texture.SetPixel(x, y, color2);
            }
        }
        texture.Apply();

        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texWidth, texHeight), new Vector2(0.5f, 0.5f), pixelsPerCell);
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = newSprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class WiggleStartAndEnd
{
    public int startIndex;
    public int endIndex;

    public WiggleStartAndEnd(int startIndex, int endIndex)
    {
        this.startIndex = startIndex;
        this.endIndex = endIndex;
    }
};

public class CharacterWobble : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    List<WiggleStartAndEnd> wiggleStartAndEnds = new List<WiggleStartAndEnd>();
    int count = 0;
    //float refreshSpeed = 0.5f;

    string text;
    int indexInText = 0;
    float timer = 0;

    [SerializeField] float typeWriterEffectSpeed = 0.1f;
    [SerializeField] bool autoStart;


    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        text = "";

        if (autoStart)
            SetText(textMesh.text);
    }

    public void SetText(string newText)
    {
        wiggleStartAndEnds.Clear();

        text = newText;

        indexInText = 0;
        int startIndex = -1;
        int endIndex = -1;
        int totalRemoval = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '@' && startIndex == -1)
            {
                startIndex = i;
            }
            else if (text[i] == '@' && endIndex == -1)
            {
                endIndex = i;
                totalRemoval++;
                wiggleStartAndEnds.Add(new WiggleStartAndEnd(startIndex - totalRemoval, endIndex - totalRemoval));
                totalRemoval++;
                startIndex = -1;
                endIndex = -1;
            }
        }

        for (int i = text.Length - 1; i > 0; i--)
        {
            if (text[i] == '@')
            {
                text = text.Remove(i, 1);
            }
        }

        textMesh.text = "";
        count = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > typeWriterEffectSpeed)
        {
            if (indexInText < text.Length)
            {
                textMesh.text += text[indexInText];
                indexInText++;
                timer = 0;
            }
        }

        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < wiggleStartAndEnds.Count; i++)
        {
            Wiggle(wiggleStartAndEnds[i].startIndex, wiggleStartAndEnds[i].endIndex);
        }

        textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        count++;
        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(0, Mathf.Sin(time * 4.3f) * 4.3f);
    }

    void Wiggle(int startIndex, int endIndex)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            if (textMesh.text.Length - 1 < i)
                break;

            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            if (c.character == ' ')
                continue;

            int index = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            vertices[index + 0] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
           
            int meshIndex = textMesh.textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textMesh.textInfo.characterInfo[i].vertexIndex;
        }
    }

    public static Color32 hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}
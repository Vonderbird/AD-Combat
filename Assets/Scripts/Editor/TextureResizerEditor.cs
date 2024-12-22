using UnityEditor;
using UnityEngine;

public class TextureResizerWindow : EditorWindow
{
    private Texture2D selectedTexture;
    private int originalWidth;
    private int originalHeight;
    private int newWidth;
    private int newHeight;

    [MenuItem("Window/Texture Resizer")]
    public static void ShowWindow()
    {
        GetWindow<TextureResizerWindow>("Texture Resizer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Texture Resizer", EditorStyles.boldLabel);

        // Display selected texture
        selectedTexture = (Texture2D)EditorGUILayout.ObjectField("Selected Texture", selectedTexture, typeof(Texture2D), false);

        if (selectedTexture != null)
        {
            // Show original dimensions
            originalWidth = selectedTexture.width;
            originalHeight = selectedTexture.height;
            EditorGUILayout.LabelField("Original Width", originalWidth.ToString());
            EditorGUILayout.LabelField("Original Height", originalHeight.ToString());

            // Input fields for new dimensions
            newWidth = EditorGUILayout.IntField("New Width", newWidth);
            newHeight = EditorGUILayout.IntField("New Height", newHeight);

            // Resize button
            if (GUILayout.Button("Resize Texture"))
            {
                if (newWidth > 0 && newHeight > 0)
                {
                    ResizeSelectedTexture();
                }
                else
                {
                    EditorUtility.DisplayDialog("Invalid Dimensions", "Width and Height must be greater than zero.", "OK");
                }
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Select a texture to resize.", MessageType.Info);
        }
    }


    private void ResizeSelectedTexture()
    {
        string path = AssetDatabase.GetAssetPath(selectedTexture);

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Could not determine the texture path. Ensure it is part of the project assets.");
            return;
        }

        // Duplicate the texture to avoid modifying the original asset directly
        Texture2D originalTexture = new Texture2D(selectedTexture.width, selectedTexture.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(selectedTexture, originalTexture);

        // Create a new Texture2D for the resized image
        Texture2D resizedTexture = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);

        // Perform nearest-neighbor scaling
        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                // Map the new pixel to the closest pixel in the original texture
                int origX = Mathf.RoundToInt((float)x / newWidth * originalTexture.width);
                int origY = Mathf.RoundToInt((float)y / newHeight * originalTexture.height);

                // Clamp to avoid out-of-bound errors
                origX = Mathf.Clamp(origX, 0, originalTexture.width - 1);
                origY = Mathf.Clamp(origY, 0, originalTexture.height - 1);

                Color pixelColor = originalTexture.GetPixel(origX, origY);
                resizedTexture.SetPixel(x, y, pixelColor);
            }
        }
        resizedTexture.Apply();

        // Save the resized texture to the same path
        byte[] bytes = resizedTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);

        // Refresh the asset database to reload the updated texture
        AssetDatabase.ImportAsset(path);
        Debug.Log($"Texture resized to {newWidth}x{newHeight} using nearest-neighbor scaling and saved to {path}");

        // Update the reference to the resized texture
        selectedTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
    }


    //private void ResizeSelectedTexture()
    //{
    //    string path = AssetDatabase.GetAssetPath(selectedTexture);

    //    if (string.IsNullOrEmpty(path))
    //    {
    //        Debug.LogError("Could not determine the texture path. Ensure it is part of the project assets.");
    //        return;
    //    }

    //    // Duplicate the texture to avoid modifying the original asset directly
    //    Texture2D originalTexture = new Texture2D(selectedTexture.width, selectedTexture.height, TextureFormat.RGBA32, false);
    //    Graphics.CopyTexture(selectedTexture, originalTexture);

    //    // Create a new Texture2D for the resized image
    //    Texture2D resizedTexture = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);

    //    // Perform manual scaling using nearest-neighbor or sharp filtering
    //    for (int y = 0; y < newHeight; y++)
    //    {
    //        for (int x = 0; x < newWidth; x++)
    //        {
    //            // Map the new pixel to the original texture
    //            float u = x / (float)newWidth;
    //            float v = y / (float)newHeight;
    //            Color pixelColor = originalTexture.GetPixelBilinear(u, v);
    //            resizedTexture.SetPixel(x, y, pixelColor);
    //        }
    //    }
    //    resizedTexture.Apply();

    //    // Save the resized texture to the same path
    //    byte[] bytes = resizedTexture.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(path, bytes);

    //    // Refresh the asset database to reload the updated texture
    //    AssetDatabase.ImportAsset(path);
    //    Debug.Log($"Texture resized to {newWidth}x{newHeight} and saved to {path}");

    //    // Update the reference to the resized texture
    //    selectedTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
    //}


    //private void ResizeSelectedTexture()
    //{
    //    string path = AssetDatabase.GetAssetPath(selectedTexture);

    //    if (string.IsNullOrEmpty(path))
    //    {
    //        Debug.LogError("Could not determine the texture path. Ensure it is part of the project assets.");
    //        return;
    //    }

    //    // Create a new RenderTexture with the specified size
    //    RenderTexture rt = new RenderTexture(newWidth, newHeight, 24);
    //    rt.filterMode = FilterMode.Bilinear; // Apply bilinear filtering for better quality
    //    RenderTexture.active = rt;

    //    // Render the original texture onto the RenderTexture
    //    Graphics.Blit(selectedTexture, rt);

    //    // Create a new Texture2D to read from the RenderTexture
    //    Texture2D resizedTexture = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);

    //    // Read the RenderTexture pixels into the Texture2D
    //    resizedTexture.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
    //    resizedTexture.Apply();

    //    // Release the active RenderTexture
    //    RenderTexture.active = null;
    //    rt.Release();

    //    // Encode the resized texture to PNG to preserve quality
    //    byte[] bytes = resizedTexture.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(path, bytes);

    //    // Refresh the asset database to reload the updated texture
    //    AssetDatabase.ImportAsset(path);
    //    Debug.Log($"Texture resized to {newWidth}x{newHeight} and saved to {path}");

    //    // Update the reference to the resized texture
    //    selectedTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
    //}


    //private void ResizeSelectedTexture()
    //{
    //    string path = AssetDatabase.GetAssetPath(selectedTexture);

    //    if (string.IsNullOrEmpty(path))
    //    {
    //        Debug.LogError("Could not determine the texture path. Ensure it is part of the project assets.");
    //        return;
    //    }

    //    // Create a RenderTexture for resizing
    //    RenderTexture rt = new RenderTexture(newWidth, newHeight, 24);
    //    RenderTexture.active = rt;

    //    // Draw the texture onto the RenderTexture
    //    Graphics.Blit(selectedTexture, rt);

    //    // Create a new Texture2D with the specified size
    //    Texture2D resizedTexture = new Texture2D(newWidth, newHeight, selectedTexture.format, false);
    //    resizedTexture.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
    //    resizedTexture.Apply();

    //    RenderTexture.active = null;
    //    rt.Release();

    //    // Save the resized texture to the same path
    //    byte[] bytes = resizedTexture.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(path, bytes);

    //    // Refresh the asset database
    //    AssetDatabase.ImportAsset(path);
    //    Debug.Log($"Texture resized to {newWidth}x{newHeight} and saved to {path}");

    //    // Update the reference to the resized texture
    //    selectedTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
    //}
}

/* Add all the layers on the 'layersToAdd' array if they're not already in the layers list
 */

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class InitializeLayers : Editor
{
    static InitializeLayers()
    {
        // Open layer manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        // Layers to add
        string[] layersToAdd = { "Ground" };

        // Check if layers are not already present & add if not
        foreach (string layerToAdd in layersToAdd)
        {
            bool layerFound = false;
            int emptyLayerIndex = -1;
            for (int i = 0; i < layersProp.arraySize; i++)
            {
                SerializedProperty layer = layersProp.GetArrayElementAtIndex(i);
                if (layer.stringValue == "" && emptyLayerIndex == -1 && i >= 8)
                {
                    emptyLayerIndex = i;
                }
                if (layer.stringValue.Equals(layerToAdd))
                {
                    layerFound = true;
                    break;
                }
            }

            // not found => add layer
            if (!layerFound)
            {
                SerializedProperty n = layersProp.GetArrayElementAtIndex(emptyLayerIndex);
                n.stringValue = layerToAdd;
                tagManager.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}


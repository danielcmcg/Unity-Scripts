/* Add all the tags on the 'tagsToAdd' array if they're not already in the tags list
 * Working on Unity 2018
 */

using UnityEditor;

[InitializeOnLoad]
public class InitializeTags : Editor
{
    static InitializeTags()
    {
        // Open tag manager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // Tags to add
        string[] tagsToAdd = { "ExampleTag1", "ExampleTag2", "ExampleTag3" };

        // Check if tags are not already present & add if not
        foreach (string tagToAdd in tagsToAdd)
        {
            bool tagFound = false;
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty tag = tagsProp.GetArrayElementAtIndex(i);
                if (tag.stringValue.Equals(tagToAdd))
                {
                    tagFound = true;
                    break;
                }
            }

            // not found => add tag
            if (!tagFound)
            {
                tagsProp.InsertArrayElementAtIndex(0);
                SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
                n.stringValue = tagToAdd;
                tagManager.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeleteEditorOnly
{
    [MenuItem("GameObject/Delete Editor Only Objects", true)]
    public static bool ValidateDeleteEditorOnlyObjects()
    {
        return Selection.activeGameObject != null;
    }

    [MenuItem("GameObject/Delete Editor Only Objects", false)]
    static void DeleteEditorOnlyObjects()
    {
        DeleteEditorOnlyObjects(Selection.gameObjects);
    }

    static void DeleteEditorOnlyObjects(IEnumerable<GameObject> objects)
    {
        var destroyObjects = new List<GameObject>();
        foreach (var obj in objects)
        {
            if (obj.CompareTag("EditorOnly"))
            {
                destroyObjects.Add(obj);
            }
            else
            {
                var children = new GameObject[obj.transform.childCount];
                for (var i = 0; i < obj.transform.childCount; ++i)
                {
                    children[i] = obj.transform.GetChild(i).gameObject;
                }
                DeleteEditorOnlyObjects(children);
            }
        }
        foreach (var obj in destroyObjects)
        {
            Undo.DestroyObjectImmediate(obj);
        }
    }
}

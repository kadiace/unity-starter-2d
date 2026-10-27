using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogError($"Fail to load prefab - path: {path}");
            return null;
        }

        if (prefab.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(path, parent);

        GameObject instance = Object.Instantiate(prefab, parent);
        instance.name = Util.RemoveUnityCloneSuffix(instance.name);
        return instance;
    }

    public void Destroy(GameObject target)
    {
        if (target.GetComponent<Poolable>() != null)
        {
            Managers.Pool.Push(target);
            return;
        }

        Object.Destroy(target);
    }
}

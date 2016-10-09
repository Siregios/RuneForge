using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public static class XmlReader<T>
{
    public static T Load(string path)
    {
        TextAsset Xml = Resources.Load<TextAsset>(path);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(Xml.text);
        T result = (T)serializer.Deserialize(reader);
        reader.Close();
        return result;
    }
}

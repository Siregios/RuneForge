using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public static class LocationManager{

    //public static Dictionary<string, Location> map = new Dictionary<string, Location>();
    //static Map map = XmlReader<Map>.Load("MapData/LocationMap");
    public static List<Location> locationList = XmlReader<Map>.Load("MapData/LocationMap").locations;
   
    public static Dictionary<string, Location> locationDict = new Dictionary<string, Location>();

    static LocationManager()
    {
        foreach (Location location in locationList)
        {
            foreach (string pairString in location.connectionStr.Split('/'))
            {
                var pair = pairString.Split(':');
                location.connections.Add(pair[0], int.Parse(pair[1]));
                
            }
            locationDict.Add(location.name, location);
        }

    }

}

public class Map
{
    [XmlArray("LocationList")]
    [XmlArrayItem("Location")]
    public List<Location> locations = new List<Location>();
}
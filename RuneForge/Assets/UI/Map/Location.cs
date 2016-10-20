using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class Location
{

    [XmlAttribute("name")]
    public string name;

    [XmlElement("Connections")]
    public string connectionStr;

    [XmlIgnoreAttribute]
    public Dictionary<string, int> connections = new Dictionary<string, int>();

    private bool firstVisit;


    //We need this, default constructor
    public Location()
    {

    }
}

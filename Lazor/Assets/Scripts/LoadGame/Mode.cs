using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
public class Mode {

	[XmlAttribute("name")]
	public string nameMode;
	[XmlElement("string")]
	public List<string> texts;
}

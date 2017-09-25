using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
[XmlRoot("ModeCollection")]
public class ModeContainer {
	[XmlArray("Modes")]
	[XmlArrayItem("Mode")]
	public List<Mode> modes = new List<Mode>();
	public static ModeContainer Load(string path){
		TextAsset _xml = Resources.Load<TextAsset> (path);

		XmlSerializer serializer = new XmlSerializer (typeof(ModeContainer));

		StringReader reader = new StringReader (_xml.text);

		ModeContainer modes = serializer.Deserialize (reader) as ModeContainer;
		reader.Close ();
		return modes;
	}
}

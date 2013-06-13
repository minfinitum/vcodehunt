using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace VCodeHunt.Config
{
    public class WindowFont : IXmlSerializable
    {
        public WindowFont()
        {
            Font = new Font("Courier New", 8.5f);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("UseFont", UseFont.ToString());
            if (UseFont)
            {
                writer.WriteElementString("FontFamily", Font.FontFamily.Name.ToString());
                writer.WriteElementString("Size", Font.Size.ToString());
            }
        }

        public void ReadXml(XmlReader reader)
        {
            UseFont = System.Convert.ToBoolean(reader.ReadElementString());

            if (UseFont)
            {
                string familyName = reader.ReadElementString();
                float size = (float)System.Convert.ToDouble(reader.ReadElementString());

                Font = new Font(familyName, size);
            }
        }


        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return (null);
        }

        public void SetFont(Font font)
        {
            UseFont = true;
            Font = font;
        }

        public bool UseFont { get; set; }

        [XmlIgnoreAttribute]
        public Font Font { get; set; }
    }
}

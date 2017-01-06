namespace VCodeHunt.Config
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;

    public class AppConfigIO
    {
        private static string m_file = "config.cfg";

        public static void Read<T>(ref T config)
        {
            bool error = false;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamReader reader = File.OpenText(m_file))
                {
                    config = (T)xs.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                error = true;
            }

            if (error)
            {
                try
                {
                    File.Delete(m_file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public static void Write<T>(T config)
        {
            bool error = false;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (StreamWriter writer = File.CreateText(m_file))
                {
                    xs.Serialize(writer, config);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                error = true;
            }

            if (error)
            {
                try
                {
                    File.Delete(m_file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}

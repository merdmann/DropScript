using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace UIDropScript
{
    public class ApplicationData
    {
        #region class variabels
        public static IDictionary<int, ApplicationData> items = new Dictionary<int, ApplicationData>();
        private static string applDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DropScript");
        private static string applPath = Path.Combine(applDirectory, "state.xml");
        #endregion

        #region instance variables
        private String script;          // point to the applData to be executed
        #endregion

        #region constructors
        public ApplicationData(int id)
        {
            items[id] = this;
        }
        #endregion

        #region getter and setters
        public String theScript {
            get
            {
                return this.script;
            }
            set
            {
                this.script = value;
            }
        }
        #endregion

        #region static methods
        static public void Reset()
        {
            if (Directory.Exists(applDirectory))
            {
                File.Delete(applPath);
                Directory.Delete(applDirectory);
            }
        }

        // save the application state
        static public void Save()
        {
            var applDoc = new XDocument(new XDeclaration("1.0", "utf-8", null));

            var root = new XElement("ApplicationData");
            applDoc.Add(root);

            foreach(int id in items.Keys)
            {
                String script = items[id].theScript;

                if (script == null)
                    script = "NULL";

                root.Add( new XElement("Parameter", 
                    new XElement("id", id ), new XElement("script", script )));
            }

            if (!Directory.Exists(applDirectory))
                Directory.CreateDirectory(applDirectory);
            applDoc.Save(applPath);
        }

        // load the configuration data 
        static public void Load()
        {
            for (int i = 0; i < 4; ++i)
                items[i] = new ApplicationData(i);

            if (File.Exists(applPath))
            {
                    XDocument doc = XDocument.Load(applPath);
                    List<XElement> result = doc.Element("ApplicationData").Elements("Parameter").ToList();

                    foreach (XElement p in result)
                    {
                        string ident = p.Element("id").FirstNode.ToString();
                        string script = p.Element("script").FirstNode.ToString();

                        if (script == "NULL")
                            script = null;

                        int id = Convert.ToInt16(ident);
                        items[id].theScript = script;
                    }
            }
        }
        #endregion
    }
}
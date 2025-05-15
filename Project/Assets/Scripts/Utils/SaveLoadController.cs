using System.Collections.Generic;
using System.IO;

public class SaveLoadController
{
    private string m_dataDirPath;
    private string m_dataFileName;

    public SaveLoadController(string dataDirPaht, string dataFileName)
    {
        m_dataDirPath = dataDirPaht;
        m_dataFileName = dataFileName;
        Erase();
    }

    public void Save(string objName)
    {
        string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);
        
        if(File.Exists(fullPath))
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(fullPath, FileMode.Append, FileAccess.Write)))
            {
                writer.WriteLine(objName);
                writer.Close();
            }
        }
        else
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(fullPath, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine(objName);
                writer.Close();
            }
        }
    }

    public List<string> Load()
    {
        string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);
        List<string> loadedData = new List<string>();

        if(File.Exists(fullPath))
        {
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    while(!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        loadedData.Add(line);
                    }
                }
            }
        }

        return loadedData;
    }

    public void Erase()
    {
        string fullPath = Path.Combine(m_dataDirPath, m_dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}

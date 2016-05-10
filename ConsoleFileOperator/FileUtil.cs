using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleFileOperator
{
    public class FileUtil
    {
        public XmlUtil xmlUtil = new XmlUtil();
        /// <summary>
        /// find all the files in the directory
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string[] GetFileNames(string filePath)
        {
            if (Directory.Exists(filePath))
            {
                return Directory.GetFiles(filePath);
            }
            return null;
        }
        /// <summary>
        /// get directory ,if not exist ,create it.
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public string GetDirectory(string loc)
        {
            if (Directory.Exists(loc))
            {
                return loc;
            }
            else
            {
                return Directory.CreateDirectory(loc).FullName;
            }
        }
        /// <summary>
        /// write into log
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="logPath"></param>
        public void RecordLog(string fileName, string logPath, string exception)
        {
            string name = string.Format("{0}{1}", DateTime.Now.Date.ToString("yyyyMMdd"), "Log.txt");
            string logFile = Path.Combine(GetDirectory(logPath), name);
            
            string content;
            if (!string.IsNullOrEmpty(exception))
            {
                content = string.Format("{0}: Error: {1} !Exception: {2}\n", DateTime.Now, fileName, exception);
            }
            else {
                content = string.Format("{0}: {1} is completed !\n", DateTime.Now, fileName);
            }
            StreamWriter file = new StreamWriter(logFile, true, Encoding.UTF8);
            file.Write(content);
            file.Close();

        }
        /// <summary>
        /// read and stored in DB , then move the file finished
        /// </summary>
        /// <param name="isStop"></param>
        public void StartService(bool isStop)
        {
            string filePath = xmlUtil.GetNodeValueByName(FilePathEnum.SourceDirectory.ToString());
            string destPath = xmlUtil.GetNodeValueByName(FilePathEnum.MoveDirectory.ToString());
            string logPath = xmlUtil.GetNodeValueByName(FilePathEnum.LogDirectory.ToString());
            string[] names;
            //var logic = new FileUploadLogic();

            while (isStop)
            {
                names = GetFileNames(filePath);
                if (names == null)
                {
                    continue;
                }
                foreach (var name in names)
                {
                    FileInfo file = new FileInfo(name);
                    if (!file.Extension.ToUpper().Equals(".CSV"))
                    {
                        continue;
                    }

                    string fileName = name.Split('\\').Last();

                    try
                    {
                        //logic.ReadStandardFile(name);
                        //logic.SaveStandardTransImport("System", name, logic.ReadStandardFile(name));
                        //log and move
                        RecordLog(fileName, logPath, string.Empty);
                        Directory.Move(name, Path.Combine(GetDirectory(destPath), fileName));
                    }
                    catch (Exception e)
                    {
                        RecordLog(fileName, logPath, e.Message);
                        break;
                    }
                }
            }
        }
    }


}

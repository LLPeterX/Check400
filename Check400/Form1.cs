using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Check400
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void buttonOpenXML_Click(object sender, EventArgs e)
        {
            string currentDirectory = Environment.CurrentDirectory;
            openXMLDialog.InitialDirectory = currentDirectory;
            openXMLDialog.ShowDialog();
            if (!String.IsNullOrEmpty(openXMLDialog.FileName))
            {
                this.tFileXML.Text = openXMLDialog.FileName;
            }

            // Пробуем определить, какой файл XSD следует использовать
            string xmlFileName = Path.GetFileName(tFileXML.Text); // чистое имя файла
            string xmlFileID = GetFileId(xmlFileName); // "BNS", "PB" и проч.
            // Сначала ищем файл в текущем каталоге
            string xsdFileName = "440-П_" + xmlFileID + ".xsd";
            string xsdFilePath = currentDirectory + "\\" + xsdFileName;
            if (File.Exists(xsdFilePath)) {
                tFileXSD.Text = xsdFilePath;
            } else  {
                // ищем файл подкаталоге "XSD" каталога запуска программы
                xsdFilePath = Application.StartupPath + "\\XSD\\" + xsdFileName;
                if (File.Exists(xsdFilePath))
                {
                    tFileXSD.Text = xsdFilePath;
                }
            }
            tMessage.Text = "";
        }

        private void buttonOpenXSD_Click(object sender, EventArgs e)
        {
            string currentDirectory;
            if (!String.IsNullOrEmpty(tFileXSD.Text))
            {
                currentDirectory = Path.GetDirectoryName(tFileXSD.Text);
            }else
            {
                currentDirectory = Environment.CurrentDirectory;
            }
            openXSDDialog.InitialDirectory = currentDirectory;
            openXSDDialog.ShowDialog();
            if (!String.IsNullOrEmpty(openXSDDialog.FileName))
            {
                tFileXSD.Text = openXSDDialog.FileName;
            }
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            
            string xmlFileName = tFileXML.Text;
            string xsdFileName = tFileXSD.Text;
            tMessage.Text = "";
            // Поверяем существование файлов XML и XSD
            if (!File.Exists(xmlFileName)) {
                //MessageBox.Show("Не найден файл XML:\n" + xmlFileName);
                tMessage.Text = "Не найден файл XML:\n" + xmlFileName;
                return;
            }
            if (!File.Exists(xsdFileName))
            {
                //MessageBox.Show("Не найден файл XSD:\n" + xsdFileName);
                tMessage.Text = "Не найден файл XSD:\n" + xsdFileName;
                return;
            }
            // пробуем определить целевое пространство имен из файла схемы
            
            XmlDocument xsd = new XmlDocument();
            try
            {
                xsd.Load(xsdFileName);
            } catch (Exception ex)
            {
                tMessage.Text = "Ошибка в файле схемы " + xsdFileName+"\n"+ex.Message;
                return;
            }
            string targetNamespace = xsd.DocumentElement.GetAttribute("targetNamespace");
            if (targetNamespace == null)  targetNamespace = "";
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(targetNamespace, xsdFileName);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            XmlReader reader = XmlReader.Create(xmlFileName, settings);
            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            try
            {
                // Пробуем определить значение namespace для файлов "440_П_xxx.xsd"
                xml.Load(reader);
                string xmlns = xml.DocumentElement.GetAttribute("xmlns");
                if (xmlns == null) xmlns = "";
                if(xmlns != targetNamespace)
                {
                    throw new Exception("Namespace отличаются:\nВ XML: "+xmlns+"\nВ XSD: "+targetNamespace);
                }
                tMessage.Text = "Успешно";
            } catch (Exception ex) {
                tMessage.Text = ex.Message;
            }
            reader.Close();
        } // buttonCheck_Click

        // Получить ID файла (PB, BNS, ZNO  проч.)
        private string GetFileId(string fileName)
        {
            string id = Path.GetFileName(fileName).ToUpper();
            if (id.StartsWith("PB") || id.StartsWith("BZ"))
            {
                id = id.Substring(0, 2);
            }
            else
            {
                id = id.Substring(0, 3);
            }
            return id;
        }

    }

}

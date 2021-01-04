﻿using System;
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
            string xmlFileName = Path.GetFileName(tFileXML.Text); // чистое имя файла без каталога
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
                if (!String.IsNullOrEmpty(tFileXML.Text))
                {
                    buttonCheck_Click(null, null);
                }
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
            // и проверить, что оно соответствует значению атрибута xmlns элемента <Файл>
            // (для файлов запросов targetNamespace не указан, так что "")
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
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.Schemas.Add(targetNamespace, xsdFileName);
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            XmlReader reader = XmlReader.Create(xmlFileName, settings);
            while (reader.Read()) { }
            reader.Close();
            if (tMessage.Text.Length == 0)
            {
                tMessage.AppendText("Успешно");
            }
        } // buttonCheck_Click
        public void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            IXmlLineInfo lineInfo = sender as IXmlLineInfo;
            XmlReader r = (XmlReader)sender;
            if (r.NodeType == XmlNodeType.Attribute)
            {
                // как получить родительский элемент текущего ошибочного атрибута ?
                tMessage.AppendText(String.Format("Line {0} Column {1}: Ошибка: {2}\r\n\r\n", lineInfo.LineNumber, lineInfo.LinePosition,
                   e.Message));
            }
            else
            {
                tMessage.AppendText(String.Format("Line {0} Column {1}: Элемент <{2}>, Ошибка: {3}\r\n\r\n", lineInfo.LineNumber, lineInfo.LinePosition,
                    r.Name, e.Message));
            }
        }

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

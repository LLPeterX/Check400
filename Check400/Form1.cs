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
            string xml_file = Path.GetFileName(tFileXML.Text); // чистое имя файла
            string xml_id = GetFileId(xml_file); // "BNS", "PB" и проч.
            // Сначала ищем файл в текущем каталоге
            string xsdName = "440-П_" + xml_id + ".xsd";
            string xsdPath = currentDirectory + "\\" + xsdName;
            if (File.Exists(xsdPath)) {
                tFileXSD.Text = xsdPath;
            } else  {
                // ищем файл подкаталоге "XSD" каталога запуска программы
                xsdPath = Application.StartupPath + "\\XSD\\" + xsdName;
                if (File.Exists(xsdPath))
                {
                    tFileXSD.Text = xsdPath;
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
            
            string xml_file = tFileXML.Text;
            string xsd_file = tFileXSD.Text;
            tMessage.Text = "";
            // Поверяем существование файлов
            if (!File.Exists(xml_file)) {
                MessageBox.Show("Не найден файл XML:\n" + xml_file);
                return;
            }
            if (!File.Exists(xsd_file))
            {
                MessageBox.Show("Не найден файл XSD:\n" + xsd_file);
                return;
            }
            XmlDocument xsd = new XmlDocument();
            xsd.Load(xsd_file);
            string targetNamespace = xsd.DocumentElement.GetAttribute("targetNamespace");
            if (targetNamespace == null)
            {
                targetNamespace = "";
            }
            MessageBox.Show("Namespace=" + targetNamespace);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(targetNamespace, xsd_file);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            XmlReader reader = XmlReader.Create(xml_file, settings);
            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            try
            {
                // Пробуем определить значение namespace для файлов "440_П_xxx.xsd"
                xml.Load(reader);
                tMessage.Text = "Успешно";
            } catch (Exception ex) {
                tMessage.Text = ex.Message;
                MessageBox.Show(ex.Message);
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

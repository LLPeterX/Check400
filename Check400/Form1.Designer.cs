namespace Check400
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tFileXML = new System.Windows.Forms.TextBox();
            this.tFileXSD = new System.Windows.Forms.TextBox();
            this.buttonOpenXML = new System.Windows.Forms.Button();
            this.buttonOpenXSD = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.openXMLDialog = new System.Windows.Forms.OpenFileDialog();
            this.tMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openXSDDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Файл XML";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Файл XSD";
            // 
            // tFileXML
            // 
            this.tFileXML.Location = new System.Drawing.Point(90, 13);
            this.tFileXML.Name = "tFileXML";
            this.tFileXML.Size = new System.Drawing.Size(571, 20);
            this.tFileXML.TabIndex = 2;
            // 
            // tFileXSD
            // 
            this.tFileXSD.Location = new System.Drawing.Point(90, 45);
            this.tFileXSD.Name = "tFileXSD";
            this.tFileXSD.Size = new System.Drawing.Size(571, 20);
            this.tFileXSD.TabIndex = 3;
            // 
            // buttonOpenXML
            // 
            this.buttonOpenXML.Location = new System.Drawing.Point(667, 13);
            this.buttonOpenXML.Name = "buttonOpenXML";
            this.buttonOpenXML.Size = new System.Drawing.Size(29, 23);
            this.buttonOpenXML.TabIndex = 4;
            this.buttonOpenXML.Text = "...";
            this.buttonOpenXML.UseVisualStyleBackColor = true;
            this.buttonOpenXML.Click += new System.EventHandler(this.buttonOpenXML_Click);
            // 
            // buttonOpenXSD
            // 
            this.buttonOpenXSD.Location = new System.Drawing.Point(666, 45);
            this.buttonOpenXSD.Name = "buttonOpenXSD";
            this.buttonOpenXSD.Size = new System.Drawing.Size(29, 23);
            this.buttonOpenXSD.TabIndex = 5;
            this.buttonOpenXSD.Text = "...";
            this.buttonOpenXSD.UseVisualStyleBackColor = true;
            this.buttonOpenXSD.Click += new System.EventHandler(this.buttonOpenXSD_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Location = new System.Drawing.Point(729, 13);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(71, 55);
            this.buttonCheck.TabIndex = 6;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // openXMLDialog
            // 
            this.openXMLDialog.Filter = "XML files|*.xml";
            this.openXMLDialog.InitialDirectory = ".";
            // 
            // tMessage
            // 
            this.tMessage.Location = new System.Drawing.Point(19, 100);
            this.tMessage.Multiline = true;
            this.tMessage.Name = "tMessage";
            this.tMessage.Size = new System.Drawing.Size(784, 243);
            this.tMessage.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Результат проверки";
            // 
            // openXSDDialog
            // 
            this.openXSDDialog.Filter = "XSD files|*.xsd";
            this.openXSDDialog.ShowReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 355);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tMessage);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonOpenXSD);
            this.Controls.Add(this.buttonOpenXML);
            this.Controls.Add(this.tFileXSD);
            this.Controls.Add(this.tFileXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Проверка файлов по 440-П v.4.00";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tFileXML;
        private System.Windows.Forms.TextBox tFileXSD;
        private System.Windows.Forms.Button buttonOpenXML;
        private System.Windows.Forms.Button buttonOpenXSD;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.OpenFileDialog openXMLDialog;
        private System.Windows.Forms.TextBox tMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openXSDDialog;
    }
}

